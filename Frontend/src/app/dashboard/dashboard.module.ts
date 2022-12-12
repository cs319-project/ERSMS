import { NgModule } from "@angular/core";
import { DashboardComponent } from "./dashboard.component";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PieChartComponent } from "./pieChart/piechart.component";
import { ColumnChartComponent } from "./submittedFormColumnChart/columnchart.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from "@angular/platform-browser";

import { NgxChartsModule } from '@swimlane/ngx-charts';
import { TodoModule } from "./ToDo/ToDo.module";
import {MatGridListModule} from "@angular/material/grid-list";
import {MatCardModule} from "@angular/material/card";
import {MatDividerModule} from "@angular/material/divider";
import {NgApexchartsModule} from "ng-apexcharts";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatTabsModule} from "@angular/material/tabs";


@NgModule({
  imports: [ReactiveFormsModule, FormsModule, BrowserAnimationsModule, BrowserModule, NgxChartsModule, TodoModule, MatGridListModule, MatCardModule, MatDividerModule, NgApexchartsModule, MatCheckboxModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatInputModule, MatTabsModule],
    exports: [PieChartComponent, ColumnChartComponent],
    declarations: [ DashboardComponent, PieChartComponent, ColumnChartComponent],
    providers: [],

})
export class DashboardModule {}
