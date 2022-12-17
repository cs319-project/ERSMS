import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GUID } from 'src/utils/guid';
import { EquivalenceRequest } from '../_models/equivalence-request';
import { Approval } from '../_models/approval';
@Injectable({
  providedIn: 'root'
})
export class EquivalenceRequestService {
  baseApiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  createEquivalenceRequest(
    equivalenceRequest: EquivalenceRequest,
    syllabus
  ): Observable<any> {
    const formData = new FormData();
    formData.append('studentId', equivalenceRequest.studentId);
    formData.append('hostCourseName', equivalenceRequest.hostCourseName);
    formData.append('hostCourseCode', equivalenceRequest.hostCourseCode);
    formData.append('additionalNotes', equivalenceRequest.additionalNotes);
    formData.append(
      'hostCourseECTS',
      equivalenceRequest.hostCourseECTS.toString()
    );
    formData.append(
      'exemptedCourseCredit',
      equivalenceRequest.exemptedCourse.bilkentCredits.toString()
    );
    formData.append(
      'exemptedCourseECTS',
      equivalenceRequest.exemptedCourse.ects.toString()
    );
    formData.append(
      'exemptedCourseName',
      equivalenceRequest.exemptedCourse.courseName
    );
    formData.append(
      'exemptedCourseCode',
      equivalenceRequest.exemptedCourse.courseCode
    );
    formData.append(
      'exemptedCourseType',
      equivalenceRequest.exemptedCourse.courseType
    );
    formData.append('Syllabus', syllabus);

    return this.http.post<any>(
      `${this.baseApiUrl}equivalencerequest`,
      formData
    );
  }

  updateEquivalenceRequest(
    equivalenceRequest: EquivalenceRequest
  ): Observable<any> {
    return this.http.put<any>(
      `${this.baseApiUrl}equivalencerequest`,
      equivalenceRequest
    );
  }

  downloadSyllabus(equivalenceRequestId: GUID): Observable<any> {
    return this.http.get(
      `${this.baseApiUrl}equivalencerequest/download/${equivalenceRequestId}`,
      {
        responseType: 'blob'
      }
    );
  }

  getEquivalenceRequests(): Observable<any> {
    return this.http.get<any>(`${this.baseApiUrl}equivalencerequest/getall`);
  }

  getEquivalenceRequest(equivalenceRequestId: GUID): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}equivalencerequest/${equivalenceRequestId}`
    );
  }

  deleteEquivalenceRequest(equivalenceRequestId: GUID): Observable<any> {
    return this.http.delete<any>(
      `${this.baseApiUrl}equivalencerequest/${equivalenceRequestId}`
    );
  }

  updateSyllabus(equivalenceRequestId: GUID, syllabus): Observable<any> {
    const formData = new FormData();
    formData.append('Syllabus', syllabus);
    return this.http.patch<any>(
      `${this.baseApiUrl}equivalencerequest/syllabus/${equivalenceRequestId}`,
      formData
    );
  }

  getAllArchivedEquivalenceRequests(): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}equivalencerequest/archivec/all`
    );
  }

  getAllNonArchivedEquivalenceRequests(): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}equivalencerequest/nonarchived/all`
    );
  }

  getArchivedEquivalenceRequestsByDepartment(
    userName: string
  ): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}equivalencerequest/archived/department/${userName}`
    );
  }

  getNonArchivedEquivalenceRequestsByDepartment(
    userName: string
  ): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}equivalencerequest/nonarchived/department/${userName}`
    );
  }

  getArchivedEquivalenceRequestsByCourseCode(
    courseCode: string
  ): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}equivalencerequest/archived/course/${courseCode}`
    );
  }

  getNonArchivedEquivalenceRequestsByCourseCode(
    courseCode: string
  ): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}equivalencerequest/nonarchived/course/${courseCode}`
    );
  }

  getEquivalenceRequestsOfStudent(studentId: GUID): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}equivalencerequest/student/${studentId}`
    );
  }

  getEquivalenceRequestsByDepartment(userName: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}equivalencerequest/department/${userName}`
    );
  }

  getEquivalenceRequestsByCourseCode(courseCode: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}equivalencerequest/course/${courseCode}`
    );
  }

  approveEquivalenceRequest(
    equivalenceRequestId: GUID,
    approval: Approval
  ): Observable<any> {
    return this.http.post<any>(
      `${this.baseApiUrl}equivalencerequest/approve/${equivalenceRequestId}`,
      approval
    );
  }

  cancelEquivalenceRequest(equivalenceRequestId: GUID): Observable<any> {
    return this.http.patch<any>(
      `${this.baseApiUrl}equivalencerequest/cancel/${equivalenceRequestId}`,
      {}
    );
  }
}
