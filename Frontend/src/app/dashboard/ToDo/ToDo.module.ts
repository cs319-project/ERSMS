import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TodoComponent } from './containers/todo/todo.component';
import { TodoListComponent } from './components/todolist/todolist.component';
import { TodoFormComponent } from './components/todoform/todoform.component';

import { TodoService } from './todo.service';
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatListModule} from "@angular/material/list";


@NgModule({
  imports: [CommonModule, FormsModule, MatIconModule, MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule, MatListModule],
  declarations: [ TodoComponent, TodoListComponent, TodoFormComponent],
  providers: [ TodoService ],
  exports: [ TodoComponent ],
})
export class TodoModule {}
