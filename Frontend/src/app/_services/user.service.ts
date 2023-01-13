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
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { DomainUser } from '../_models/domain-user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + 'user/';

  constructor(private http: HttpClient) {}

  getUserDetails(userName: string): Observable<any> {
    return this.http.get(`${this.baseUrl}${userName}`);
  }

  getUsers(): Observable<DomainUser[]> {
    return this.http.get<DomainUser[]>(this.baseUrl);
  }

  updateUser(user: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}update`, user);
  }

  deleteUser(userName: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}delete/${userName}`);
  }

  getStudents(): Observable<any> {
    return this.http.get(`${this.baseUrl}placedstudent/getall`);
  }

  getSameSchoolStudents(userName: string): Observable<any> {
    return this.http.get(`${this.baseUrl}student/${userName}/sameSchool`);
  }

  getUserTuples(): Observable<any> {
    return this.http.get(`${this.baseUrl}registeredStudents/tuples`);
  }

  cancelPlacedStudent(userName: String): Observable<any> {
    console.log(`${this.baseUrl}placedStudent/cancelApplication/${userName}`);
    return this.http.patch(
      `${this.baseUrl}placedStudent/cancelApplication/${userName}`,
      null
    );
  }
}
