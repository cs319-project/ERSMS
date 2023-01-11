import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {UserService} from "../../_services/user.service";
import {ToastrService} from "ngx-toastr";
import {Student} from "../../_models/student";
import {ManualPlacementData, UserData} from "../placement.component";

@Component({
  selector: 'app-manual-placement-dialog',
  templateUrl: './manual-placement-dialog.component.html',
  styleUrls: ['./manual-placement-dialog.component.css']
})
export class ManualPlacementDialogComponent implements OnInit {
  constructor(public dialogRef: MatDialogRef<ManualPlacementDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: ManualPlacementData,
              private userService: UserService,
              private toastr: ToastrService) {
  }

  ngOnInit(): void {
  }

  onSave(unplaced) {
    this.userService.getUserDetails(this.data.user.id.toString()).subscribe(user => {
      user.exchangeSchool = this.data.user.hostUniversity;
      this.userService.updateUser(user).subscribe(
        (res: any) => {
          this.toastr.success('User updated successfully');
          this.dialogRef.close(this.data);
        },
        error => {
          const errorMsg = error.error ? error.error : error;
          this.toastr.error('User update failed: ' + errorMsg);
        }
      );
    });
  }
}
