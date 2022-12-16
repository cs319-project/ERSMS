import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {ViewUser} from './ViewUser';
import {ActorsEnum} from '../../../_models/enum/actors-enum';
import {DepartmentsEnum} from "../../../_models/enum/departments-enum";
import {FacultiesEnum} from "../../../_models/enum/faculties-enum";

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.css']
})
export class UserDialogComponent implements OnInit {
  userTypes: string[] = [ActorsEnum.OISEP, ActorsEnum.Student, ActorsEnum.ExchangeCoordinator,
  ActorsEnum.CourseCoordinatorInstructor, ActorsEnum.DeanDepartmentChair];
  departments: string[] = Object.keys(DepartmentsEnum);
  faculties: string[]  = Object.values(FacultiesEnum);

  hidePassword = true;
  constructor(
    public dialogRef: MatDialogRef<UserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ViewUser
  ) {
    if (this.data == null) {
      this.data = {
        user: {
          id: null,
          actorType: '',
          identityUser: { userName: '', email: '' },
          lastName: '',
          firstName: ''
        },
        userType: null,
        entranceYear: null,
        department: { facultyName: '', departmentName: '' },
        isDean: null,
        cgpa: null
      };
    }
  }

  ngOnInit(): void {}

  onSave() {
    this.dialogRef.close(this.data);
  }
}
