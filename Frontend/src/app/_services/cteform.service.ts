import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GUID } from 'src/utils/guid';
import { CteForm } from '../_models/cte-form';
import { Approval } from '../_models/approval';
@Injectable({
  providedIn: 'root'
})
export class CTEFormService {
  baseApiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  uploadPdf(formId: GUID, pdf): Observable<any> {
    const formData = new FormData();
    formData.append('pdf', pdf);
    return this.http.post<any>(
      `${this.baseApiUrl}cteform/upload/?formId=${formId}`,
      formData
    );
  }

  downloadPdf(formId: GUID): Observable<any> {
    return this.http.get(`${this.baseApiUrl}cteform/download/${formId}`, {
      responseType: 'blob'
    });
  }

  deletePdf(formId: GUID): Observable<any> {
    return this.http.delete<any>(
      `${this.baseApiUrl}cteform/deletePdf/${formId}`
    );
  }

  getCTEForms(): Observable<any> {
    return this.http.get<any>(`${this.baseApiUrl}cteform/getall`);
  }

  getCTEFomById(id: GUID): Observable<any> {
    return this.http.get<any>(`${this.baseApiUrl}cteform/${id}`);
  }

  createCTEForm(cteForm: CteForm): Observable<any> {
    return this.http.post<any>(`${this.baseApiUrl}cteform`, cteForm);
  }

  updateCTEForm(cteForm: CteForm): Observable<any> {
    return this.http.put<any>(`${this.baseApiUrl}cteform/update`, cteForm);
  }

  deleteCTEForm(id: GUID): Observable<any> {
    return this.http.delete<any>(`${this.baseApiUrl}cteform/${id}`);
  }

  getCTEFormOfStudent(studentId: string): Observable<any> {
    return this.http.get<any>(`${this.baseApiUrl}cteform/student/${studentId}`);
  }

  getAllArchivedCTEForms(): Observable<any> {
    return this.http.get<any>(`${this.baseApiUrl}cteform/archived/all`);
  }

  getArchivedCTEFormsByDepartment(userName: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}cteform/archived/department/${userName}`
    );
  }

  getAllNonArchivedCTEForms(): Observable<any> {
    return this.http.get<any>(`${this.baseApiUrl}cteform/nonarchived/all`);
  }

  getNonArchivedCTEFormsByDepartment(userName: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}cteform/nonarchived/department/${userName}`
    );
  }

  getCTEFormsByDepartment(userName: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}cteform/department/${userName}`
    );
  }

  approveCTEFormDean(approval: Approval, formId: GUID): Observable<any> {
    return this.http.post<any>(
      `${this.baseApiUrl}cteform/deanApprove/${formId}`,
      approval
    );
  }

  approveCTEFormChair(approval: Approval, formId: GUID): Observable<any> {
    return this.http.post<any>(
      `${this.baseApiUrl}cteform/chairApprove/${formId}`,
      approval
    );
  }

  approveCTEFormCoordinator(approval: Approval, formId: GUID): Observable<any> {
    return this.http.post<any>(
      `${this.baseApiUrl}cteform/coordinatorApprove/${formId}`,
      approval
    );
  }

  approveCTEFormFAB(approval: Approval, formId: GUID): Observable<any> {
    return this.http.post<any>(
      `${this.baseApiUrl}cteform/fabApprove/${formId}`,
      approval
    );
  }

  cancelCTEForm(formId: GUID): Observable<any> {
    return this.http.patch<any>(
      `${this.baseApiUrl}cteform/cancel/${formId}`,
      {}
    );
  }

  getNonArchivedCTEFormsByFacultyForDean(userName: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}cteform/nonarchived/faculty/${userName}`
    );
  }

  getArchivedCTEFormsByFacultyForDean(userName: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}cteform/archived/faculty/${userName}`
    );
  }

  getNonArchivedCTEFormsByDepartmentForChair(
    userName: string
  ): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}cteform/nonarchived/department/chair/${userName}`
    );
  }

  getArchivedCTEFormsByDepartmentForChair(userName: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}cteform/archived/department/chair/${userName}`
    );
  }
}
