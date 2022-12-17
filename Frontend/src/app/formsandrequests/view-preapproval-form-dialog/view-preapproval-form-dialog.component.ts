import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ViewPreApprovalForm } from './viewPreApprovalForm';
import { Approval } from '../../_models/approval';
import { PreApprovalFormService } from 'src/app/_services/preapprovalform.service';

@Component({
  selector: 'app-view-preapproval-form-dialog',
  templateUrl: './view-preapproval-form-dialog.component.html',
  styleUrls: ['./view-preapproval-form-dialog.component.css']
})
export class ViewPreapprovalFormDialogComponent implements OnInit {
  formStatus: string;
  boardStatus: string;
  coordinatorStatus: string;
  nameOfUser: string;
  roleOfUser: string;
  coordinatorApproved: boolean;
  fabApproved: boolean;
  constructor(
    public dialogRef: MatDialogRef<ViewPreapprovalFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ViewPreApprovalForm,
    private preApprovalFormService: PreApprovalFormService
  ) {
    this.nameOfUser =
      JSON.parse(localStorage.getItem('user')).userDetails.firstName +
      ' ' +
      JSON.parse(localStorage.getItem('user')).userDetails.lastName;
    this.roleOfUser = JSON.parse(localStorage.getItem('user')).roles[0];
  }

  ngOnInit(): void {
    if (this.data.preApprovalForm.facultyAdministrationBoardApproval != null) {
      this.boardStatus = this.getStatus(
        this.data.preApprovalForm.facultyAdministrationBoardApproval
      );
      this.fabApproved = true;
    } else {
      this.boardStatus = 'Waiting';
      this.fabApproved = false;
    }
    if (this.data.preApprovalForm.exchangeCoordinatorApproval != null) {
      this.coordinatorStatus = this.getStatus(
        this.data.preApprovalForm.exchangeCoordinatorApproval
      );
      this.coordinatorApproved = true;
    } else {
      this.coordinatorStatus = 'Waiting';
      this.coordinatorApproved = false;
    }
    if (this.data.preApprovalForm.isApproved) {
      this.formStatus = 'Accepted';
    } else if (this.data.preApprovalForm.isRejected) {
      this.formStatus = 'Rejected';
    } else if (this.data.preApprovalForm.isCanceled) {
      this.formStatus = 'Cancelled';
    } else {
      this.formStatus = 'Waiting';
    }
  }

  approveForm() {
    if (this.roleOfUser == 'Exchange Coordinator') {
      let approval: Approval = {
        dateOfApproval: new Date(),
        isApproved: true,
        name: this.nameOfUser,
        comment: this.data.approvalComment
      };
      this.preApprovalFormService
        .approvePreApprovalFormCoordinator(
          this.data.preApprovalForm.id,
          approval
        )
        .subscribe(data => {
          this.dialogRef.close();
        });
    }
  }

  approveFormFAB() {
    if (this.roleOfUser == 'Exchange Coordinator') {
      let approval: Approval = {
        dateOfApproval: new Date(),
        isApproved: true,
        name: this.nameOfUser,
        comment: this.data.approvalComment
      };
      this.preApprovalFormService
        .approvePreApprovalFormFAB(this.data.preApprovalForm.id, approval)
        .subscribe(data => {
          this.dialogRef.close();
        });
    }
  }

  rejectForm() {
    if (this.roleOfUser == 'Exchange Coordinator') {
      let approval: Approval = {
        dateOfApproval: new Date(),
        isApproved: false,
        name: this.nameOfUser,
        comment: this.data.approvalComment
      };
      this.preApprovalFormService
        .approvePreApprovalFormCoordinator(
          this.data.preApprovalForm.id,
          approval
        )
        .subscribe(data => {
          this.dialogRef.close();
        });
    }
  }

  rejectFormFAB() {
    if (this.roleOfUser == 'Exchange Coordinator') {
      let approval: Approval = {
        dateOfApproval: new Date(),
        isApproved: false,
        name: this.nameOfUser,
        comment: this.data.approvalComment
      };
      this.preApprovalFormService
        .approvePreApprovalFormFAB(this.data.preApprovalForm.id, approval)
        .subscribe(data => {
          this.dialogRef.close();
        });
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
