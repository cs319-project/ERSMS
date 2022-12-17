import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { RequestedCourseGroup } from '../../_models/requested-course-group';
import { PreApprovalForm } from '../../_models/pre-approval-form';
import { GUID } from '../../../utils/guid';
import { RequestedCourse } from '../../_models/requested-course';
import { TransferredCourseGroup } from '../../_models/transferred-course-group';
import { FormControl, Validators } from '@angular/forms';
import { ActorsEnum } from '../../_models/enum/actors-enum';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from '../../_services/user.service';
import { PreApprovalFormService } from '../../_services/preapprovalform.service';
import { CourseType } from 'src/app/_models/enum/course-type-enum';

@Component({
  selector: 'app-preapproval-form-dialog',
  templateUrl: './preapproval-form-dialog.component.html',
  styleUrls: ['./preapproval-form-dialog.component.css']
})
export class PreapprovalFormDialogComponent implements OnInit {
  error = true;
  submitted = false;

  courseCredit = new FormControl('', [Validators.required]);
  courseCode = new FormControl('', [Validators.required]);
  courseName = new FormControl('', [Validators.required]);
  courseType = new FormControl('', [Validators.required]);

  courseCreditBilkent = new FormControl('', [Validators.required]);
  courseCodeBilkent = new FormControl('', [Validators.required]);
  courseNameBilkent = new FormControl('', [Validators.required]);

  userName: string;

  getErrorMessageEmpty() {
    return this.courseCredit.hasError('required')
      ? 'All fields must be filled'
      : this.courseCode.hasError('required')
      ? 'All fields must be filled'
      : this.courseName.hasError('required')
      ? 'All fields must be filled'
      : '';
  }

  getErrorMessageEmptyBilkent() {
    return this.courseCreditBilkent.hasError('required')
      ? 'All fields must be filled'
      : this.courseCodeBilkent.hasError('required')
      ? 'All fields must be filled'
      : this.courseNameBilkent.hasError('required')
      ? 'All fields must be filled'
      : this.courseType.hasError('required')
      ? 'All fields must be filled'
      : '';
  }

  courseTypes: string[] = Object.values(CourseType);

  constructor(
    private _snackBar: MatSnackBar,
    private userService: UserService,
    private preApprovalFormService: PreApprovalFormService,
    public dialogRef: MatDialogRef<PreapprovalFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PreApprovalForm
  ) {
    this.userName = JSON.parse(localStorage.getItem('user')).userName;
    this.data.idNumber = this.userName;
  }

  ngOnInit(): void {}

  save_and_close(signed: boolean) {
    this.dialogRef.close(signed);
  }

  onAddGroup() {
    let newGroup: RequestedCourseGroup = {
      requestedCourses: [{ courseCode: null, courseName: null, ects: null }],
      requestedExemptedCourse: {
        courseCode: null,
        courseName: null,
        courseType: null,
        ects: null,
        bilkentCredits: null
      }
    };

    for (let i = 0; i < newGroup.requestedCourses.length; i++) {
      this.courseCredit = new FormControl('', [Validators.required]);
      this.courseCode = new FormControl('', [Validators.required]);
      this.courseName = new FormControl('', [Validators.required]);
      this.courseType = new FormControl('', [Validators.required]);
    }

    if (this.data.requestedCourseGroups) {
      this.data.requestedCourseGroups.push(newGroup);
    } else {
      this.data.requestedCourseGroups = [newGroup];
    }
  }

  onAddCourse(courseGroup: RequestedCourseGroup) {
    let newRequestedCourse: RequestedCourse = {
      courseCode: null,
      courseName: null,
      ects: null
    };
    courseGroup.requestedCourses.push(newRequestedCourse);
  }

  onGroupDelete(groupIndex: number) {
    this.data.requestedCourseGroups.splice(groupIndex, 1);
  }

  onCourseDelete(groupIndex: number, courseIndex: number) {
    this.data.requestedCourseGroups[groupIndex].requestedCourses.splice(
      courseIndex,
      1
    );
  }

  onSubmit() {
    this.userService.getUserDetails(this.data.idNumber).subscribe(
      result => {
        if (result && result.actorType == ActorsEnum.Student) {
          this.data.hostUniversityName = result.exchangeSchool;
          this.data.firstName = result.firstName;
          this.data.lastName = result.lastName;
          this.data.department = result.major.departmentName;
          this.data.academicYear = result.preferredSemester.academicYear;
          this.data.semester = result.preferredSemester.semester;
          this.data.submissionTime = new Date();
          console.log(this.data);
          this.preApprovalFormService
            .createPreApprovalForm(this.data)
            .subscribe(
              res => {
                if (res) {
                  this._snackBar.open('Form is submitted', 'Close', {
                    duration: 3000
                  });
                  this.dialogRef.close();
                }
              },
              error => {
                this._snackBar.open(
                  'An Error occured while submitting',
                  'Close',
                  {
                    duration: 3000
                  }
                );
              }
            );
        } else {
          this._snackBar.open(
            'No student with ID ' + this.data.idNumber,
            'Close',
            {
              duration: 3000
            }
          );
        }
      },
      error => {
        this._snackBar.open(
          'No student with ID ' + this.data.idNumber,
          'Close',
          {
            duration: 3000
          }
        );
      }
    );

    this.submitted = true;
    this.error =
      this.courseCredit.hasError('required') ||
      this.courseCode.hasError('required') ||
      this.courseName.hasError('required') ||
      this.courseCreditBilkent.hasError('required') ||
      this.courseCodeBilkent.hasError('required') ||
      this.courseNameBilkent.hasError('required') ||
      this.courseType.hasError('required');

    if (!this.error) {
      console.log(this.data);
      this.dialogRef.close(this.data);
    }
  }
}
