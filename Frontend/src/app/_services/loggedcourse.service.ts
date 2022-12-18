import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GUID } from 'src/utils/guid';
@Injectable({
  providedIn: 'root'
})
export class LoggedCourseService {
  baseApiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getLoggedEquivalentCourses(): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}loggedcourse/logged-equivalent-courses`
    );
  }

  getLoggedTransferredCourses(): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}loggedcourse/logged-transferred-courses`
    );
  }

  deleteLoggedEquivalentCourse(loggedCourseId: GUID): Observable<any> {
    return this.http.delete<any>(
      `${this.baseApiUrl}loggedcourse/logged-equivalent-courses/${loggedCourseId}`
    );
  }

  deleteLoggedTransferredCourse(loggedCourseId: GUID): Observable<any> {
    return this.http.delete<any>(
      `${this.baseApiUrl}loggedcourse/logged-transferred-courses/${loggedCourseId}`
    );
  }
}
