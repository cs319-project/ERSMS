import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { Todo, HttpResponse } from './ToDo.interface';


@Injectable()
export class TodoService {

  constructor() {}

  addTodo(todo: Todo): Observable<HttpResponse> {
    const todos = this.getLocalTodos();
    const body  = JSON.stringify([...todos, todo]);

    localStorage.setItem('todos', body);

    return of({ response: 'Success', statusCode: 200 });
  }

  getLocalTodos(): Todo[] {
    return JSON.parse(localStorage.getItem('todos')) || [];
  }

  getTodos(): Observable<Todo[]> {
    const todos = this.getLocalTodos();
    return of(todos);
  }

  getNextId(): number {
    const todos = Array.from(this.getLocalTodos());
    const id    = todos.length ? todos.length : 0;

    return id;
  }

  deleteTodo(id: number): Observable<HttpResponse> {
    const todos   = this.getLocalTodos();
    const newList = todos.filter(item => item.id != id);
    const body    = JSON.stringify(newList); 

    localStorage.setItem('todos', body);

    return of({ response: newList, statusCode: 200 });
  }

}
