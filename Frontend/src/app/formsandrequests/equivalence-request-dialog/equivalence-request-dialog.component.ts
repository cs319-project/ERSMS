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
  fileName: string = '';

  constructor(
    public dialogRef: MatDialogRef<EquivalenceRequestDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EquivalenceRequest,
    private toastr: ToastrService,
    //private fileUploadService: FileUploadService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {}

  onSubmit() {
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
    const file: File = event.target.files[0];
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      text: `Are you sure to upload this syllabus for  ${this.data.hostCourseName}?`,
      fileName: this.fileName
    };
    const dialogRef = this.dialog.open(
      ScoreTableUploadDialogComponent,
      dialogConfig
    );

    dialogRef.afterClosed().subscribe(uploadItem => {
      if (uploadItem) {
        this.fileName = file.name;
        // TODO: add upload syllabus logic
      }
    });
  }
}
