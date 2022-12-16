import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ViewEquivalanceRequest} from "./viewEquivalanceRequest";
import {Approval} from "../../_models/approval";

@Component({
  selector: 'app-view-equivalance-request-dialog',
  templateUrl: './view-equivalance-request-dialog.component.html',
  styleUrls: ['./view-equivalance-request-dialog.component.css']
})
export class ViewEquivalanceRequestDialogComponent implements OnInit {
  formStatus: string;
  instructorStatus: string;
  constructor(public dialogRef: MatDialogRef<ViewEquivalanceRequestDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: ViewEquivalanceRequest) { }

  ngOnInit(): void {
    this.instructorStatus = this.getStatus(this.data.eqReq.instructorApproval);

    if (this.data.eqReq.isApproved){
      this.formStatus = 'Accepted';
    }
    else if(this.data.eqReq.isRejected){
      this.formStatus = 'Rejected';
    }
    else if(this.data.eqReq.isCanceled){
      this.formStatus = 'Cancelled';
    }
    else{
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
