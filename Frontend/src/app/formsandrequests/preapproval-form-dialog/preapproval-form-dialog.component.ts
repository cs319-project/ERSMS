import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { RequestedCourseGroup } from '../../_models/requested-course-group';
import { PreApprovalForm } from '../../_models/pre-approval-form';
import { GUID } from '../../../utils/guid';
import { RequestedCourse } from '../../_models/requested-course';
import { TransferredCourseGroup } from '../../_models/transferred-course-group';
import { FormControl, Validators } from '@angular/forms';

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
    public dialogRef: MatDialogRef<PreapprovalFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PreApprovalForm
  ) {}

  ngOnInit(): void {}

  save_and_close(signed: boolean) {
    this.dialogRef.close(signed);
  }

  onAddGroup() {
    let newGroup: RequestedCourseGroup = {
      id: null,
      requestedCourses: [
        { id: null, courseCode: null, courseName: null, ects: null }
      ],
      requestedExemptedCourse: {
        id: null,
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
      id: null,
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
