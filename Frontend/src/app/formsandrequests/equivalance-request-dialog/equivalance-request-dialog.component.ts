import {Component, Inject, Input, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogConfig, MatDialogRef} from "@angular/material/dialog";
import {EquivalanceRequest} from "../../_models/equivalance-request";
import {
  ScoreTableUploadDialogComponent
} from "../../dashboard/score-table-upload-dialog/score-table-upload-dialog.component";
import {ToastrService} from "ngx-toastr";
import {FileUploadService} from "../../_services/file-upload.service";

@Component({
  selector: 'app-equivalance-request-dialog',
  templateUrl: './equivalance-request-dialog.component.html',
  styleUrls: ['./equivalance-request-dialog.component.css']
})
export class EquivalanceRequestDialogComponent implements OnInit {
  @Input()
  requiredFileType: string; // TODO: set file type
  fileName: string = "";

  constructor(    public dialogRef: MatDialogRef<EquivalanceRequestDialogComponent>,
                  @Inject(MAT_DIALOG_DATA) public data: EquivalanceRequest,
                  private toastr: ToastrService,
                  private fileUploadService: FileUploadService,
                  private dialog: MatDialog,) { }

  ngOnInit(): void {

  }

  onSubmit() {
    console.log(this.data);
    this.dialogRef.close(this.data);
  }

  onFileSelected(event) {
    const file: File = event.target.files[0];
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      text: `Are you sure to upload this syllabus for  ${
        this.data.hostCourseName
      }?`,
      fileName: this.fileName
    };
    const dialogRef = this.dialog.open(
      ScoreTableUploadDialogComponent,
      dialogConfig
    );

    dialogRef.afterClosed().subscribe(uploadItem => {
      if (uploadItem) {
        this.fileName = file.name;
        // TODO: add upload syllabus logic
      }
    });
  }
}
