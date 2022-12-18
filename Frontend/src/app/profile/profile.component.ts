import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { Student } from '../_models/student';

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
  cgpa: number;
  preferredSemester: string;
  preferredSchools: string;
  exchangeSchool: string;
  entranceYear: number;
  otherStudents: any[];

  constructor(private userService: UserService) {
    const user = JSON.parse(localStorage.getItem('user'));
    this.userName = user.userName;
    this.actorType = user.userDetails.actorType;
    this.fullName = `${user.userDetails.firstName} ${user.userDetails.lastName}`;
    this.department = user.userDetails.major.departmentName;
    this.email = user.userDetails.identityUser.email;
    this.cgpa = user.userDetails.cgpa / 100;
    this.preferredSemester = `${user.userDetails.preferredSemester.academicYear} ${user.userDetails.preferredSemester.semester}`;
    this.preferredSchools = user.userDetails.preferredSchools.join(', ');
    this.entranceYear = user.userDetails.entranceYear;
    this.exchangeSchool = user.userDetails.exchangeSchool
      ? user.userDetails.exchangeSchool
      : 'Not placed';
    this.otherStudents = [];
    userService.getSameSchoolStudents(this.userName).subscribe(data => {
      data.forEach(other => {
        if (user.userName !== other.identityUser.userName) {
          console.log(other);
          const temp = {
            fullName: `${other.firstName} ${other.lastName}`,
            department: other.major.departmentName,
            email: other.identityUser.email
          };
          this.otherStudents.push(temp);
        }
      });
    });
  }

  ngOnInit(): void {}
}
