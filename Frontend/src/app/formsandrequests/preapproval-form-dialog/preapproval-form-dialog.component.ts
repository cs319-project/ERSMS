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
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-preapproval-form-dialog',
  templateUrl: './preapproval-form-dialog.component.html',
  styleUrls: ['./preapproval-form-dialog.component.css']
})
export class PreapprovalFormDialogComponent implements OnInit {
  userName: string;

  courseTypes: string[] = Object.values(CourseType);

  constructor(
    private toastr: ToastrService,
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
          if (
            this.data.requestedCourseGroups == null ||
            this.data.requestedCourseGroups.length == 0
          ) {
            this.toastr.error('Please add at least one course group');
            return;
          }
          this.data.requestedCourseGroups.forEach(group => {
            if (group.requestedExemptedCourse.courseType == null) {
              this.toastr.error('Please select a course type');
              return;
            }
            if (
              group.requestedExemptedCourse.courseType == 'Additional Course'
            ) {
              group.requestedExemptedCourse.ects = 0;
              group.requestedExemptedCourse.bilkentCredits = 0;
            }
            if (
              group.requestedExemptedCourse.courseType == 'Mandatory Course' &&
              (group.requestedExemptedCourse.courseCode == null ||
                group.requestedExemptedCourse.courseCode == '' ||
                group.requestedExemptedCourse.courseName == null ||
                group.requestedExemptedCourse.courseName == '')
            ) {
              this.toastr.error(
                'Please enter a course code and course name for exempted course'
              );
              return;
            } else if (
              group.requestedExemptedCourse.courseType != 'Mandatory Course' &&
              group.requestedExemptedCourse.courseCode != null &&
              group.requestedExemptedCourse.courseCode != ''
            ) {
              group.requestedExemptedCourse.courseCode =
                group.requestedExemptedCourse.courseCode
                  .replace(/[^a-z0-9]/gi, '')
                  .toLocaleUpperCase();
            } else if (
              group.requestedExemptedCourse.courseType == 'Mandatory Course'
            ) {
              group.requestedExemptedCourse.courseCode =
                group.requestedExemptedCourse.courseCode
                  .replace(/[^a-z0-9]/gi, '')
                  .toLocaleUpperCase();
            }
            if (
              group.requestedExemptedCourse.courseType != 'Additional Course' &&
              (group.requestedExemptedCourse.bilkentCredits == null ||
                group.requestedExemptedCourse.bilkentCredits == 0)
            ) {
              this.toastr.error('Please enter Bilkent Credits');
              return;
            }
            if (
              group.requestedExemptedCourse.courseType != 'Additional Course' &&
              (group.requestedExemptedCourse.ects == null ||
                group.requestedExemptedCourse.ects == 0)
            ) {
              this.toastr.error('Please enter ECTS for the exempted course');
              return;
            }

            group.requestedCourses.forEach(requestedCourse => {
              if (
                requestedCourse.courseCode == null ||
                requestedCourse.courseCode == ''
              ) {
                this.toastr.error(
                  'Please enter a course code for host university'
                );
                return;
              }
              if (
                requestedCourse.courseName == null ||
                requestedCourse.courseName == ''
              ) {
                this.toastr.error(
                  'Please enter a course name for host university'
                );
                return;
              }

              if (requestedCourse.ects == null || requestedCourse.ects == 0) {
                this.toastr.error('Please enter ECTS');
                return;
              }
            });
          });

          this.data.hostUniversityName = result.exchangeSchool;
          this.data.firstName = result.firstName;
          this.data.lastName = result.lastName;
          this.data.department = result.major.departmentName;
          this.data.academicYear = result.preferredSemester.academicYear;
          this.data.semester = result.preferredSemester.semester;
          // this.data.requestedCourseGroups.forEach(group => {
          //   group.requestedExemptedCourse.courseCode =
          //     group.requestedExemptedCourse.courseCode
          //       .replace(/[^a-z0-9]/gi, '')
          //       .toLocaleUpperCase();
          // });
          console.log(this.data);
          this.preApprovalFormService
            .createPreApprovalForm(this.data)
            .subscribe(
              res => {
                if (res) {
                  this.toastr.success('Form is submitted successfully');
                  this.dialogRef.close(this.data);
                }
              },
              error => {
                this.toastr.error('An error occured while submitting the form');
              }
            );
        } else {
          this.toastr.error('No student with ID ' + this.data.idNumber);
        }
      },
      error => {
        this.toastr.error('No student with ID ' + this.data.idNumber);
      }
    );
  }
}
