import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ViewPreApprovalForm} from "./viewPreApprovalForm";
import {Approval} from "../../_models/approval";

@Component({
  selector: 'app-view-preapproval-form-dialog',
  templateUrl: './view-preapproval-form-dialog.component.html',
  styleUrls: ['./view-preapproval-form-dialog.component.css']
})
export class ViewPreapprovalFormDialogComponent implements OnInit {


  formStatus: string;
  boardStatus: string;
  coordinatorStatus: string;
  constructor(public dialogRef: MatDialogRef<ViewPreapprovalFormDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: ViewPreApprovalForm) { }

  ngOnInit(): void {
    this.boardStatus = this.getStatus(this.data.preApprovalForm.facultyAdministrationBoardApproval);
    this.coordinatorStatus = this.getStatus(this.data.preApprovalForm.exchangeCoordinatorApproval);
    if (this.data.preApprovalForm.isApproved){
      this.formStatus = 'Accepted';
    }
    else if(this.data.preApprovalForm.isRejected){
      this.formStatus = 'Rejected';
    }
    else if(this.data.preApprovalForm.isCanceled){
      this.formStatus = 'Cancelled';
    }
    else{
      this.formStatus = 'Waiting';
    }
  }

  getStatus(approval: Approval){
    if(approval.dateOfApproval == null){
      return 'Waiting';
    }
    else{
      if(approval.isApproved){
        return 'Accepted';
      }
      else{
        return 'Rejected';
      }
    }
  }

}
