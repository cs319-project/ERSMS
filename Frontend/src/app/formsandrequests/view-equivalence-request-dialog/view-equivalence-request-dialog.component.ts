import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ViewEquivalenceRequest } from './viewEquivalenceRequest';
import { Approval } from '../../_models/approval';
import { EquivalenceRequestService } from 'src/app/_services/equivalencerequest.service';

@Component({
  selector: 'app-view-equivalence-request-dialog',
  templateUrl: './view-equivalence-request-dialog.component.html',
  styleUrls: ['./view-equivalence-request-dialog.component.css']
})
export class ViewEquivalenceRequestDialogComponent implements OnInit {
  formStatus: string;
  instructorStatus: string;
  nameOfUser: string;
  constructor(
    public dialogRef: MatDialogRef<ViewEquivalenceRequestDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ViewEquivalenceRequest,
    private cteFormService: EquivalenceRequestService
  ) {
    this.nameOfUser =
      JSON.parse(localStorage.getItem('user')).userDetails.firstName +
      ' ' +
      JSON.parse(localStorage.getItem('user')).userDetails.lastName;
  }

  ngOnInit(): void {
    if (this.data.eqReq.instructorApproval != null) {
      this.instructorStatus = this.getStatus(
        this.data.eqReq.instructorApproval
      );
    } else {
      this.instructorStatus = 'Waiting';
    }

    if (this.data.eqReq.isApproved) {
      this.formStatus = 'Accepted';
    } else if (this.data.eqReq.isRejected) {
      this.formStatus = 'Rejected';
    } else if (this.data.eqReq.isCanceled) {
      this.formStatus = 'Cancelled';
    } else {
      this.formStatus = 'Waiting';
    }
  }

  approveFormCoordinator() {
    let approval: Approval = {
      dateOfApproval: new Date(),
      isApproved: true,
      name: this.nameOfUser,
      comment: this.data.eqReq.instructorApproval.comment
    };

    this.cteFormService
      .approveEquivalenceRequest(this.data.eqReq.id, approval)
      .subscribe(() => {
        this.dialogRef.close();
      });
  }

  rejectFormCoordinator() {
    let approval: Approval = {
      dateOfApproval: new Date(),
      isApproved: false,
      name: this.nameOfUser,
      comment: this.data.eqReq.instructorApproval.comment
    };

    this.cteFormService
      .approveEquivalenceRequest(this.data.eqReq.id, approval)
      .subscribe(() => {
        this.dialogRef.close();
      });
  }

  getStatus(approval: Approval) {
    if (approval != null) {
      if (approval.dateOfApproval == null) {
        return 'Waiting';
      } else {
        if (approval.isApproved) {
          return 'Accepted';
        } else {
          return 'Rejected';
        }
      }
    } else {
      return 'Waiting';
    }
  }
}
