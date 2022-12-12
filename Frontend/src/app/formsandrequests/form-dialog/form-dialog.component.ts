import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormDialogData } from './form-dialog.interface'

@Component({
  selector: 'app-form-dialog',
  templateUrl: './form-dialog.component.html',
  styleUrls: ['./form-dialog.component.css']
})
export class FormDialogComponent implements OnInit {
  panelOpenState = false;
  displayedColumns: string[] = ['name', 'email', 'id', 'school'];
  dataSource;

  constructor(
    public dialogRef: MatDialogRef<FormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: FormDialogData
  ) {
    this.dataSource = [{
      'name': data.studentName,
      'email': data.studentEmail,
      'id': data.studentId,
      'school': data.exchangeSchool
    }]
  }

  ngOnInit(): void {
    this.dialogRef.updateSize('80%');
  }

  save_and_close(signed: boolean) {
    this.dialogRef.close(signed);
  }
}
