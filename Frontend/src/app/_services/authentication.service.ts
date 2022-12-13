import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthenticationResult } from '../_models/authentication-result';
import { LoggedInUser } from '../_models/logged-in-user';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  token = '';
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<LoggedInUser | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private userService: UserService) {}

  login(model: any) {
    return this.http
      .post<AuthenticationResult>(this.baseUrl + 'authentication/login', model)
      .pipe(
        map((response: AuthenticationResult) => {
          const user = response;
          const token = user.token;

          let roles = this.getDecodedToken(token).role;
          if (!Array.isArray(roles)) roles = [roles];

          if (user) {
            const appUser = this.userService.getUserDetails(
              user.userName,
              roles
            );

            const loggedInUser = new LoggedInUser();
            loggedInUser.userDetail = appUser;
            loggedInUser.token = token;
            loggedInUser.roles = roles;

            console.log('loggedInUser: ', loggedInUser);

            this.setCurrentUser(loggedInUser);
          }
        })
      );
  }

  // register(model: any) {
  //   return this.http
  //     .post<DomainUser>(this.baseUrl + 'account/register', model)
  //     .pipe(
  //       map(user => {
  //         if (user) {
  //           this.setCurrentUser(user);
  //         }
  //       })
  //     );
  // }

  setCurrentUser(user: LoggedInUser) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
