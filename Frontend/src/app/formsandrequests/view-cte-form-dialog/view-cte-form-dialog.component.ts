import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ViewCTEForm } from './viewCTEForm';
import { Approval } from '../../_models/approval';

@Component({
  selector: 'app-view-cte-form-dialog',
  templateUrl: './view-cte-form-dialog.component.html',
  styleUrls: ['./view-cte-form-dialog.component.css']
})
export class ViewCteFormDialogComponent implements OnInit {
  formStatus: string;
  chairStatus: string;
  deanStatus: string;
  boardStatus: string;
  coordinatorStatus: string;
  constructor(
    public dialogRef: MatDialogRef<ViewCteFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ViewCTEForm
  ) {}

  ngOnInit(): void {
    if (this.data.cteForm.chairApproval != null) {
      this.chairStatus = this.getStatus(this.data.cteForm.chairApproval);
    } else {
      this.chairStatus = 'Waiting';
    }
    if (this.data.cteForm.facultyOfAdministrationBoardApproval != null) {
      this.boardStatus = this.getStatus(
        this.data.cteForm.facultyOfAdministrationBoardApproval
      );
    } else {
      this.boardStatus = 'Waiting';
    }
    if (this.data.cteForm.exchangeCoordinatorApproval != null) {
      this.coordinatorStatus = this.getStatus(
        this.data.cteForm.exchangeCoordinatorApproval
      );
    } else {
      this.coordinatorStatus = 'Waiting';
    }
    if (this.data.cteForm.deanApproval != null) {
      this.deanStatus = this.getStatus(this.data.cteForm.deanApproval);
    } else {
      this.deanStatus = 'Waiting';
    }
    if (this.data.cteForm.isApproved) {
      this.formStatus = 'Accepted';
    } else if (this.data.cteForm.isRejected) {
      this.formStatus = 'Rejected';
    } else if (this.data.cteForm.isCanceled) {
      this.formStatus = 'Cancelled';
    } else {
      this.formStatus = 'Waiting';
    }
  }

  getStatus(approval: Approval) {
    if (approval.dateOfApproval == null) {
      return 'Waiting';
    } else {
      if (approval.isApproved) {
        return 'Accepted';
      } else {
        return 'Rejected';
      }
    }
  }
}
