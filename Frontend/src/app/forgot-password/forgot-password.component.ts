import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {AppScene} from "../app.component";


@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  @Input() currentScene!: AppScene;
  @Output() currentSceneChange: EventEmitter<AppScene> = new EventEmitter<AppScene>();

  constructor() {}

  ngOnInit(): void {
  }

  goLogin() {
    this.currentScene = AppScene.Login;
    this.currentSceneChange.emit(this.currentScene);
  }
}
