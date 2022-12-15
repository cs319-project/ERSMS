import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AppScene } from '../app.component';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from '../_services/authentication.service';
import { Login } from '../_models/login';
import { AuthenticationResult } from '../_models/authentication-result';
import { UserService } from '../_services/user.service';
import { LoggedInUser } from '../_models/logged-in-user';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  loading = false;

  constructor(
    public authenticationService: AuthenticationService,
    private userService: UserService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  hidePassword = true;

  login() {
    this.loading = true;

    const loginInfo: Login = {
      email: this.model.email,
      password: this.model.password
    };

    this.authenticationService
      .login(loginInfo)
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

  goForgotPassword() {
    // this.currentScene = AppScene.ForgotPassword;
    // this.currentSceneChange.emit(this.currentScene);
  }

  goSignUp() {
    this.router.navigate(['/signup']);
  }
}
