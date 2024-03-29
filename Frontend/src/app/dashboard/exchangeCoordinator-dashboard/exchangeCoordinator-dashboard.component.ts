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
import { CTEFormService } from 'src/app/_services/cteform.service';
import { EquivalenceRequestService } from 'src/app/_services/equivalencerequest.service';
import { PreApprovalFormService } from 'src/app/_services/preapprovalform.service';
import { CteForm } from 'src/app/_models/cte-form';
import { PreApprovalForm } from 'src/app/_models/pre-approval-form';
import { EquivalenceRequest } from 'src/app/_models/equivalence-request';
import { Announcement } from '../../_models/announcement';
import { AnnouncementService } from '../../_services/announcement.service';
import { formatDate } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

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
  // eslint-disable-next-line @angular-eslint/component-selector
  selector: 'app-exchangeCoordinator-dashboard',
  templateUrl: './exchangeCoordinator-dashboard.component.html',
  styleUrls: ['./exchangeCoordinator-dashboard.component.css']
})
export class ExchangeCoordinatorDashboardComponent implements OnInit {
  todoList: ToDoItem[] = [];
  approved: number[] = [];
  rejected: number[] = [];
  processing: number[] = [];
  cteForms: number[] = [];
  preApproval: number[] = [];
  courseEq: number[] = [];
  actorsEnum = ActorsEnum;
  role: string;
  userName: string;
  _departmentsEnum = DepartmentsEnum;
  departmentsEnum = Object.keys(DepartmentsEnum);

  @ViewChild('chart') chart: ChartComponent;
  public pieChartOptions: Partial<PieChartOptions>;
  public barChartOptions: Partial<BarChartOptions>;

  announcements: Announcement[] = [];

  dateFormat = 'dd/MM/yyyy h:mm';
  timeFormat = 'h:mm';
  locale = 'en-TR';

  constructor(
    private toastr: ToastrService,
    private _formBuilder: FormBuilder,
    private toDoService: ToDoItemService,
    private cteFormService: CTEFormService,
    private preapprovalFormService: PreApprovalFormService,
    private courseEqService: EquivalenceRequestService,
    private announcementService: AnnouncementService
  ) {
    this.role = JSON.parse(localStorage.getItem('user')).roles[0];
    this.userName = JSON.parse(localStorage.getItem('user')).userName;

    this.PopulateToDoList();

    cteFormService.getCTEFormsByDepartment(this.userName).subscribe(data => {
      data.forEach(element => {
        if (element.isRejected) {
          this.rejected.push(0);
        } else if (element.isApproved) {
          this.approved.push(0);
        } else if (!element.isCanceled && !element.isArchived) {
          this.processing.push(0);
        }

        this.cteForms.push(0);

        this.pieChartOptions = {
          series: [
            this.processing.length,
            this.approved.length,
            this.rejected.length
          ],
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
              name: 'Form Types',
              data: [
                this.cteForms.length,
                this.preApproval.length,
                this.courseEq.length
              ]
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
          colors: ['#008FFB', '#00E396', '#DBA800'],
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
            categories: ['CTE', 'Pre-Approval', 'Course Equivalence'],
            labels: {
              style: {
                colors: [],
                fontSize: '12px'
              }
            }
          }
        };
      });
    });

    preapprovalFormService
      .getPreApprovalFormsByDepartment(this.userName)
      .subscribe(data => {
        data.forEach(element => {
          if (element.isRejected) {
            this.rejected.push(0);
          } else if (element.isApproved) {
            this.approved.push(0);
          } else if (!element.isCanceled && !element.isArchived) {
            this.processing.push(0);
          }

          this.preApproval.push(0);

          this.pieChartOptions = {
            series: [
              this.processing.length,
              this.approved.length,
              this.rejected.length
            ],
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
                name: 'Form Types',
                data: [
                  this.cteForms.length,
                  this.preApproval.length,
                  this.courseEq.length
                ]
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
            colors: ['#008FFB', '#00E396', '#DBA800'],
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
              categories: ['CTE', 'Pre-Approval', 'Course Equivalence'],
              labels: {
                style: {
                  colors: [],
                  fontSize: '12px'
                }
              }
            }
          };
        });
      });

    courseEqService
      .getEquivalenceRequestsByDepartment(this.userName)
      .subscribe(data => {
        data.forEach(element => {
          if (element.isRejected) {
            this.rejected.push(0);
          } else if (element.isApproved) {
            this.approved.push(0);
          } else if (!element.isCanceled && !element.isArchived) {
            this.processing.push(0);
          }

          this.courseEq.push(0);

          this.pieChartOptions = {
            series: [
              this.processing.length,
              this.approved.length,
              this.rejected.length
            ],
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
                name: 'Form Types',
                data: [
                  this.cteForms.length,
                  this.preApproval.length,
                  this.courseEq.length
                ]
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
            colors: ['#008FFB', '#00E396', '#DBA800'],
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
              categories: ['CTE', 'Pre-Approval', 'Course Equivalence'],
              labels: {
                style: {
                  colors: [],
                  fontSize: '12px'
                }
              }
            }
          };
        });
      });

    announcementService.getAllAnnouncements().subscribe(data => {
      if (data) {
        data.forEach(element => {
          let temp: Announcement = {
            id: element.id,
            sender: element.sender,
            creationDate: element.creationDate,
            description: element.description
          };
          this.announcements.unshift(temp);
        });
      }
    });

    this.pieChartOptions = {
      series: [
        this.processing.length,
        this.approved.length,
        this.rejected.length
      ],
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
          name: 'Form Types',
          data: [
            this.cteForms.length,
            this.preApproval.length,
            this.courseEq.length
          ]
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
      colors: ['#008FFB', '#00E396', '#DBA800'],
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
        categories: ['CTE', 'Pre-Approval', 'Course Equivalence'],
        labels: {
          style: {
            colors: [],
            fontSize: '12px'
          }
        }
      }
    };
  }

  PopulateToDoList() {
    this.todoList = [];
    this.toDoService.getCoordinatorToDoList(this.userName).subscribe(data => {
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
    // console.log(todoItem);
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
    todoItem.description = this.editingValue;
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
    this.toDoService.createToDoItem(newItem, this.userName).subscribe(
      result => {
        if (result) {
          this.PopulateToDoList();
          this.waitingList = this.todoList.filter(
            todoItem => !todoItem.isComplete
          );
          this.toastr.success('To-Do item is successfully added');
          this.addingValue = '';
          this.isAdding = false;
          this.selectedTabIndex = 0;
        } else {
          this.toastr.error('An error occured while adding the to-do item');
        }
      },
      error => {
        this.toastr.error('An error occured while adding the to-do item');
      }
    );
  }

  addItem2(todoItem: ToDoItem) {
    this.todoList.unshift({
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

  formatTheDate(date: Date) {
    const formattedDate = formatDate(
      date.toString(),
      this.dateFormat,
      this.locale
    );
    return formattedDate;
  }
}
