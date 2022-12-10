import { Component, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-todo-form',
  templateUrl: './todoform.component.html',
  styleUrls: ['./todoform.component.scss']
})
export class TodoFormComponent { 

  description: string;

  @Output() closeForm: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() newTask: EventEmitter<string> = new EventEmitter<string>();

  constructor() {}

  submitNewTask(value: string): void {
    this.description = '';
    this.newTask.emit(value);
    this.closeForm.emit(true);
  }

}
