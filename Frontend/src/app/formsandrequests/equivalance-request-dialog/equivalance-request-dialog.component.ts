import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EquivalanceRequest } from '../../_models/equivalence-request';

@Component({
  selector: 'app-equivalance-request-dialog',
  templateUrl: './equivalance-request-dialog.component.html',
  styleUrls: ['./equivalance-request-dialog.component.css']
})
export class EquivalanceRequestDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<EquivalanceRequestDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EquivalanceRequest
  ) {}

  ngOnInit(): void {}

  onSubmit() {
    console.log(this.data);
    this.dialogRef.close(this.data);
  }
}
