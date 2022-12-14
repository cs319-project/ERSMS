import {Component, OnInit, ViewChild} from '@angular/core';
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
} from "ng-apexcharts";
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {createNewUser, UserData} from "../placement/placement.component";

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

export interface todoItem{
  description: string;
  isCompleted: boolean;
  isStarred: boolean;
};

export interface activity{
  name: string;
  description: string;
  time: string;
};

export interface dayActivities{
  date: string;
  activities: activity[];
}

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
  })

export class DashboardComponent implements OnInit{
  displayedColumns = ['name', 'email', 'preferences', 'score'];
  dataSource: MatTableDataSource<UserData>;
  page_index = 0;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  @ViewChild("chart") chart: ChartComponent;
  public pieChartOptions: Partial<PieChartOptions>;
  public barChartOptions: Partial<BarChartOptions>;

  todoList: todoItem[] = [{description:"Kutay Tire", isCompleted: false, isStarred:true},
    { description: "Meeting with Kutay Tire at 15.30", isCompleted: false, isStarred: false },
    { description: "Check Atak Talay Yücel's Pre-approval Form", isCompleted: false, isStarred: true },
    { description: "Check Yiğit Yalın's Pre-approval Form", isCompleted: false, isStarred: true }
  ];

  activities: dayActivities[] = [{date: "12 September",activities:
[{name: "Kutay Tire", description: "Added a new pre-approval form.", time: "22:13"},
  {name: "Berk Çakar", description: "Added a new pre-approval form.",  time: "12:13"},
  {name: "Kutay Tire", description: "Added a new pre-approval form.",  time: "13:12"},
  {name: "Berk Çakar", description: "Added a new pre-approval form.",  time: "09:44"}]},
    {date: "15 September",activities:
        [{name: "Atak Talay Yücel", description: "Added a new pre-approval form.", time: "10:15"},
          {name: "Borga Haktan Bilen", description: "Added a new pre-approval form.", time: "07:07"},
          {name: "Atak Talay Yücel", description: "Added a new pre-approval form.", time: "11:44"},
          {name: "Borga Haktan Bilen", description: "Added a new pre-approval form.", time: "10:10"}]}
  ];



  selectedTabIndex = 0;

  waitingList: todoItem[];
  starredList: todoItem[];
  completedList: todoItem[];

  editingItem: todoItem = null;
  editingValue: string;

  addingValue: string;

  editing = true;
  value = "Todo Item";
  isAdding = false;

  ngOnInit(): void {
    this.waitingList = this.todoList.filter(todoItem => !todoItem.isCompleted);
    this.starredList = this.todoList.filter(todoItem => todoItem.isStarred);
    this.completedList = this.todoList.filter(todoItem => todoItem.isCompleted);
  }

  constructor(){
    this.pieChartOptions = {
      series: [44, 55, 13],
      chart: {
        type: "donut",
        toolbar: {
          show: true
        }
      },
      colors: [ 
        "#FF965D",
        "#49C96D",
        "#FD7972"
      ],
      labels: ["Processing", "Accepted  ", "Rejected"],
      responsive: [
        {
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              position: "bottom"
            }
          }
        }
      ]
    };

    this.barChartOptions = {
      series: [
        {
          name: "submission",
          data: [21, 22, 10, 28, 16, 21, 13]
        }
      ],
      chart: {
        height: 350,
        type: "bar",
        events: {
          click: function(chart, w, e) {
            // console.log(chart, w, e)
          }
        }
      },
      colors: [
        "#008FFB",
        "#00E396",
      ],
      plotOptions: {
        bar: {
          columnWidth: "45%",
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
        categories: [
          "Mon",
          "Tue",
          "Wed",
          "Thur",
          "Fri",
          "Sat",
          "Sun"
        ],
        labels: {
          style: {
            colors: [
            ],
            fontSize: "12px"
          }
        }
      }
    };

    const users: UserData[] = [];
    for (let i = 1; i <= 100; i++) { users.push(createNewUser()); }

    // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource(users);
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
    this.addingValue = "";
    this.isAdding = true;
  }

  cancelAdding() {
    this.isAdding = false;
  }

  addItem() {
    this.todoList.push({description: this.addingValue, isCompleted: false, isStarred: false});
    this.waitingList = this.todoList.filter(todoItem => !todoItem.isCompleted);
    this.addingValue = "";
    this.isAdding = false;
    this.selectedTabIndex = 0;
  }

  deleteItem(todoItem: todoItem) {
    let index: number = this.todoList.indexOf(todoItem);
    this.todoList.splice(index,1);
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
}

