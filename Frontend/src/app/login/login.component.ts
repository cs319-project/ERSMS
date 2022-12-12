import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';
import {AppScene} from "../app.component";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  @Input() currentScene!: AppScene;
  @Output() currentSceneChange: EventEmitter<AppScene> = new EventEmitter<AppScene>();

  constructor(private _snackBar: MatSnackBar, private router: Router) { }

  ngOnInit(): void {
  }

  hidePassword = true;

  login(){
    let successful = true; // TODO: Add checks
    if (successful) {
      this.openSnackBar("Login successful!", "Close", 5000);
    }
    this.currentScene = AppScene.App;
    this.router.navigate([`../dashboard`]);
    this.currentSceneChange.emit(this.currentScene);
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
