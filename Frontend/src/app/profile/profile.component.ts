import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { Student } from '../_models/student';
import { ScoreTableUploadDialogComponent } from '../dashboard/score-table-upload-dialog/score-table-upload-dialog.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfigurableFocusTrap } from '@angular/cdk/a11y';
import { ConfirmationDialogComponent } from '../appointments/confirmation-dialog/confirmation-dialog.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  userName: string;
  actorType: string;
  fullName: string;
  department: string;
  email: string;
  cgpa: number;
  preferredSemester: string;
  preferredSchools: string;
  exchangeSchool: string;
  entranceYear: number;
  otherStudents: any[];

  constructor(
    private userService: UserService,
    private dialog: MatDialog,
    private toaster: ToastrService
  ) {
    const user = JSON.parse(localStorage.getItem('user'));
    this.userName = user.userName;
    this.actorType = user.userDetails.actorType;
    this.fullName = `${user.userDetails.firstName} ${user.userDetails.lastName}`;
    this.department = user.userDetails.major.departmentName;
    this.email = user.userDetails.identityUser.email;
    this.cgpa = user.userDetails.cgpa / 100;
    this.preferredSemester = `${user.userDetails.preferredSemester.academicYear} ${user.userDetails.preferredSemester.semester}`;
    this.preferredSchools = user.userDetails.preferredSchools.join(', ');
    this.entranceYear = user.userDetails.entranceYear;
    this.exchangeSchool = user.userDetails.exchangeSchool
      ? user.userDetails.exchangeSchool
      : null;
    this.otherStudents = [];
    userService.getSameSchoolStudents(this.userName).subscribe(data => {
      data.forEach(other => {
        if (user.userName !== other.identityUser.userName) {
          const temp = {
            fullName: `${other.firstName} ${other.lastName}`,
            department: other.major.departmentName,
            email: other.identityUser.email
          };
          this.otherStudents.push(temp);
        }
      });
    });
    console.log(!this.otherStudents);
  }

  ngOnInit(): void {}

  cancelApplication() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      text: `Are you sure you want to cancel your application?`
    };
    const dialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      dialogConfig
    );

    dialogRef.afterClosed().subscribe(cancelConfirm => {
      if (cancelConfirm) {
        this.userService.cancelPlacedStudent(this.userName).subscribe(
          data => {
            const user = JSON.parse(localStorage.getItem('user'));
            user.userDetails.exchangeSchool = null;
            this.exchangeSchool = null;
            localStorage.setItem('user', JSON.stringify(user));

            this.toaster.success('Your application is canceled.');
          },
          error => {
            this.toaster.error(
              'An error happened during canceling application.'
            );
          }
        );
      }
    });
  }
}
