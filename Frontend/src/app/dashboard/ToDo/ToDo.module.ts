import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';



import { TodoComponent } from './containers/todo/todo.component';
import { TodoListComponent } from './components/todolist/todolist.component';
import { TodoFormComponent } from './components/todoform/todoform.component';

import { TodoService } from './todo.service';


@NgModule({
  imports: [ CommonModule, FormsModule],
  declarations: [ TodoComponent, TodoListComponent, TodoFormComponent],
  providers: [ TodoService ],
  exports: [ TodoComponent ],
})
export class TodoModule {}
