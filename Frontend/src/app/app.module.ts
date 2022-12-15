import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavigationComponent } from './navigation/navigation.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatMenuModule } from '@angular/material/menu';
import { MatBadgeModule } from '@angular/material/badge';
import { DashboardModule } from './dashboard/dashboard.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { PlacementModule } from './placement/placement.module';
import { LoginComponent } from './login/login.component';
import { MatCardModule } from '@angular/material/card';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SignupComponent } from './signup/signup.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { MessagesComponent } from './messages/messages.component';
import { SharedModule } from './_modules/shared.module';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';

import { FormsAndRequestsComponent } from './formsandrequests/formsandrequests.component';
import { ProfileComponent } from './profile/profile.component';
import { LoggingModule } from './logging/logging.module';
import { FormDialogComponent } from './formsandrequests/form-dialog/form-dialog.component';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTableModule } from '@angular/material/table';
import { MatExpansionModule } from '@angular/material/expansion';
import { AnnouncementComponent } from './navigation/announcement/announcement.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { AppointmentsComponent } from './appointments/appointments.component';
import { ScoreTableUploadDialogComponent } from './dashboard/score-table-upload-dialog/score-table-upload-dialog.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { CreateAppointmentDialogComponent } from './appointments/create-appointment-dialog/create-appointment-dialog.component';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ConfirmationDialogComponent } from './appointments/confirmation-dialog/confirmation-dialog.component';
import { PreapprovalFormDialogComponent } from './formsandrequests/preapproval-form-dialog/preapproval-form-dialog.component';
import { EquivalenceRequestDialogComponent } from './formsandrequests/equivalence-request-dialog/equivalence-request-dialog.component';
import { CteFormDialogComponent } from './formsandrequests/cte-form-dialog/cte-form-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    LoginComponent,
    SignupComponent,
    FormsAndRequestsComponent,
    ForgotPasswordComponent,
    ProfileComponent,
    MessagesComponent,
    FormDialogComponent,
    AnnouncementComponent,
    AppointmentsComponent,
    CreateAppointmentDialogComponent,
    ConfirmationDialogComponent,
    ScoreTableUploadDialogComponent,
    PreapprovalFormDialogComponent,
    EquivalenceRequestDialogComponent,
    CteFormDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatToolbarModule,
    MatButtonModule,
    MatGridListModule,
    HttpClientModule,
    MatMenuModule,
    MatBadgeModule,
    DashboardModule,
    NgxChartsModule,
    PlacementModule,
    MatCheckboxModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    LoggingModule,
    MatTabsModule,
    MatTableModule,
    MatExpansionModule,
    MatDialogModule,
    MatOptionModule,
    MatSelectModule,
    MatPaginatorModule,
    MatSortModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
