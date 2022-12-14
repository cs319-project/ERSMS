import { NgModule } from "@angular/core";
import { DashboardComponent } from "./dashboard.component";
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from "@angular/platform-browser";

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
import {MatTableModule} from "@angular/material/table";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {MatOptionModule} from "@angular/material/core";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import {MatSortModule} from "@angular/material/sort";
import {MatSelectModule} from "@angular/material/select";



@NgModule({
  imports: [FormsModule, BrowserAnimationsModule, BrowserModule, MatGridListModule, MatCardModule, MatDividerModule, NgApexchartsModule, MatCheckboxModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatInputModule, MatTabsModule, MatTableModule, MatPaginatorModule, MatProgressBarModule, MatSortModule, MatOptionModule, MatAutocompleteModule, ReactiveFormsModule, MatSelectModule],
  exports: [],
  declarations: [ DashboardComponent, AdminDashboardComponent],
  providers: [],
})
export class DashboardModule {}
