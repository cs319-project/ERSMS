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
    const userDetail = Array<ActorsType>();
    for (let role in roles) {
      const actualRole = roles[role];
      switch (actualRole) {
        case ActorsEnum.Admin:
          this.http.get<Admin>(this.baseUrl + userName).subscribe(response => {
            userDetail.push(response);
          });
          break;
        case ActorsEnum.Student:
          this.http
            .get<Student>(this.baseUrl + userName)
            .subscribe(response => {
              userDetail.push(response);
            });
          break;
        case ActorsEnum.OISEP:
          this.http.get<OISEP>(this.baseUrl + userName).subscribe(response => {
            userDetail.push(response);
          });
          break;
        case ActorsEnum.ExchangeCoordinator:
          this.http
            .get<ExchangeCoordinator>(this.baseUrl + userName)
            .subscribe(response => {
              userDetail.push(response);
            });
          break;
        case ActorsEnum.DeanDepartmentChair:
          this.http
            .get<DeanDepartmentChair>(this.baseUrl + userName)
            .subscribe(response => {
              userDetail.push(response);
            });
          break;
        case ActorsEnum.CourseCoordinatorInstructor:
          this.http
            .get<CourseCoordinatorInstructor>(this.baseUrl + userName)
            .subscribe(response => {
              userDetail.push(response);
            });
          break;
      }
    }
    return userDetail;
  }
}
