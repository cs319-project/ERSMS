import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { AppScene } from '../app.component';
import { Router } from '@angular/router';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AnnouncementComponent } from './announcement/announcement.component';
import { AuthenticationService } from '../_services/authentication.service';
import { UserService } from '../_services/user.service';
import { ActorsEnum } from '../_models/enum/actors-enum';
import { AnnouncementService } from '../_services/announcement.service';
import { Announcement } from '../_models/announcement';
import { GUID } from 'src/utils/guid';
import { formatDate } from '@angular/common';
import { NotificationService } from '../_services/notification.service';
import { NotificationERSMS } from '../_models/notification';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  actorsEnum = ActorsEnum;
  announcement: string;
  name: string;
  role: string;
  unreadCount: number = 0;
  userName: string;
  nameOfLoggedInUser: string;

  notifications: {
    content: String;
    read: boolean;
    userId: GUID;
    // timeSent: String;
    // isAnnouncement: boolean;
    id: GUID;
  }[] = [];

  // = [
  //   {
  //     message: 'Your Pre-Approval Form Has Been Approved!',
  //     timeSent: '3 mins ago'
  //   },
  //   { message: 'Talay has sent you a message', timeSent: '7 mins ago' }
  // ]

  constructor(
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer,
    private router: Router,
    private dialog: MatDialog,
    private _snackBar: MatSnackBar,
    public authenticationService: AuthenticationService,
    public userService: UserService,
    private announcementService: AnnouncementService,
    private notificationService: NotificationService
  ) {
    this.authenticationService.currentUser$.subscribe(user => {
      this.role = user.roles[0];
      this.userName = user.userName;
      this.name = user.userDetails.firstName + ' ' + user.userDetails.lastName;
    });

    this.nameOfLoggedInUser =
      JSON.parse(localStorage.getItem('user')).userDetails.firstName +
      ' ' +
      JSON.parse(localStorage.getItem('user')).userDetails.lastName;

    this.router.navigate(['/dashboard']);

    this.matIconRegistry.addSvgIcon(
      `dashboard`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/dashboard.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `forms`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/forms.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `logging`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/logging.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `placements`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/placements.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `messages`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/messages.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `notification-idle`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/notification_idle.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `settings`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/settings.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `mail`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/mail.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `edit`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/edit.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `calendar`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/calendar.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `plus`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/plus.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `send`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/send.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `attachment`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/attachment.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `emoji`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/emoji.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      `delete`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/delete.svg'
      )
    );
    this.populateNotifications();
  }

  ngOnInit(): void {
    //this.populateNotifications();
  }

  readNotification(notification) {
    if (!notification.isAnnouncement) {
      this.notificationService.readNotification(notification.id);
    }
  }

  populateNotifications() {
    let notification: NotificationERSMS;
    this.notifications = [];
    this.unreadCount = 0;
    this.notificationService
      .getNotifications(this.userName)
      .subscribe(result => {
        result.forEach(element => {
          console.log(notification);
          this.notifications.push({
            read: element.read,
            content: element.content,
            id: element.id,
            userId: element.userId
          });
          this.unreadCount = this.notifications.length;
        });
      });
    // this.announcementService.getAllAnnouncements().subscribe(result => {
    //   announcements = result;
    //   //console.log(announcements);

    //   const format = 'dd/MM/yyyy h:mm';
    //   const locale = 'en-TR';

    //   announcements.forEach(element => {
    //     const formattedDate = formatDate(element.creationDate, format, locale);
    //     this.notifications.push({
    //       message: element.description,
    //       timeSent: formattedDate,
    //       isAnnouncement: true,
    //       id: element.id
    //     });
    //   });
    //   this.unreadCount = this.notifications.length;
    // });
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }

  readAllNotifications() {
    this.notificationService.readAllNotifications(this.userName).subscribe();
    this.unreadCount = 0;
  }

  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = { data: { description: this.announcement } };
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    let returnedDescription: string;

    const dialogRef = this.dialog.open(AnnouncementComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result);
        this.openSnackBar('Announcement sent', 'Close', 5);
        //console.log(result);
        let announcement: Announcement = {
          description: result,
          title: 'Announcement',
          sender: this.nameOfLoggedInUser
        };
        this.announcementService.createAnnouncement(announcement).subscribe();
      }
    });
  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }
}
