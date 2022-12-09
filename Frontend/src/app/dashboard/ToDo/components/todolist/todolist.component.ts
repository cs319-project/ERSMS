import { ForwardRefHandling } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';

import { Todo, Status } from '../../todo.interface';

import { TodoService } from '../../todo.service';


@Component({
  selector: 'app-todo-list',
  templateUrl: './todolist.component.html',
  styleUrls: ['./todolist.component.scss']
})
export class TodoListComponent {

  todos: Todo[] = [];
  isNewTodo: boolean = false;


  constructor(private todoService: TodoService) {}

  ngOnInit(): void {
    this.fetchTodoList();
  }

  fetchTodoList(): void {
    this.todoService
      .getTodos()
      .subscribe(todos => this.todos = todos);
  }

  onNewTask(description: string): void {
    const newTask = {
      id:  this.todoService.getNextId(),
      description,
      status: Status.Open,
      done: false
    };

    this.todoService
      .addTodo(newTask)
      .subscribe(response => this.todos = [...this.todos, newTask]);
  }

  complete(id: number): void {
    
  }

  deleteTodo(id: number): void {
    this.todoService
      .deleteTodo(id)
      .subscribe(({ response }) => this.todos = response);
  }

}
