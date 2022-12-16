import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreateUser } from './create-user';
import { ActorsEnum } from '../../../_models/enum/actors-enum';

@Component({
  selector: 'app-create-user-dialog',
  templateUrl: './create-user-dialog.component.html',
  styleUrls: ['./create-user-dialog.component.css']
})
export class CreateUserDialogComponent implements OnInit {
  userTypes: string[] = [
    ActorsEnum.OISEP,
    ActorsEnum.Student,
    ActorsEnum.ExchangeCoordinator,
    ActorsEnum.CourseCoordinatorInstructor,
    ActorsEnum.DeanDepartmentChair
  ];
  hidePassword: boolean = true;
  hidePasswordConfirm: boolean = true;
  constructor(
    public dialogRef: MatDialogRef<CreateUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CreateUser
  ) {
    if (this.data == null) {
      this.data = {
        appUser: {
          firstName: '',
          lastName: '',
          identityUser: { email: '', userName: '' },
          actorType: null,
          id: null
        },
        password: ''
      };
    }
  }

  ngOnInit(): void {}

  onSubmit() {
    console.log(this.data);
    this.dialogRef.close(this.data);
  }
}
