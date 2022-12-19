import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CteForm } from '../../_models/cte-form';
import { RequestedCourseGroup } from '../../_models/requested-course-group';
import { TransferredCourseGroup } from '../../_models/transferred-course-group';
import { TransferredCourse } from '../../_models/transferred-course';
import { FormControl, Validators } from '@angular/forms';
import { CTEFormService } from '../../_services/cteform.service';
import { UserService } from '../../_services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActorsEnum } from '../../_models/enum/actors-enum';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cte-form-dialog',
  templateUrl: './cte-form-dialog.component.html',
  styleUrls: ['./cte-form-dialog.component.css']
})
export class CteFormDialogComponent implements OnInit {
  courseTypes: string[] = [
    'Mandatory Course',
    'Technical Elective',
    'General Elective',
    'Project Elective',
    'Social Science Core Elective',
    'Arts Core Elective',
    'Additional Course'
  ];

  constructor(
    private toastr: ToastrService,
    private userService: UserService,
    private cteFormService: CTEFormService,
    public dialogRef: MatDialogRef<CteFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CteForm
  ) {}

  ngOnInit(): void {}

  onAddGroup() {
    let newGroup: TransferredCourseGroup = {
      transferredCourses: [
        {
          courseCode: null,
          courseName: null,
          ects: null,
          grade: null
        }
      ],
      exemptedCourse: {
        courseCode: null,
        courseName: null,
        courseType: null,
        ects: null,
        bilkentCredits: null
      }
    };
    if (this.data.transferredCourseGroups) {
      this.data.transferredCourseGroups.push(newGroup);
    } else {
      this.data.transferredCourseGroups = [newGroup];
    }
  }

  onAddCourse(courseGroup: TransferredCourseGroup) {
    // console.log(courseGroup);
    let newRequestedCourse: TransferredCourse = {
      courseCode: null,
      courseName: null,
      ects: null,
      grade: null
    };
    courseGroup.transferredCourses.push(newRequestedCourse);
  }

  onGroupDelete(groupIndex: number) {
    this.data.transferredCourseGroups.splice(groupIndex, 1);
  }

  onCourseDelete(groupIndex: number, courseIndex: number) {
    this.data.transferredCourseGroups[groupIndex].transferredCourses.splice(
      courseIndex,
      1
    );
  }

  onSubmit() {
    //TODO Handle Empty Forms
    if (this.data.idNumber == null || this.data.idNumber == '') {
      this.toastr.error('Please enter an ID number');
      return;
    }
    this.userService.getUserDetails(this.data.idNumber).subscribe(
      result => {
        if (result && result.actorType == ActorsEnum.Student) {
          if (
            this.data.transferredCourseGroups == null ||
            this.data.transferredCourseGroups.length == 0
          ) {
            this.toastr.error('Please add at least one course group');
            return;
          }
          this.data.transferredCourseGroups.forEach(group => {
            if (group.exemptedCourse.courseType == null) {
              this.toastr.error('Please select a course type');
              return;
            }
            if (group.exemptedCourse.courseType == 'Additional Course') {
              group.exemptedCourse.ects = 0;
              group.exemptedCourse.bilkentCredits = 0;
            }
            if (
              group.exemptedCourse.courseType == 'Mandatory Course' &&
              (group.exemptedCourse.courseCode == null ||
                group.exemptedCourse.courseCode == '' ||
                group.exemptedCourse.courseName == null ||
                group.exemptedCourse.courseName == '')
            ) {
              this.toastr.error(
                'Please enter a course code and course name for exempted course'
              );
              return;
            } else if (
              group.exemptedCourse.courseType != 'Mandatory Course' &&
              group.exemptedCourse.courseCode != null &&
              group.exemptedCourse.courseCode != ''
            ) {
              group.exemptedCourse.courseCode = group.exemptedCourse.courseCode
                .replace(/[^a-z0-9]/gi, '')
                .toLocaleUpperCase();
            } else if (group.exemptedCourse.courseType == 'Mandatory Course') {
              group.exemptedCourse.courseCode = group.exemptedCourse.courseCode
                .replace(/[^a-z0-9]/gi, '')
                .toLocaleUpperCase();
            }
            if (
              group.exemptedCourse.courseType != 'Additional Course' &&
              (group.exemptedCourse.bilkentCredits == null ||
                group.exemptedCourse.bilkentCredits == 0)
            ) {
              this.toastr.error('Please enter Bilkent Credits');
              return;
            }
            if (
              group.exemptedCourse.courseType != 'Additional Course' &&
              (group.exemptedCourse.ects == null ||
                group.exemptedCourse.ects == 0)
            ) {
              this.toastr.error('Please enter ECTS for the exempted course');
              return;
            }

            group.transferredCourses.forEach(transferredCourse => {
              if (
                transferredCourse.courseCode == null ||
                transferredCourse.courseCode == ''
              ) {
                this.toastr.error(
                  'Please enter a course code for host university'
                );
                return;
              }
              if (
                transferredCourse.courseName == null ||
                transferredCourse.courseName == ''
              ) {
                this.toastr.error(
                  'Please enter a course name for host university'
                );
                return;
              }

              if (
                transferredCourse.ects == null ||
                transferredCourse.ects == 0
              ) {
                this.toastr.error('Please enter ECTS');
                return;
              }
              if (
                transferredCourse.grade == null ||
                transferredCourse.grade == ''
              ) {
                this.toastr.error('Please enter a grade');
                return;
              }
            });
          });
          // console.log(result);
          this.data.hostUniversityName = result.exchangeSchool;
          this.data.firstName = result.firstName;
          this.data.lastName = result.lastName;
          this.data.department = result.major.departmentName;
          // this.data.transferredCourseGroups.forEach(group => {
          //   group.exemptedCourse.courseCode = group.exemptedCourse.courseCode
          //     .replace(/[^a-z0-9]/gi, '')
          //     .toLocaleUpperCase();
          // });
          this.cteFormService.createCTEForm(this.data).subscribe(
            res => {
              if (res) {
                this.toastr.success('Form is submitted');
                this.dialogRef.close();
              }
            },
            error => {
              this.toastr.error('An Error occured while submitting');
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
