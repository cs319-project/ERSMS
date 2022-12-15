import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AppScene } from '../app.component';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from '../_services/authentication.service';
import { Login } from '../_models/login';
import {
  FormControl,
  FormGroup,
  Validators,
  FormBuilder
} from '@angular/forms';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  requiredForm: FormGroup;
  loading = false;
  hidePassword = true;

  constructor(
    public authenticationService: AuthenticationService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.buildForm();
  }

  buildForm() {
    this.requiredForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required])
    });
  }

  login() {
    this.loading = true;
    if (this.requiredForm.valid) {
      this.authenticationService
        .login(this.requiredForm.value)
        .pipe(first())
        .subscribe(
          data => {
            this.toastr.success('Login successful');
            this.router.navigate(['/']);
          },
          error => {
            this.loading = false;
          }
        );
    }
  }

  goForgotPassword() {
    // this.currentScene = AppScene.ForgotPassword;
    // this.currentSceneChange.emit(this.currentScene);
  }

  goSignUp() {
    this.router.navigate(['/signup']);
  }
}
