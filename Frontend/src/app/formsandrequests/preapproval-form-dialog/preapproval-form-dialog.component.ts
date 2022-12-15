import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {RequestedCourseGroup} from "../../_models/requested-course-group";
import {PreApprovalForm} from "../../_models/pre-approval-form";
import {GUID} from "../../../utils/guid";
import {RequestedCourse} from "../../_models/requested-course";

@Component({
  selector: 'app-preapproval-form-dialog',
  templateUrl: './preapproval-form-dialog.component.html',
  styleUrls: ['./preapproval-form-dialog.component.css']
})
export class PreapprovalFormDialogComponent implements OnInit {
  requestedCourseGroups: RequestedCourseGroup[] = [];
  courseTypes: string[] = ["Mandatory", "Techincal Elective", "General Elective"];
  courseType: string;

  constructor(
    public dialogRef: MatDialogRef<PreapprovalFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PreApprovalForm
  ) {

  }

  ngOnInit(): void {
  }

  save_and_close(signed: boolean) {
    this.dialogRef.close(signed);
  }


  onAddGroup() {
    let newGroup: RequestedCourseGroup = {id: null,
      requestedCourses: [{id:null, courseCode: null, courseName:null,credits:null}],
      requestedExemptedCourse: {id: null, courseCode: null, courseName: null, courseType: null, credits: null}};
    this.requestedCourseGroups.push(newGroup);
  }

  onAddCourse(courseGroup: RequestedCourseGroup) {
    let newRequestedCourse: RequestedCourse = {id: null, courseCode: null, courseName: null, credits: null};
    courseGroup.requestedCourses.push(newRequestedCourse);
  }

  onGroupDelete(groupIndex: number) {
    this.requestedCourseGroups.splice(groupIndex, 1);
  }

  onCourseDelete(groupIndex: number, courseIndex: number) {
    this.requestedCourseGroups[groupIndex].requestedCourses.splice(courseIndex,1);
  }

  onSubmit() {
    console.log(this.requestedCourseGroups);
    this.dialogRef.close(this.requestedCourseGroups);
  }

}

