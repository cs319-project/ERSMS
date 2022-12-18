import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ViewEquivalenceRequest } from './viewEquivalenceRequest';
import { Approval } from '../../_models/approval';
import { EquivalenceRequestService } from 'src/app/_services/equivalencerequest.service';
import {formatDate} from "@angular/common";

@Component({
  selector: 'app-view-equivalence-request-dialog',
  templateUrl: './view-equivalence-request-dialog.component.html',
  styleUrls: ['./view-equivalence-request-dialog.component.css']
})

export class ViewEquivalenceRequestDialogComponent implements OnInit {
  formStatus: string;
  instructorStatus: string;
  nameOfUser: string;
  roleOfUser: string;
  isFormApproved: boolean;
  format = 'dd/MM/yyyy h:mm';
  locale = 'en-TR';

  userComment: string;

  constructor(
    public dialogRef: MatDialogRef<ViewEquivalenceRequestDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ViewEquivalenceRequest,
    private eqReqService: EquivalenceRequestService
  ) {
    this.nameOfUser =
      JSON.parse(localStorage.getItem('user')).userDetails.firstName +
      ' ' +
      JSON.parse(localStorage.getItem('user')).userDetails.lastName;
    this.roleOfUser = JSON.parse(localStorage.getItem('user')).roles[0];
  }

  ngOnInit(): void {
    console.log(this.data.eqReq);
    if (this.data.eqReq.instructorApproval != null) {
      this.instructorStatus = this.getStatus(
        this.data.eqReq.instructorApproval
      );
      this.isFormApproved = true;
    } else {
      this.instructorStatus = 'Waiting';
      this.isFormApproved = false;
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

  downloadSyllabus() {
    this.eqReqService.downloadSyllabus(this.data.eqReq.id).subscribe(data => {
      const blob = new Blob([data], {
        type: this.data.eqReq.fileName.endsWith('.docx')
          ? 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'
          : 'application/pdf'
      });
      const url = window.URL.createObjectURL(blob);
      window.open(url);
    });
  }

  approveFormCoordinator() {
    let approval: Approval = {
      dateOfApproval: new Date(),
      isApproved: true,
      name: this.nameOfUser,
      comment: this.data.approvalComment
    };

    this.eqReqService
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
      comment: this.data.approvalComment
    };

    this.eqReqService
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

  formatTheDate(date: Date){
    const formattedDate = formatDate(
      date.toString(),
      this.format,
      this.locale
    );
    return formattedDate;
  }
}
