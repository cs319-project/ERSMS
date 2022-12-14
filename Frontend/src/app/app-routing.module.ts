import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FormsAndRequestsComponent } from './formsandrequests/formsandrequests.component';
import { LoggingComponent } from './logging/logging.component';
import { MessagesComponent } from './messages/messages.component';
import { PlacementComponent } from './placement/placement.component';
import { ProfileComponent } from './profile/profile.component';
import { AppointmentsComponent } from './appointments/appointments.component';
import { LoginComponent } from './login/login.component';
import { NavigationComponent } from './navigation/navigation.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', component: NavigationComponent, canActivate: [AuthGuard] }
  //{ path: '**', redirectTo: '' }
  // {
  //   path: '',
  //   runGuardsAndResolvers: 'always',
  //   canActivate: [AuthGuard],
  //   children: [{ path: 'dashboard', component: DashboardComponent }]
  // }
  // { path: 'appointments', component: AppointmentsComponent },
  // { path: 'dashboard', component: DashboardComponent },
  // { path: 'formsandrequests', component: FormsAndRequestsComponent },
  // { path: 'logging', component: LoggingComponent },
  // { path: 'messages', component: MessagesComponent },
  // { path: 'placements', component: PlacementComponent },
  // { path: 'profile', component: ProfileComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
