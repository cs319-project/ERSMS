import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ActorsEnum } from '../../../_models/enum/actors-enum';
import { DepartmentsEnum } from '../../../_models/enum/departments-enum';
import { FacultiesEnum } from '../../../_models/enum/faculties-enum';
import { DomainUser } from 'src/app/_models/domain-user';
import { UserService } from 'src/app/_services/user.service';
import { ToastrService } from 'ngx-toastr';
import { CourseCoordinatorInstructor } from 'src/app/_models/course-coordinator-instructor';
import { ActorsType } from 'src/app/_types/actors-type';

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.css']
})
export class UserDialogComponent implements OnInit {
  actorsEnum = ActorsEnum;
  userTypes: string[] = Object.values(ActorsEnum);
  departments: string[] = Object.values(DepartmentsEnum);
  faculties: string[] = Object.values(FacultiesEnum);
  extraData: ActorsType;

  hidePassword = true;
  constructor(
    public dialogRef: MatDialogRef<UserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DomainUser,
    private userService: UserService,
    private toastr: ToastrService
  ) {
    console.log(this.data);
    if (this.data) {
      if (this.requiresExtraData()) {
        this.userService
          .getUserDetails(data.identityUser.userName)
          .toPromise()
          .then((res: CourseCoordinatorInstructor) => {
            console.log(res);
            this.extraData = res;
          });
      }
    }
  }

  ngOnInit(): void {}

  requiresExtraData(): boolean {
    return (
      this.data.actorType === ActorsEnum.CourseCoordinatorInstructor ||
      this.data.actorType === ActorsEnum.ExchangeCoordinator ||
      this.data.actorType === ActorsEnum.DeanDepartmentChair
    );
  }

  onSave() {
    console.log(this.extraData.actorType);
    this.userService
      .updateUser(this.requiresExtraData ? this.extraData : this.data)
      .subscribe(
        (res: any) => {
          this.toastr.success('User updated successfully');
          this.dialogRef.close(null);
        },
        error => {
          const errorMsg = error.error ? error.error : error;
          this.toastr.error('User update failed: ' + errorMsg);
        }
      );
  }
}
