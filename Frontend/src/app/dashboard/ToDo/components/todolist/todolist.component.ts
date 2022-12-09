import {Component} from '@angular/core';

import {Status, Todo} from '../../todo.interface';

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

  changeEvent($event, id: number): void {
    let item = this.todos.find((obj) => {
      return obj.id === id;
    });
    if(item.status == Status.Completed){
      item.status = Status.Open;
    }
    else{
      item.status = Status.Completed;
    }
  }

  deleteTodo(id: number): void {
    this.todoService
      .deleteTodo(id)
      .subscribe(({ response }) => this.todos = response);
  }

}
