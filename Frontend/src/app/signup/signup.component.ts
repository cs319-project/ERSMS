import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';
import {AppScene} from "../app.component";

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  @Input() currentScene!: AppScene;
  @Output() currentSceneChange: EventEmitter<AppScene> = new EventEmitter<AppScene>();

  constructor(private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  hidePassword = true;
  signUp(){
    let successful = true; // TODO: Add checks
    if (successful) {
      this.openSnackBar("Signup successful!", "Close", 5000);
    }
  }

  openSnackBar(message: string, action: string, duration: number) {
    this._snackBar.open(message, action, {
      duration: duration * 1000
    });
  }
  goLogin() {
    this.currentScene = AppScene.Login;
    this.currentSceneChange.emit(this.currentScene);
  }
}
