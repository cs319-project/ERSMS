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
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-equivalence-request-dialog',
  templateUrl: './equivalence-request-dialog.component.html',
  styleUrls: ['./equivalence-request-dialog.component.css']
})
export class EquivalenceRequestDialogComponent implements OnInit {
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
    private eqReqService: EquivalenceRequestService,
    private _snackBar: MatSnackBar
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
            'No student with ID ' + this.data.studentId,
            'Close',
            {
              duration: 3000
            }
          );
        }
      },
      error => {
        this._snackBar.open(
          'No student with ID ' + this.data.studentId,
          'Close',
          {
            duration: 3000
          }
        );
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
    syllabus: File = event.target.files[0];
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      text: `Are you sure to upload this syllabus for  ${this.data.hostCourseName}?`,
      fileName: this.data.fileName
    };
    const dialogRef = this.dialog.open(
      ScoreTableUploadDialogComponent,
      dialogConfig
    );

    dialogRef.afterClosed().subscribe(uploadItem => {
      if (uploadItem) {
        this.data.fileName = event.target.files[0].name;
        this.syllabus = uploadItem;
        console.log(this.syllabus);
        // TODO: add upload syllabus logic
        // this.eqReqService
        //   .createEquivalenceRequest(
        //     file,
        //     this._departmentsEnum[this.oisepDepartment][0],
        //     this._departmentsEnum[this.oisepDepartment][1]
        //   )
        //   .subscribe(
        //     res => {
        //       this.toastr.success('Score table uploaded successfully');
        //     },
        //     err => {
        //       this.toastr.error('Error uploading score table');
        //     }
        //   );
      }
    });
  }
}
