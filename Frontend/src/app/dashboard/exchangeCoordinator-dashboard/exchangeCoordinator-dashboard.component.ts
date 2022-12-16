
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActorsEnum } from 'src/app/_models/enum/actors-enum';
import { DepartmentsEnum } from 'src/app/_models/enum/departments-enum';
import { ToDoItem } from 'src/app/_models/to-do-item';
import { ToDoItemService } from 'src/app/_services/todoitem.service';


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
  selector: 'app-exchangeCoordinator-dashboard',
  templateUrl: './exchangeCoordinator-dashboard.component.html',
  styleUrls: ['./exchangeCoordinator-dashboard.component.css']
})
export class ExchangeCoordinatorDashboardComponent implements OnInit {
  todoList: ToDoItem[] = [];
  actorsEnum = ActorsEnum;
  role: string;
  userName: string;
  _departmentsEnum = DepartmentsEnum;
  departmentsEnum = Object.keys(DepartmentsEnum);

  @ViewChild('chart') chart: ChartComponent;
  public pieChartOptions: Partial<PieChartOptions>;
  public barChartOptions: Partial<BarChartOptions>;

  activities: dayActivities[] = [
    {
      date: '12 September',
      activities: [
        {
          name: 'Kutay Tire',
          description: 'Added a new pre-approval form.',
          time: '22:13'
        },
        {
          name: 'Berk Çakar',
          description: 'Added a new pre-approval form.',
          time: '12:13'
        },
        {
          name: 'Kutay Tire',
          description: 'Added a new pre-approval form.',
          time: '13:12'
        },
        {
          name: 'Berk Çakar',
          description: 'Added a new pre-approval form.',
          time: '09:44'
        }
      ]
    },
    {
      date: '15 September',
      activities: [
        {
          name: 'Atak Talay Yücel',
          description: 'Added a new pre-approval form.',
          time: '10:15'
        },
        {
          name: 'Borga Haktan Bilen',
          description: 'Added a new pre-approval form.',
          time: '07:07'
        },
        {
          name: 'Atak Talay Yücel',
          description: 'Added a new pre-approval form.',
          time: '11:44'
        },
        {
          name: 'Borga Haktan Bilen',
          description: 'Added a new pre-approval form.',
          time: '10:10'
        }
      ]
    }
  ];

  constructor(
    private _formBuilder: FormBuilder,
    private toDoService: ToDoItemService
  ) {
    this.role = JSON.parse(localStorage.getItem('user')).roles[0];
    this.userName = JSON.parse(localStorage.getItem('user')).userName;

    toDoService.getStudentToDoList('22002700').subscribe(data => {
      //console.log(data);
      data.forEach(element => {
        let temp: ToDoItem = {
          cascadeId: null,
          title: '',
          description: element.description,
          isComplete: element.isComplete,
          isStarred: element.isStarred,
          id: element.id
        };
        //console.log(element);
        this.addItem2(temp);
      });
      this.waitingList = this.todoList.filter(todoItem => !todoItem.isComplete);
      this.starredList = this.todoList.filter(todoItem => todoItem.isStarred);
      this.completedList = this.todoList.filter(
        todoItem => todoItem.isComplete
      );
    });

    console.log(this.todoList);

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
  }

  stateForm = this._formBuilder.group({
    stateGroup: ''
  });

  selectedTabIndex = 0;

  waitingList: ToDoItem[];
  starredList: ToDoItem[];
  completedList: ToDoItem[];

  editingItem: ToDoItem = null;
  editingValue: string;

  addingValue: string;

  value = 'Todo Item';
  isAdding = false;
  departmentTables;

  ngOnInit(): void {
    this.waitingList = this.todoList.filter(todoItem => !todoItem.isComplete);
    this.starredList = this.todoList.filter(todoItem => todoItem.isStarred);
    this.completedList = this.todoList.filter(todoItem => todoItem.isComplete);
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

  starClicked(todoItem: ToDoItem) {
    console.log(todoItem);
    this.toDoService
      .starToDoItem(todoItem.id, !todoItem.isStarred)
      .subscribe(result => {
        if (result) {
          todoItem.isStarred = !todoItem.isStarred;
          this.starredList = this.todoList.filter(
            todoItem => todoItem.isStarred
          );
        }
      });
  }

  checkboxClicked(todoItem: ToDoItem) {
    this.toDoService
      .completeToDoItem(todoItem.id, !todoItem.isComplete)
      .subscribe(result => {
        if (result) {
          todoItem.isComplete = !todoItem.isComplete;
          this.completedList = this.todoList.filter(
            todoItem => todoItem.isComplete
          );
          this.waitingList = this.todoList.filter(
            todoItem => !todoItem.isComplete
          );
        }
      });
  }

  startEditing(itemList: ToDoItem[], i: number) {
    this.editingValue = itemList[i].description;
    this.editingItem = itemList[i];
  }

  cancelEditing() {
    this.editingItem = null;
  }

  saveEditing(todoItem: ToDoItem) {
    this.toDoService.updateToDoItem(todoItem).subscribe(result => {
      if (result) {
        todoItem.description = this.editingValue;
      }
    });
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
    let newItem: ToDoItem = {
      description: this.addingValue,
      isComplete: false,
      isStarred: false,
      title: ''
    };
    this.toDoService
      .createToDoItem(newItem, this.userName)
      .subscribe(result => {
        if (result) {
          this.todoList.push(newItem);
          this.waitingList = this.todoList.filter(
            todoItem => !todoItem.isComplete
          );
        }
      });
    this.addingValue = '';
    this.isAdding = false;
    this.selectedTabIndex = 0;
  }

  addItem2(todoItem: ToDoItem) {
    this.todoList.push({
      description: todoItem.description,
      isComplete: todoItem.isComplete,
      cascadeId: undefined,
      title: '',
      isStarred: todoItem.isStarred,
      id: todoItem.id
    });
    this.waitingList = this.todoList.filter(todoItem => !todoItem.isComplete);
    this.addingValue = '';
    this.isAdding = false;
    this.selectedTabIndex = 0;
  }

  deleteItem(todoItem: ToDoItem) {
    this.toDoService.deleteToDoItem(todoItem.id).subscribe(result => {
      let index: number = this.todoList.indexOf(todoItem);
      this.todoList.splice(index, 1);
      this.waitingList = this.todoList.filter(todoItem => !todoItem.isComplete);
      this.starredList = this.todoList.filter(todoItem => todoItem.isStarred);
      this.completedList = this.todoList.filter(
        todoItem => todoItem.isComplete
      );
    });
  }
}