import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators
} from '@angular/forms';
import { AuthenticationService } from '../_services/authentication.service';
import { ActorsEnum } from '../_models/enum/actors-enum';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  requiredForm: FormGroup;
  hidePassword = true;
  hidePasswordConfirm: boolean = true;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private toastr: ToastrService
  ) {
    this.buildForm();
  }

  ngOnInit(): void {}

  signUp() {
    if (this.requiredForm.valid) {
      this.authService.register(this.requiredForm.value).subscribe(
        (response: any) => {
          this.toastr.success('Registration successful');
          this.router.navigate(['/login']);
        },
        error => {
          const errorMsg = error.error ? error.error : error;
          this.toastr.error('Registration failed: ' + errorMsg);
        }
      );
    }
  }

  buildForm() {
    this.requiredForm = this.formBuilder.group({
      actorType: [ActorsEnum.Student.toString(), Validators.required],
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

  onSubmit() {}

  goLogin() {
    this.router.navigate(['/login']);
  }
}
