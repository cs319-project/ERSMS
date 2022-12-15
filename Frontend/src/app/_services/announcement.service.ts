import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GUID } from 'src/utils/guid';
import { Announcement } from '../_models/announcement';
@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {
  baseApiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getAllAnnouncements(): Observable<any> {
    return this.http.get(`${this.baseApiUrl}announcement/getall`);
  }

  getAnnouncement(guid: GUID): Observable<any> {
    return this.http.get(`${this.baseApiUrl}announcement/get/${guid}`);
  }

  deleteAnnouncement(guid: GUID): Observable<any> {
    return this.http.delete(`${this.baseApiUrl}announcement/${guid}`);
  }

  createAnnouncement(announcement: Announcement): Observable<any> {
    return this.http.post(`${this.baseApiUrl}announcement`, announcement);
  }

  updateAnnouncement(announcement: Announcement): Observable<any> {
    return this.http.put(`${this.baseApiUrl}announcement`, announcement);
  }
}
