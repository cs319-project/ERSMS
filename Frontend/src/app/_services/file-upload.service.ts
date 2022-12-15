import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
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
      `${this.baseApiUrl}placement/upload?departmentName=${departmentName}&facultyName=${facultyName}`,
      formData
    );
  }
}
