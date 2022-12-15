import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EquivalenceRequest } from '../../_models/equivalence-request';

@Component({
  selector: 'app-equivalance-request-dialog',
  templateUrl: './equivalence-request-dialog.component.html',
  styleUrls: ['./equivalence-request-dialog.component.css']
})
export class EquivalenceRequestDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<EquivalenceRequestDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EquivalenceRequest
  ) {}

  ngOnInit(): void {}

  onSubmit() {
    console.log(this.data);
    this.dialogRef.close(this.data);
  }
}
