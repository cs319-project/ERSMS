import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GUID } from 'src/utils/guid';
@Injectable({
  providedIn: 'root'
})
export class PlacementService {
  baseApiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  uploadPlacementTable(
    file,
    departmentName: string,
    facultyName: string
  ): Observable<any> {
    const formData = new FormData();
    formData.append('placementTable', file);
    return this.http.post(
      `${this.baseApiUrl}placement/upload/?departmentName=${departmentName}&facultyName=${facultyName}`,
      formData
    );
  }

  getPlacementTables(
    departmentName: string,
    facultyName: string
  ): Observable<any> {
    return this.http.get(
      `${this.baseApiUrl}placement/get?departmentName=${departmentName}&facultyName=${facultyName}`
    );
  }

  getPlacementTable(guid: GUID): Observable<any> {
    return this.http.get(`${this.baseApiUrl}placement/get/${guid}`);
  }

  downloadPlacementTable(guid: GUID): Observable<any> {
    return this.http.get(`${this.baseApiUrl}placement/download/${guid}`, {
      responseType: 'blob'
    });
  }

  deletePlacementTable(guid: GUID): Observable<any> {
    return this.http.delete(`${this.baseApiUrl}placement/delete/${guid}`);
  }

  placeStudents(guid: GUID): Observable<any> {
    return this.http.post(
      `${this.baseApiUrl}placement/placeStudents/${guid}`,
      {}
    );
  }
}
