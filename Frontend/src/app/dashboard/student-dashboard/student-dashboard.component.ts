import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Announcement } from 'src/app/_models/announcement';
import { ActorsEnum } from 'src/app/_models/enum/actors-enum';
import { ToDoItem } from 'src/app/_models/to-do-item';
import { AnnouncementService } from 'src/app/_services/announcement.service';
import { ToDoItemService } from 'src/app/_services/todoitem.service';
import { GUID } from 'src/utils/guid';
import { formatDate } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

/*
export interface todoItem {
  description: string;
  isCompleted: boolean;
  isStarred: boolean;
  id: GUID;
}
*/
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
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css']
})
export class StudentDashboardComponent implements OnInit {
  todoList: ToDoItem[] = [];
  actorsEnum = ActorsEnum;
  role: string;
  userName: string;
  announcements: Announcement[] = [];

  dateFormat = 'dd/MM/yyyy h:mm';
  timeFormat = 'h:mm';
  locale = 'en-TR';

  constructor(
    private toastr: ToastrService,
    private _formBuilder: FormBuilder,
    private toDoService: ToDoItemService,
    private announcementService: AnnouncementService
  ) {
    this.role = JSON.parse(localStorage.getItem('user')).roles[0];
    this.userName = JSON.parse(localStorage.getItem('user')).userName;

    this.PopulateToDoList();

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
  }

  PopulateToDoList() {
    this.todoList = [];
    this.toDoService.getStudentToDoList(this.userName).subscribe(data => {
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
        console.log(this.editingValue);
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
          this.toastr.success('ToDo Item is successfully added');
          this.addingValue = '';
          this.isAdding = false;
          this.selectedTabIndex = 0;
        } else {
          this.toastr.error('Error Occured while adding the ToDo Item');
        }
      },
      error => {
        this.toastr.error('Error Occured while adding the ToDo Item');
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
