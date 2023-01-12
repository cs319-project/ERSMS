import {
  AfterViewInit,
  Component,
  Input,
  OnInit,
  ViewChild
} from '@angular/core';
import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexPlotOptions,
  ApexYAxis,
  ApexXAxis,
  ApexAxisChartSeries,
  ApexGrid,
  ApexLegend
} from 'ng-apexcharts';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { createNewUser, UserData } from '../placement/placement.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../appointments/confirmation-dialog/confirmation-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ScoreTableUploadDialogComponent } from './score-table-upload-dialog/score-table-upload-dialog.component';
import { FormBuilder } from '@angular/forms';
import { ActorsEnum } from '../_models/enum/actors-enum';
import { DepartmentsEnum } from '../_models/enum/departments-enum';
import { ToastrService } from 'ngx-toastr';
import { PlacementService } from '../_services/placement.service';
import { PlacementTable } from '../_models/placement-table';
import { CTEFormService } from '../_services/cteform.service';
import { PreApprovalFormService } from '../_services/preapprovalform.service';
import { EquivalenceRequestService } from '../_services/equivalencerequest.service';
import { DepartmentToFacultyMapper } from 'src/utils/department-to-faculty-mapper';
import { Router } from '@angular/router';

export type PieChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  colors: string[];
  responsive: ApexResponsive[];
  labels: any;
};

export type BarChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  grid: ApexGrid;
  colors: string[];
  legend: ApexLegend;
};

export interface todoItem {
  description: string;
  isCompleted: boolean;
  isStarred: boolean;
}

export interface activity {
  name: string;
  description: string;
  time: string;
}

export interface dayActivities {
  date: string;
  activities: activity[];
}

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, AfterViewInit {
  actorsEnum = ActorsEnum;
  role: string;
  oisepDepartment: string;
  _departmentsEnum = DepartmentsEnum;
  departmentsEnum = Object.keys(DepartmentsEnum).filter(
    k => k !== 'NotSpecified'
  );
  placementTables: PlacementTable[] = [];
  placementTable: PlacementTable;

  @ViewChild(MatTable) scoreTable: MatTable<UserData>;

  stateForm = this._formBuilder.group({
    stateGroup: ''
  });

  @Input()
  requiredFileType: string;

  fileName: string;
  displayedColumns = ['name', 'email', 'preferences', 'score'];
  dataSource: MatTableDataSource<UserData>;
  page_index = 0;

  department: string = 'CS';

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('chart') chart: ChartComponent;
  public pieChartOptions: Partial<PieChartOptions>;
  public barChartOptions: Partial<BarChartOptions>;

  todoList: todoItem[] = [
    { description: 'Kutay Tire', isCompleted: false, isStarred: true },
    {
      description: 'Meeting with Kutay Tire at 15.30',
      isCompleted: false,
      isStarred: false
    },
    {
      description: "Check Atak Talay Yücel's Pre-Approval Form",
      isCompleted: false,
      isStarred: true
    },
    {
      description: "Check Yiğit Yalın's Pre-Approval Form",
      isCompleted: false,
      isStarred: true
    }
  ];

  selectedTabIndex = 0;

  waitingList: todoItem[];
  starredList: todoItem[];
  completedList: todoItem[];

  editingItem: todoItem = null;
  editingValue: string;

  addingValue: string;

  value = 'Todo Item';
  isAdding = false;
  departmentTables;

  ngOnInit(): void {
    this.waitingList = this.todoList.filter(todoItem => !todoItem.isCompleted);
    this.starredList = this.todoList.filter(todoItem => todoItem.isStarred);
    this.completedList = this.todoList.filter(todoItem => todoItem.isCompleted);
  }

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private _formBuilder: FormBuilder,
    private toastr: ToastrService,
    private placementService: PlacementService
  ) {
    this.role = JSON.parse(localStorage.getItem('user')).roles[0];
    this.pieChartOptions = {
      series: [44, 55, 13],
      chart: {
        type: 'donut',
        toolbar: {
          show: true
        }
      },
      colors: ['#FF965D', '#49C96D', '#FD7972'],
      labels: ['Processing', 'Accepted  ', 'Rejected'],
      responsive: [
        {
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              position: 'bottom'
            }
          }
        }
      ]
    };

    this.barChartOptions = {
      series: [
        {
          name: 'submission',
          data: [21, 22, 10, 28, 16, 21, 13]
        }
      ],
      chart: {
        height: 350,
        type: 'bar',
        events: {
          click: function (chart, w, e) {
            // console.log(chart, w, e)
          }
        }
      },
      colors: ['#008FFB', '#00E396'],
      plotOptions: {
        bar: {
          columnWidth: '45%',
          distributed: true
        }
      },
      dataLabels: {
        enabled: true
      },
      legend: {
        show: false
      },
      grid: {
        show: true
      },
      xaxis: {
        categories: ['Mon', 'Tue', 'Wed', 'Thur', 'Fri', 'Sat', 'Sun'],
        labels: {
          style: {
            colors: [],
            fontSize: '12px'
          }
        }
      }
    };

    const csUsers: UserData[] = [];
    for (let i = 1; i <= 100; i++) {
      csUsers.push(createNewUser());
    }

    const mathUsers: UserData[] = [];
    for (let i = 1; i <= 100; i++) {
      mathUsers.push(createNewUser());
    }

    this.departmentTables = {
      CS: csUsers,
      MATH: mathUsers,
      PHYS: []
    };

    // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource(
      this.departmentTables[this.department]
    );
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  toggleEditing() {
    this.editingItem = null;
  }

  OnTabChange(index) {
    console.log(index);
  }

  toggleAdd() {
    this.isAdding = !this.isAdding;
  }

  starClicked(todoItem: todoItem) {
    todoItem.isStarred = !todoItem.isStarred;
    this.starredList = this.todoList.filter(todoItem => todoItem.isStarred);
  }

  checkboxClicked(todoItem: todoItem) {
    todoItem.isCompleted = !todoItem.isCompleted;
    this.completedList = this.todoList.filter(todoItem => todoItem.isCompleted);
    this.waitingList = this.todoList.filter(todoItem => !todoItem.isCompleted);
  }

  startEditing(itemList: todoItem[], i: number) {
    this.editingValue = itemList[i].description;
    this.editingItem = itemList[i];
  }

  cancelEditing() {
    this.editingItem = null;
  }

  saveEditing(todoItem: todoItem) {
    todoItem.description = this.editingValue;
    this.editingItem = null;
  }

  startAdding() {
    this.addingValue = '';
    this.isAdding = true;
  }

  cancelAdding() {
    this.isAdding = false;
  }

  addItem() {
    this.todoList.push({
      description: this.addingValue,
      isCompleted: false,
      isStarred: false
    });
    this.waitingList = this.todoList.filter(todoItem => !todoItem.isCompleted);
    this.addingValue = '';
    this.isAdding = false;
    this.selectedTabIndex = 0;
  }

  deleteItem(todoItem: todoItem) {
    let index: number = this.todoList.indexOf(todoItem);
    this.todoList.splice(index, 1);
    this.waitingList = this.todoList.filter(todoItem => !todoItem.isCompleted);
    this.starredList = this.todoList.filter(todoItem => todoItem.isStarred);
    this.completedList = this.todoList.filter(todoItem => todoItem.isCompleted);
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  handlePageEvent(e: PageEvent) {
    this.page_index = e.pageIndex;
  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }

  deleteScoreTable(department: string) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      text: `Are you sure to delete the score table for ${department} department?`
    };
    const dialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      dialogConfig
    );
    dialogRef.afterClosed().subscribe(deleteItem => {
      if (deleteItem) {
        this.openSnackBar(
          `Score table for ${department} department is deleted`,
          'Close',
          5
        );
      }
    });
  }

  onFileSelected(event, department) {
    const file: File = event.target.files[0];

    if (file) {
      this.fileName = file.name;
    }

    if (!this.fileName.endsWith('.xlsx')) {
      this.toastr.error('Please select a valid Excel file (.xlsx)');
      return;
    }
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      text: `Are you sure to upload this score table for  ${this.oisepDepartment}?`,
      fileName: this.fileName
    };
    const dialogRef = this.dialog.open(
      ScoreTableUploadDialogComponent,
      dialogConfig
    );

    dialogRef.afterClosed().subscribe(uploadItem => {
      if (uploadItem) {
        this.placementService
          .uploadPlacementTable(
            file,
            this.oisepDepartment,
            DepartmentToFacultyMapper.map(this.oisepDepartment).toString()
          )
          .subscribe(
            res => {
              this.updatePlacementTables();
              this.toastr.success('Score table uploaded successfully');
            },
            err => {
              console.log(err);
              this.toastr.error('Error uploading score table: ' + err.error);
            }
          );
      }
    });
  }

  updatePlacementTables() {
    this.placementTables = [];
    this.placementService
      .getPlacementTables(
        this.oisepDepartment,
        DepartmentToFacultyMapper.map(this.oisepDepartment).toString()
      )
      .subscribe(res => {
        this.placementTables = res;
      });
  }

  downloadPlacementTable() {
    this.placementService
      .downloadPlacementTable(this.placementTable.id)
      .subscribe(
        res => {
          const blob = new Blob([res], {
            type: this.placementTable.fileName.endsWith('.xlsx')
              ? 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
              : 'application/vnd.ms-excel'
          });
          const url = window.URL.createObjectURL(blob);
          const link = document.createElement('a');
          link.href = url;
          link.download = this.placementTable.fileName;
          link.click();
        },
        err => {
          this.toastr.error('Error when downloading the score table');
        },
        () => {
          this.toastr.success('Score table downloaded successfully');
        }
      );
  }

  deletePlacementTable() {
    this.placementService
      .deletePlacementTable(this.placementTable.id)
      .subscribe(
        res => {
          this.updatePlacementTables();
          this.placementTable = null;
        },
        err => {
          this.toastr.error('Error when deleting the score table');
        },
        () => {
          this.toastr.success('Score table deleted successfully');
        }
      );
  }

  updatePlacementTable() {
    // this.placementService
    //   .downloadPlacementTable(this.placementTable.id)
    //   .subscribe(res => {
    //     const blob = new Blob([res], {
    //       type: this.placementTable.fileName.endsWith('.xlsx')
    //         ? 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
    //         : 'application/vnd.ms-excel'
    //     });
    //     console.log(blob);
    //   });
  }

  onDepartmentSelect() {
    this.dataSource = new MatTableDataSource(
      this.departmentTables[this.department]
    );
    this.dataSource = new MatTableDataSource(
      this.departmentTables[this.department]
    );
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.scoreTable.renderRows();
    console.log(this.department);
  }
}
