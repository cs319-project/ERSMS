import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {CteForm} from "../../_models/cte-form";
import {RequestedCourseGroup} from "../../_models/requested-course-group";
import {TransferredCourseGroup} from "../../_models/transferred-course-group";
import {TransferredCourse} from "../../_models/transferred-course";
import {FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-cte-form-dialog',
  templateUrl: './cte-form-dialog.component.html',
  styleUrls: ['./cte-form-dialog.component.css']
})
export class CteFormDialogComponent implements OnInit {

  error = true;
  submitted = false;
  studentid = new FormControl('', [Validators.required]);
  courseCredit = new FormControl('', [Validators.required]);
  courseCode = new FormControl('', [Validators.required]);
  courseName = new FormControl('', [Validators.required]);
  grade = new FormControl('', [Validators.required]);
  courseType = new FormControl('', [Validators.required]);

  courseCreditBilkent = new FormControl('', [Validators.required]);
  courseCodeBilkent = new FormControl('', [Validators.required]);
  courseNameBilkent = new FormControl('', [Validators.required]);

  getErrorMessageEmpty() {
    return this.studentid.hasError('required') ? 'All fields must be filled' :
      this.courseCredit.hasError('required') ? 'All fields must be filled' :
      this.courseCode.hasError('required') ? 'All fields must be filled' :
      this.courseName.hasError('required') ? 'All fields must be filled' :
      this.grade.hasError('required') ? 'All fields must be filled' :
            '';
  }

  getErrorMessageEmptyBilkent() {

    return this.courseCreditBilkent.hasError('required') ? 'All fields must be filled' :
      this.courseCodeBilkent.hasError('required') ? 'All fields must be filled' :
      this.courseNameBilkent.hasError('required') ? 'All fields must be filled' :
      this.courseType.hasError('required') ? 'All fields must be filled' :
            '';

  }

  courseTypes: string[] = ["Mandatory Course", "Techincal Elective", "General Elective",
  "Project Elective", "Social Science Core Elective", "Arts Core Elective", "Additional Course"];

  constructor(    public dialogRef: MatDialogRef<CteFormDialogComponent>,
                  @Inject(MAT_DIALOG_DATA) public data: CteForm) { }

  ngOnInit(): void {
  }

  onAddGroup() {
    let newGroup: TransferredCourseGroup = {id: null,
      transferredCourses: [{id:null, courseCode: null, courseName:null,credits:null, grade:null}],
      exemptedCourse: {id: null, courseCode: null, courseName: null, courseType: null, credits: null}};
    if(this.data.transferredCourseGroup){
      this.data.transferredCourseGroup.push(newGroup);
    }
    else{
      this.data.transferredCourseGroup = [newGroup];
    }
  }

  onAddCourse(courseGroup: TransferredCourseGroup) {

    let newRequestedCourse: TransferredCourse = {id: null, courseCode: null, courseName: null, credits: null, grade: null};
    courseGroup.transferredCourses.push(newRequestedCourse);
  }

  onGroupDelete(groupIndex: number) {
    this.data.transferredCourseGroup.splice(groupIndex, 1);
  }

  onCourseDelete(groupIndex: number, courseIndex: number) {
    this.data.transferredCourseGroup[groupIndex].transferredCourses.splice(courseIndex,1);
  }

  onSubmit() {

    this.submitted = true;
    this.error = (this.courseCredit.hasError('required') || this.studentid.hasError('required') || this.courseCode.hasError('required') ||
    this.courseName.hasError('required') || this.grade.hasError('required') || this.courseCreditBilkent.hasError('required') || this.courseCodeBilkent.hasError('required') || this.courseNameBilkent.hasError('required') ||
    this.courseType.hasError('required'));

    if(!this.error) {
      console.log(this.data);
      this.dialogRef.close(this.data);
    }
  }
}
