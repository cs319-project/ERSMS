import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {AppointmentsComponent} from "../appointments.component";

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrls: ['./confirmation-dialog.component.css']
})
export class ConfirmationDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<AppointmentsComponent>,
              @Inject(MAT_DIALOG_DATA) public data) {
    data.text;
  }

  ngOnInit(): void {
  }

  save_choice(deleteItem: boolean) {
    this.dialogRef.close(deleteItem);
  }
}
