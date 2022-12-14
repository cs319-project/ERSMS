import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map, catchError, switchMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthenticationResult } from '../_models/authentication-result';
import { LoggedInUser } from '../_models/logged-in-user';
import { ActorsType } from '../_types/actors-type';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<LoggedInUser | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post(this.baseUrl + 'authentication/login', model).pipe(
      map((response: AuthenticationResult) => {
        const user = response;
        if (user) {
          const loggedInUser: LoggedInUser = {
            userName: user.userName,
            roles: [],
            token: user.token,
            userDetails: user.userDetails
          };

          this.setCurrentUser(loggedInUser);
        }
      })
    );
  }

  // register(model: any) {
  //   return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
  //     map(user => {
  //       if (user) {
  //         this.setCurrentUser(user);
  //       }
  //     })
  //   );
  //  }

  setCurrentUser(user: LoggedInUser) {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? (user.roles = roles) : user.roles.push(roles);
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
