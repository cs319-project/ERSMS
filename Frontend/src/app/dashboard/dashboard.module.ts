import { NgModule } from "@angular/core";
import { DashboardComponent } from "./dashboard.component";
import { TodoComponent } from "./ToDoList/todolist.component";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [ReactiveFormsModule,FormsModule],
    exports: [TodoComponent],
    declarations: [TodoComponent, DashboardComponent],
    providers: [],
  
})
export class DashboardModule {}
