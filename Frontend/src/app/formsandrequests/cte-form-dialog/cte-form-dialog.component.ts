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

  courseCredit = new FormControl('', [Validators.required, Validators.pattern("[0-9]+")]);

  getErrorMessage() {
    return this.courseCredit.hasError('required') ? 'You must enter a value' :
        this.courseCredit.hasError('pattern') ? 'Only numbers allowed' :
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
    console.log(this.data);
    this.dialogRef.close(this.data);
  }
}
