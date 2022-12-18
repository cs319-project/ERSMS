import { Component, Inject, OnInit } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogConfig,
  MatDialogRef
} from '@angular/material/dialog';
import { ViewPreApprovalForm } from './viewPreApprovalForm';
import { Approval } from '../../_models/approval';
import { PreApprovalFormService } from 'src/app/_services/preapprovalform.service';
import { formatDate } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { ScoreTableUploadDialogComponent } from 'src/app/dashboard/score-table-upload-dialog/score-table-upload-dialog.component';

@Component({
  selector: 'app-view-preapproval-form-dialog',
  templateUrl: './view-preapproval-form-dialog.component.html',
  styleUrls: ['./view-preapproval-form-dialog.component.css']
})
export class ViewPreapprovalFormDialogComponent implements OnInit {
  fileObj: File;
  pdf: File;
  error = true;
  formStatus: string;
  boardStatus: string;
  coordinatorStatus: string;
  nameOfUser: string;
  roleOfUser: string;
  coordinatorApproved: boolean;
  fabApproved: boolean;

  userComment: string;

  format = 'dd/MM/yyyy h:mm';
  locale = 'en-TR';
  fileName: string;

  constructor(
    public dialogRef: MatDialogRef<ViewPreapprovalFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ViewPreApprovalForm,
    private preApprovalFormService: PreApprovalFormService,
    private dialog: MatDialog,
    private toastr: ToastrService
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
        isApproved: true,
        name: this.nameOfUser,
        comment: this.userComment
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
        isApproved: true,
        name: this.nameOfUser,
        comment: this.userComment
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
        isApproved: false,
        name: this.nameOfUser,
        comment: this.userComment
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
        isApproved: false,
        name: this.nameOfUser,
        comment: this.userComment
      };
      this.preApprovalFormService
        .approvePreApprovalFormFAB(this.data.preApprovalForm.id, approval)
        .subscribe(data => {
          this.dialogRef.close();
        });
    }
  }

  formatTheDate(date: Date) {
    const formattedDate = formatDate(date.toString(), this.format, this.locale);
    return formattedDate;
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

  downloadPdf() {
    this.preApprovalFormService
      .downloadPdf(this.data.preApprovalForm.id)
      .subscribe(data => {
        var file = new Blob([data], { type: 'application/pdf' });
        var fileURL = URL.createObjectURL(file);
        window.open(fileURL);
      });
  }

  // uploadPdf() {
  //   this.preApprovalFormService
  //     .uploadPdf(this.data.preApprovalForm.id, this.data.pdfFile)
  //     .subscribe(data => {
  //       this.dialogRef.close();
  //     });
  // }

  // TODO: look for uploading mechanism
  uploadPdf(event) {
    this.fileObj = event.target.files[0];

    if (
      !(
        this.fileObj.name.endsWith('.pdf') ||
        this.fileObj.name.endsWith('.docx')
      )
    ) {
      this.error = false;
      this.toastr.error('Please upload a pdf or docx file for the syllabus');
      this.fileObj = null;
      return;
    }

    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    dialogConfig.data = {
      text: `Are you sure to upload this pdf (${this.fileObj.name})?`,
      fileName: this.data.preApprovalForm
    };

    const dialogRef = this.dialog.open(
      ScoreTableUploadDialogComponent,
      dialogConfig
    );

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // this.pdf = this.fileObj;
        // this.data.preApprovalForm.fileName = this.fileObj.name;
        this.preApprovalFormService
          .uploadPdf(this.data.preApprovalForm.id, this.fileObj)
          .subscribe(data => {
            this.dialogRef.close();
          });
      } else {
        this.fileObj = null;
        // this.data.fileName = '';
      }
    });
  }
  onFileSelected(event) {
    const file: File = event.target.files[0];

    if (file) {
      this.fileName = file.name;
    }

    if (!this.fileName.endsWith('.pdf')) {
      this.toastr.error('Please select a document (.pdf)');
      return;
    }
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      text: `Are you sure to upload this file?`,
      fileName: this.fileName
    };
    const dialogRef = this.dialog.open(
      ScoreTableUploadDialogComponent,
      dialogConfig
    );

    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        this.preApprovalFormService
          .uploadPdf(this.data.preApprovalForm.id, file)
          .subscribe(result => {
            if (result) {
              this.toastr.success('Document is uploaded successfully');
            } else {
              this.toastr.error('Error uploading score table');
            }
          });
      }
    });
  }
}
