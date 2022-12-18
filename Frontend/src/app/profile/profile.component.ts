import { Component, OnInit } from '@angular/core';
import { UserService } from "../_services/user.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  userName: string;
  actorType: string;
  fullName: string;
  department: string;
  email: string;

  constructor(private userService: UserService) {
    const user = JSON.parse(localStorage.getItem('user'));
    console.log(user);
    this.userName = user.userName;
    this.actorType = user.userDetails.actorType;
    this.fullName = `${user.userDetails.firstName} ${user.userDetails.lastName}`;
    this.department = user.userDetails.department.departmentName;
    // TODO: Complete profile fields
  }

  ngOnInit(): void {}
}
