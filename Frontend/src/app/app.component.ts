import {Component, OnInit} from "@angular/core";
import { LoginComponent } from "./login/login.component";
import {Router} from "@angular/router";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent implements OnInit{
  title = "ERSMS";

  isLoggedBefore: boolean = true;
  currentScene: AppScene;

  constructor(private router: Router) {

  }
  ngOnInit(): void {
    if (this.isLoggedBefore){
      this.router.navigate([`../dashboard`]);
      this.currentScene = AppScene.App;
    }
    else{
      this.router.navigate([`../login`]);
      this.currentScene = AppScene.Login;
    }
  }


  isLogin(){
    return this.currentScene == AppScene.Login;
  }
  isSignUp(){
    return this.currentScene == AppScene.SignUp;
  }
  isApp(){
    return this.currentScene == AppScene.App;
  }
  isForgotMyPassword(){
    return this.currentScene == AppScene.ForgotPassword;
  }
}

export enum AppScene{
  Login,
  SignUp,
  ForgotPassword,
  App
}
