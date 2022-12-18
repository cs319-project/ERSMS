import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GUID } from 'src/utils/guid';

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
    return this.http.patch(
      `${this.baseApiUrl}notification/markallasread/${userName}`,
      {}
    );
  }

  readNotification(notificationId: GUID): Observable<any> {
    return this.http.patch(
      `${this.baseApiUrl}notification/markasread/${notificationId}`,
      {}
    );
  }
}
