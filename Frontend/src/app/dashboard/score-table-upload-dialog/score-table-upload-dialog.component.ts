import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AppointmentsComponent } from '../../appointments/appointments.component';

@Component({
  selector: 'app-score-table-upload-dialog',
  templateUrl: './score-table-upload-dialog.component.html',
  styleUrls: ['./score-table-upload-dialog.component.css']
})
export class ScoreTableUploadDialogComponent implements OnInit {
  text: string;
  fileName: string;

  constructor(
    public dialogRef: MatDialogRef<AppointmentsComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) {
    this.text = data.text;
    this.fileName = data.fileName;
  }

  ngOnInit(): void {}

  getResponse(uploadTable: boolean) {
    this.dialogRef.close(uploadTable);
  }
}
