import { Component, Inject, Input, OnInit } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogConfig,
  MatDialogRef
} from '@angular/material/dialog';
import { EquivalenceRequest } from '../../_models/equivalence-request';
import { ScoreTableUploadDialogComponent } from '../../dashboard/score-table-upload-dialog/score-table-upload-dialog.component';
import { ToastrService } from 'ngx-toastr';
//import { FileUploadService } from '../../_services/file-upload.service';
import { FormControl, Validators } from '@angular/forms';
import { ActorsEnum } from '../../_models/enum/actors-enum';
import { UserService } from '../../_services/user.service';
import { EquivalenceRequestService } from '../../_services/equivalencerequest.service';
import { CourseType } from 'src/app/_models/enum/course-type-enum';

@Component({
  selector: 'app-equivalence-request-dialog',
  templateUrl: './equivalence-request-dialog.component.html',
  styleUrls: ['./equivalence-request-dialog.component.css']
})
export class EquivalenceRequestDialogComponent implements OnInit {
  fileObj: File;
  courseTypes: string[] = Object.values(CourseType);
  courseType = new FormControl('', [Validators.required]);
  error = true;
  submitted = false;
  courseCode = new FormControl('', [Validators.required]);
  courseName = new FormControl('', [Validators.required]);

  syllabus: File;
  userName: string;
  file = new FormControl('', [Validators.required]);
  courseCodeBilkent = new FormControl('', [Validators.required]);
  courseNameBilkent = new FormControl('', [Validators.required]);
  
  getErrorMessageEmpty() {
    return this.courseCode.hasError('required')
      ? 'All fields must be filled'
      : this.courseName.hasError('required')
      ? 'All fields must be filled'
      : '';
  }

  getErrorMessageEmptyBilkent() {
    return this.courseCodeBilkent.hasError('required')
      ? 'All fields must be filled'
      : this.courseNameBilkent.hasError('required')
      ? 'All fields must be filled'
      : '';
  }

  @Input()
  requiredFileType: string; // TODO: set file type

  constructor(
    public dialogRef: MatDialogRef<EquivalenceRequestDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EquivalenceRequest,
    private toastr: ToastrService,
    //private fileUploadService: FileUploadService,
    private dialog: MatDialog,
    private userService: UserService,
    private eqReqService: EquivalenceRequestService
  ) {
    this.userName = JSON.parse(localStorage.getItem('user')).userName;
    this.data.studentId = this.userName;
  }

  ngOnInit(): void {}

  onSubmit() {
    this.userService.getUserDetails(this.data.studentId).subscribe(
      result => {
        if (result && result.actorType == ActorsEnum.Student) {
          this.data.hostUniversityName = result.exchangeSchool;
          this.data.firstName = result.firstName;
          this.data.lastName = result.lastName;
          console.log(this.data);
          console.log(this.syllabus);
          this.eqReqService
            .createEquivalenceRequest(this.data, this.syllabus)
            .subscribe(
              res => {
                if (res) {
                  this.toastr.success('Form is submitted successfully');
                  this.dialogRef.close();
                }
              },
              error => {
                this.toastr.error('An error occured while submitting the form');
              }
            );
        } else {
          this.toastr.error('No student with ID ' + this.data.studentId);
        }
      },
      error => {
        this.toastr.error('No student with ID ' + this.data.studentId);
      }
    );
    this.submitted = true;
    this.error =
      this.courseCode.hasError('required') ||
      this.courseName.hasError('required') ||
      this.courseCodeBilkent.hasError('required') ||
      this.courseNameBilkent.hasError('required');
    if (!this.error) {
      console.log(this.data);
      this.dialogRef.close(this.data);
    }
  }

  onFileSelected(event) {
    this.fileObj = event.target.files[0];

    if (
      !(
        this.fileObj.name.endsWith('.pdf') ||
        this.fileObj.name.endsWith('.docx')
      )
    ) {
      this.error = false;
      this.toastr.error('Please upload a pdf or docx file for the syllabus');
      this.fileObj = null;
      return;
    }

    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    dialogConfig.data = {
      text: `Are you sure to upload this syllabus for  ${this.data.hostCourseName}?`,
      fileName: this.data.fileName
    };

    const dialogRef = this.dialog.open(
      ScoreTableUploadDialogComponent,
      dialogConfig
    );

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.syllabus = this.fileObj;
        this.data.fileName = this.fileObj.name;
      } else {
        this.fileObj = null;
        this.data.fileName = '';
      }
    });
  }
}
