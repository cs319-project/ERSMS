import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GUID } from 'src/utils/guid';
import { ToDoItem } from '../_models/to-do-item';
@Injectable({
  providedIn: 'root'
})
export class ToDoItemService {
  baseApiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  createToDoItem(toDoItem: ToDoItem, username: string): Observable<any> {
    return this.http.post<any>(
      `${this.baseApiUrl}/todoitem/${username}`,
      toDoItem
    );
  }

  updateToDoItem(toDoItem: ToDoItem): Observable<any> {
    return this.http.put<any>(`${this.baseApiUrl}/todoitem`, toDoItem);
  }

  getToDoItems(): Observable<any> {
    return this.http.get<any>(`${this.baseApiUrl}/todoitem/getall`);
  }

  deleteToDoItem(toDoItemId: GUID): Observable<any> {
    return this.http.delete<any>(`${this.baseApiUrl}/todoitem/${toDoItemId}`);
  }

  getToDoItem(toDoItemId: GUID): Observable<any> {
    return this.http.get<any>(`${this.baseApiUrl}/todoitem/${toDoItemId}`);
  }

  completeToDoItem(toDoItemId: GUID, isCompleted: boolean): Observable<any> {
    return this.http.put<any>(
      `${this.baseApiUrl}/todoitem/complete/${toDoItemId}`,
      isCompleted
    );
  }

  starToDoItem(toDoItemId: GUID, isStarred: boolean): Observable<any> {
    return this.http.put<any>(
      `${this.baseApiUrl}/todoitem/star/${toDoItemId}`,
      isStarred
    );
  }

  createToDoItemToAllDepartment(
    toDoItem: ToDoItem,
    department: string
  ): Observable<any> {
    return this.http.post<any>(
      `${this.baseApiUrl}/todoitem/addToAllDepartment/${department}`,
      toDoItem
    );
  }

  getCoordinatorToDoList(username: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}/todoitem/coordinatorToDoList/${username}`
    );
  }

  getStudentToDoList(username: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}/todoitem/studentToDoList/${username}`
    );
  }
}
