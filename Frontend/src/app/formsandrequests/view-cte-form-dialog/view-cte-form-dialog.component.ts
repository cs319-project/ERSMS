import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ViewCTEForm } from './viewCTEForm';
import { Approval } from '../../_models/approval';
import { Student } from 'src/app/_models/student';
import { CTEFormService } from 'src/app/_services/cteform.service';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-view-cte-form-dialog',
  templateUrl: './view-cte-form-dialog.component.html',
  styleUrls: ['./view-cte-form-dialog.component.css']
})
export class ViewCteFormDialogComponent implements OnInit {
  @Input() student: Student;
  formStatus: string;
  chairStatus: string;
  deanStatus: string;
  boardStatus: string;
  coordinatorStatus: string;
  roleOfUser: string;
  nameOfUser: string;
  isFABApproved: boolean;
  isSelfApproved: boolean;

  format = 'dd/MM/yyyy h:mm';
  locale = 'en-TR';

  constructor(
    public dialogRef: MatDialogRef<ViewCteFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ViewCTEForm,
    private cteFormService: CTEFormService
  ) {
    this.roleOfUser = JSON.parse(localStorage.getItem('user')).roles[0];
    this.nameOfUser =
      JSON.parse(localStorage.getItem('user')).userDetails.firstName +
      ' ' +
      JSON.parse(localStorage.getItem('user')).userDetails.lastName;
  }
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
      this.isFABApproved = true;
    } else {
      this.boardStatus = 'Waiting';
      this.isFABApproved = false;
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

    if (
      this.roleOfUser == 'Exchange Coordinator' &&
      this.coordinatorStatus == 'Waiting'
    ) {
      this.isSelfApproved = false;
    } else if (this.roleOfUser == 'Dean Department Chair') {
      let isDean = JSON.parse(localStorage.getItem('user')).userDetails.isDean;
      if (isDean && this.deanStatus == 'Waiting') {
        this.isSelfApproved = false;
      } else if (!isDean && this.chairStatus == 'Waiting') {
        this.isSelfApproved = false;
      } else {
        this.isSelfApproved = true;
      }
    } else {
      this.isSelfApproved = true;
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

  approveForm() {
    if (this.roleOfUser == 'Exchange Coordinator') {
      let approval: Approval = {
        dateOfApproval: new Date(),
        isApproved: true,
        name: this.nameOfUser,
        comment: this.data.approvalComment
      };
      this.cteFormService
        .approveCTEFormCoordinator(approval, this.data.cteForm.id)
        .subscribe(data => {
          this.dialogRef.close();
        });
    } else if (this.roleOfUser == 'Dean Department Chair') {
      let approval: Approval = {
        dateOfApproval: new Date(),
        isApproved: true,
        name: this.nameOfUser,
        comment: this.data.approvalComment
      };
      let isDean = JSON.parse(localStorage.getItem('user')).userDetails.isDean;

      if (!isDean) {
        this.cteFormService
          .approveCTEFormChair(approval, this.data.cteForm.id)
          .subscribe(data => {
            this.dialogRef.close();
          });
      } else {
        this.cteFormService
          .approveCTEFormDean(approval, this.data.cteForm.id)
          .subscribe(data => {
            this.dialogRef.close();
          });
      }
    }
  }

  approveFormFAB() {
    let approval: Approval = {
      dateOfApproval: new Date(),
      isApproved: true,
      name: this.nameOfUser,
      comment: this.data.approvalComment
    };
    this.cteFormService
      .approveCTEFormFAB(approval, this.data.cteForm.id)
      .subscribe(data => {
        this.dialogRef.close();
      });
  }

  rejectFormFAB() {
    let approval: Approval = {
      dateOfApproval: new Date(),
      isApproved: false,
      name: this.nameOfUser,
      comment: this.data.approvalComment
    };
    this.cteFormService
      .approveCTEFormFAB(approval, this.data.cteForm.id)
      .subscribe(data => {
        this.dialogRef.close();
      });
  }

  formatTheDate(date: Date) {
    const formattedDate = formatDate(date.toString(), this.format, this.locale);
    return formattedDate;
  }

  rejectForm() {
    if (this.roleOfUser == 'Exchange Coordinator') {
      let approval: Approval = {
        dateOfApproval: new Date(),
        isApproved: false,
        name: this.nameOfUser,
        comment: this.data.approvalComment
      };
      this.cteFormService
        .approveCTEFormCoordinator(approval, this.data.cteForm.id)
        .subscribe(data => {
          this.dialogRef.close();
        });
    } else if (this.roleOfUser == 'Dean Department Chair') {
      let approval: Approval = {
        dateOfApproval: new Date(),
        isApproved: false,
        name: this.nameOfUser,
        comment: this.data.approvalComment
      };
      let isDean = JSON.parse(localStorage.getItem('user')).userDetails.isDean;

      if (!isDean) {
        this.cteFormService
          .approveCTEFormChair(approval, this.data.cteForm.id)
          .subscribe(data => {
            this.dialogRef.close();
          });
      } else {
        this.cteFormService
          .approveCTEFormDean(approval, this.data.cteForm.id)
          .subscribe(data => {
            this.dialogRef.close();
          });
      }
    }
  }

  downloadPdf() {
    this.cteFormService.downloadPdf(this.data.cteForm.id).subscribe(data => {
      var file = new Blob([data], { type: 'application/pdf' });
      var fileURL = URL.createObjectURL(file);
      window.open(fileURL);
    });
  }
}
