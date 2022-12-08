import { NgModule } from "@angular/core";
import { DashboardComponent } from "./dashboard.component";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PieChartComponent } from "./pieChart/piechart.component";
import { ColumnChartComponent } from "./submittedFormColumnChart/columnchart.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from "@angular/platform-browser";

import { NgxChartsModule } from '@swimlane/ngx-charts';
import { TodoModule } from "./ToDo/ToDo.module";


@NgModule({
    imports: [ReactiveFormsModule,FormsModule, BrowserAnimationsModule, BrowserModule, NgxChartsModule, TodoModule],
    exports: [PieChartComponent, ColumnChartComponent],
    declarations: [ DashboardComponent, PieChartComponent, ColumnChartComponent],
    providers: [],
  
})
export class DashboardModule {}
