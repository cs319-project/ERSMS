import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { FormsAndRequestsComponent } from "./formsandrequests/formsandrequests.component";
import { LoggingComponent } from "./logging/logging.component";
import { MessagesComponent } from "./messages/messages.component";
import { PlacementComponent } from "./placement/placement.component";
import { ProfileComponent} from "./profile/profile.component";

const routes: Routes = [
  {path: 'dashboard', component: DashboardComponent},
{path: 'formsandrequests', component: FormsAndRequestsComponent},
{path: 'logging', component: LoggingComponent},
{path: 'messages', component: MessagesComponent},
{path: 'placements', component: PlacementComponent},
  {path: 'profile', component: ProfileComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
