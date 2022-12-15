import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  baseApiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getNotifications(userName: string): Observable<any> {
    return this.http.get(`${this.baseApiUrl}notification/${userName}`);
  }

  readAllNotifications(userName: string): Observable<any> {
    return this.http.put(
      `${this.baseApiUrl}notification/markallasread/${userName}`,
      {}
    );
  }
}
