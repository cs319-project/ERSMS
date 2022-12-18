import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreateUser } from './create-user';
import { ActorsEnum } from '../../../_models/enum/actors-enum';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl,
  ValidatorFn
} from '@angular/forms';
import { AuthenticationService } from '../../../_services/authentication.service';
import { ToastrService } from 'ngx-toastr';

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
    ActorsEnum.DeanDepartmentChair,
    ActorsEnum.Admin
  ];
  requiredForm: FormGroup;
  hidePassword = true;
  hidePasswordConfirm = true;
  actorType: string = '';

  constructor(
    public dialogRef: MatDialogRef<CreateUserDialogComponent>,
    private authService: AuthenticationService,
    private toastr: ToastrService,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: CreateUser
  ) {
    this.buildForm();
  }

  buildForm() {
    this.requiredForm = this.formBuilder.group({
      actorType: [this.actorType, Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      userName: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    });
    this.requiredForm.controls['password'].valueChanges.subscribe({
      next: () =>
        this.requiredForm.controls['confirmPassword'].updateValueAndValidity()
    });
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value
        ? null
        : { notMatching: true };
    };
  }

  ngOnInit(): void {}

  onSubmit() {
    if (this.requiredForm.valid) {
      this.authService.register(this.requiredForm.value).subscribe(
        (response: any) => {
          console.log(response);
          this.toastr.success('New user successfully created');
          this.dialogRef.close(response);
        },
        error => {
          const errorMsg = error.error ? error.error : error;
          this.toastr.error('Registration failed: ' + errorMsg);
        }
      );
    }
  }
}
