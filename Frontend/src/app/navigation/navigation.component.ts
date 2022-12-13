import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { MatIconRegistry} from "@angular/material/icon";
import { DomSanitizer} from "@angular/platform-browser";
import {AppScene} from "../app.component";
import {Router} from "@angular/router";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";
import {AnnouncementComponent} from "./announcement/announcement.component";
import {createRandomDialogData} from "../logging/logging.component";

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  @Input() currentScene!: AppScene;
  @Output() currentSceneChange: EventEmitter<AppScene> = new EventEmitter<AppScene>();

  announcement: string = "";
  name :string  = "Kutay Tire";

  notifications :  {message: String, timeSent:String}[] = [
    {message : "Your Pre-Approval Form Has Been Approved!", timeSent: "3 mins ago"},
    {message : "Talay has sent you a message", timeSent: "7 mins ago"},
  ];
  constructor(    private matIconRegistry: MatIconRegistry,
                  private domSanitizer: DomSanitizer,
                  private router: Router,
                  private dialog: MatDialog,
                  private _snackBar: MatSnackBar
  ) {
    this.matIconRegistry.addSvgIcon(
      `dashboard`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/dashboard.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `forms`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/forms.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `logging`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/logging.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `placements`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/placements.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `messages`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/messages.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `notification-idle`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/notification_idle.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `settings`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/settings.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `mail`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/mail.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `edit`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/edit.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `calendar`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/calendar.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `plus`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/plus.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `send`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/send.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `attachment`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/attachment.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `emoji`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/emoji.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `delete`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/delete.svg")
    );
  }

  ngOnInit(): void {
  }

  logout() {
    this.currentScene = AppScene.Login;
    this.router.navigate([`../`]);
    this.currentSceneChange.emit(this.currentScene);
  }

  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = { data: { description: this.announcement } };
    dialogConfig.disableClose = true;

    const dialogRef = this.dialog.open(AnnouncementComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.openSnackBar("Announcement sent", 'Close', 5);
        console.log(result);
      }
    });

  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }
}
