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

@Component({
  selector: 'app-cte-form-dialog',
  templateUrl: './cte-form-dialog.component.html',
  styleUrls: ['./cte-form-dialog.component.css']
})
export class CteFormDialogComponent implements OnInit {
  
  courseTypes: string[] = [
    'Mandatory Course',
    'Techincal Elective',
    'General Elective',
    'Project Elective',
    'Social Science Core Elective',
    'Arts Core Elective',
    'Additional Course'
  ];

  constructor(
    private _snackBar: MatSnackBar,
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
    console.log(courseGroup);
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
    this.userService.getUserDetails(this.data.idNumber).subscribe(
      result => {
        if (result && result.actorType == ActorsEnum.Student) {
          // console.log(result);
          this.data.hostUniversityName = result.exchangeSchool;
          this.data.firstName = result.firstName;
          this.data.lastName = result.lastName;
          this.data.department = result.major.departmentName;
          this.data.submissionTime = new Date();
          this.cteFormService.createCTEForm(this.data).subscribe(
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

  }
}
