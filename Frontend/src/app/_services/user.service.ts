// a service for getting the user details based on actor type
//
// Path: Frontend/src/app/_services/user.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Admin } from '../_models/admin';
import { Student } from '../_models/student';
import { OISEP } from '../_models/oisep';
import { ExchangeCoordinator } from '../_models/exchange-coordinator';
import { DeanDepartmentChair } from '../_models/dean-department-chair';
import { CourseCoordinatorInstructor } from '../_models/course-coordinator-instructor';
import { ActorsType } from '../_types/actors-type';
import { ActorsEnum } from '../_models/enum/actors-enum';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + 'user/';

  constructor(private http: HttpClient) {}

  getUserDetails(userName: string, roles: any) {
    let userDetail: any;

    this.http.get(this.baseUrl + userName).subscribe(
      (response: any) => {
        console.log('response: ', response);
        userDetail = response;
      },
      error => {
        console.log(error);
      }
    );

    return userDetail;
  }
}
