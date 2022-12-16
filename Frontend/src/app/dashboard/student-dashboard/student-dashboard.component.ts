import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatTable } from '@angular/material/table';
import { UserData } from 'src/app/placement/placement.component';
import { ActorsEnum } from 'src/app/_models/enum/actors-enum';
import { DepartmentsEnum } from 'src/app/_models/enum/departments-enum';
import { ToDoItem } from 'src/app/_models/to-do-item';
import { ToDoItemService } from 'src/app/_services/todoitem.service';
import { GUID } from 'src/utils/guid';

export interface todoItem {
  description: string;
  isCompleted: boolean;
  isStarred: boolean;
  id: GUID;
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
  selector: 'app-student-dashboard',
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css']
})
export class StudentDashboardComponent implements OnInit {
  todoList: todoItem[] = [];
  actorsEnum = ActorsEnum;
  role: string;
  _departmentsEnum = DepartmentsEnum;
  departmentsEnum = Object.keys(DepartmentsEnum);

  constructor(
    private _formBuilder: FormBuilder,
    private toDoService: ToDoItemService
  ) {
    this.role = JSON.parse(localStorage.getItem('user')).roles[0];

    toDoService.getStudentToDoList('22002700').subscribe(data => {
      //console.log(data);
      data.forEach(element => {
        let temp: todoItem = {
          description: element.description,
          isCompleted: element.isComplete,
          isStarred: element.isStarred,
          id: new GUID(element.id)
        };
        //console.log(element);
        this.addItem2(temp);
      });
    });

    console.log(this.todoList);
  }

  stateForm = this._formBuilder.group({
    stateGroup: ''
  });

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
    this.toDoService.starToDoItem(todoItem.id, !todoItem.isStarred).subscribe();
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
      isStarred: false,
      id: null
    });
    this.waitingList = this.todoList.filter(todoItem => !todoItem.isCompleted);
    this.addingValue = '';
    this.isAdding = false;
    this.selectedTabIndex = 0;
  }

  addItem2(todoItem: todoItem) {
    this.todoList.push({
      description: todoItem.description,
      isCompleted: todoItem.isCompleted,
      isStarred: todoItem.isStarred,
      id: todoItem.id
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
}
