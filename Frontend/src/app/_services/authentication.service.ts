import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { DomainUser } from '../_models/domain-user';
import { Admin } from '../_models/admin';
import { AuthenticationResult } from '../_models/authentication-result';
import { Student } from '../_models/student';
import { OISEP } from '../_models/oisep';
import { ExchangeCoordinator } from '../_models/exchange-coordinator';
import { DeanDepartmentChair } from '../_models/dean-department-chair';
import { CourseCoordinatorInstructor } from '../_models/course-coordinator-instructor';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  token = '';
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<
    | Admin
    | Student
    | OISEP
    | ExchangeCoordinator
    | DeanDepartmentChair
    | CourseCoordinatorInstructor
    | null
  >(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http
      .post<AuthenticationResult>(this.baseUrl + 'authentication/login', model)
      .pipe(
        map((response: AuthenticationResult) => {
          const actorType = response.actorType;
          this.token = response.token;
          console.log(response.actorType);

          if (actorType === 'Admin') {
            this.http
              .get<Admin>(this.baseUrl + 'user/' + response.userName)
              .subscribe(data => {
                this.setCurrentUser(data);
              });
          } else if (actorType === 'Student') {
            this.http
              .get<Student>(this.baseUrl + 'user/' + response.userName)
              .subscribe(data => {
                this.setCurrentUser(data);
              });
          } else if (
            actorType ===
            'Office of International Students and Exchange Programs'
          ) {
            this.http
              .get<OISEP>(this.baseUrl + 'user/' + response.userName)
              .subscribe(data => {
                this.setCurrentUser(data);
              });
          } else if (actorType === 'Exchange Coordinator') {
            this.http
              .get<ExchangeCoordinator>(
                this.baseUrl + 'user/' + response.userName
              )
              .subscribe(data => {
                this.setCurrentUser(data);
              });
          } else if (actorType === 'Dean Department Chair') {
            this.http
              .get<DeanDepartmentChair>(
                this.baseUrl + 'user/' + response.userName
              )
              .subscribe(data => {
                this.setCurrentUser(data);
              });
          } else if (actorType === 'Course Coordinator Instructor') {
            this.http
              .get<CourseCoordinatorInstructor>(
                this.baseUrl + 'user/' + response.userName
              )
              .subscribe(data => {
                this.setCurrentUser(data);
              });
          }
        })
      );
  }

  register(model: any) {
    return this.http
      .post<DomainUser>(this.baseUrl + 'account/register', model)
      .pipe(
        map(user => {
          if (user) {
            this.setCurrentUser(user);
          }
        })
      );
  }

  setCurrentUser(
    user:
      | Student
      | Admin
      | OISEP
      | ExchangeCoordinator
      | DeanDepartmentChair
      | CourseCoordinatorInstructor
  ) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
