import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppScene } from '../app.component';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../_services/authentication.service';
import { Login } from '../_models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};

  @Input() currentScene!: AppScene;
  @Output() currentSceneChange: EventEmitter<AppScene> =
    new EventEmitter<AppScene>();

  constructor(
    public authenticationService: AuthenticationService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {}

  hidePassword = true;

  login() {
    const loginInfo: Login = {
      email: this.model.email,
      password: this.model.password
    };

    this.authenticationService.login(loginInfo).subscribe({
      next: _ => {
        //this.router.navigateByUrl('/members');
        this.model = {};

      }
    });

    this.currentScene = AppScene.App;
    this.router.navigate([`../dashboard`]);
    this.currentSceneChange.emit(this.currentScene);
    this.openSnackBar("Successful login", 'Close', 1);
  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }

  goForgotPassword() {
    this.currentScene = AppScene.ForgotPassword;
    this.currentSceneChange.emit(this.currentScene);
  }

  goSignUp() {
    this.currentScene = AppScene.SignUp;
    this.currentSceneChange.emit(this.currentScene);
  }
}
