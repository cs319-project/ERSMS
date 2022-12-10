import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NavigationComponent } from './navigation/navigation.component';
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatListModule} from "@angular/material/list";
import {MatIconModule} from "@angular/material/icon";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatButtonModule} from "@angular/material/button";
import {MatGridListModule} from "@angular/material/grid-list";
import {HttpClientModule} from "@angular/common/http";
import {MatMenuModule} from "@angular/material/menu";
import {MatBadgeModule} from "@angular/material/badge";
import { DashboardModule } from "./dashboard/dashboard.module";
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { MatCheckboxModule} from "@angular/material/checkbox";
import { PlacementModule } from "./placement/placement.module";
import { LoginComponent } from './login/login.component';
import {MatCardModule} from "@angular/material/card";
import {FormsModule} from "@angular/forms";
import {ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import { SignupComponent } from './signup/signup.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';

import {FormsAndRequestsComponent} from "./formsandrequests/formsandrequests.component";
import { ProfileComponent } from './profile/profile.component';
import { LoggingModule } from "./logging/logging.module";

@NgModule({
  declarations: [AppComponent, NavigationComponent, LoginComponent, SignupComponent, FormsAndRequestsComponent, ForgotPasswordComponent, ProfileComponent],
  imports: [BrowserModule, AppRoutingModule, BrowserAnimationsModule, MatSidenavModule, MatListModule, MatIconModule, MatToolbarModule, MatButtonModule, MatGridListModule, HttpClientModule, MatMenuModule, MatBadgeModule, DashboardModule, NgxChartsModule,
    PlacementModule, MatCheckboxModule, MatCardModule, FormsModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, LoggingModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
