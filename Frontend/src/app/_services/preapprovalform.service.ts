import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GUID } from 'src/utils/guid';
import { PreApprovalForm } from '../_models/pre-approval-form';
import { Approval } from '../_models/approval';
@Injectable({
  providedIn: 'root'
})
export class PreApprovalFormService {
  baseApiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  createPreApprovalForm(preApprovalForm: PreApprovalForm): Observable<any> {
    return this.http.post<any>(
      `${this.baseApiUrl}/preapprovalform`,
      preApprovalForm
    );
  }

  updatePreApprovalForm(preApprovalForm: PreApprovalForm): Observable<any> {
    return this.http.put<any>(
      `${this.baseApiUrl}/preapprovalform`,
      preApprovalForm
    );
  }

  getPreApprovalForms(): Observable<any> {
    return this.http.get<any>(`${this.baseApiUrl}/preapprovalform/getall`);
  }

  cancelPreApprovalForm(formId: GUID): Observable<any> {
    return this.http.patch<any>(
      `${this.baseApiUrl}/preapprovalform/cancel/${formId}`,
      {}
    );
  }

  deletePreApprovalForm(formId: GUID): Observable<any> {
    return this.http.delete<any>(
      `${this.baseApiUrl}/preapprovalform/${formId}`
    );
  }

  getPreApprovalForm(formId: GUID): Observable<any> {
    return this.http.get<any>(`${this.baseApiUrl}/preapprovalform/${formId}`);
  }

  getAllArchivedPreApprovalForms(): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}/preapprovalform/archived/all`
    );
  }

  getArchivedPreApprovalFormsByDepartment(username: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}/preapprovalform/archived/department/${username}`
    );
  }

  getAllNonArchivedPreApprovalForms(): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}/preapprovalform/nonarchived/all`
    );
  }

  getNonArchivedPreApprovalFormsByDepartment(
    username: string
  ): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}/preapprovalform/nonarchived/department/${username}`
    );
  }

  getPreApprovalFormsOfStudent(studentId: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}/preapprovalform/student/${studentId}`
    );
  }

  getPreApprovalFormsByDepartment(username: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}/preapprovalform/department/${username}`
    );
  }

  approvePreApprovalFormCoordinator(
    formId: GUID,
    approval: Approval
  ): Observable<any> {
    return this.http.post<any>(
      `${this.baseApiUrl}/preapprovalform/coordinatorApprove/${formId}`,
      approval
    );
  }

  approvePreApprovalFormFAB(formId: GUID, approval: Approval): Observable<any> {
    return this.http.post<any>(
      `${this.baseApiUrl}/preapprovalform/fabApprove/${formId}`,
      approval
    );
  }
}
