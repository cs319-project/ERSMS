import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GUID } from 'src/utils/guid';
import { CreateMessage } from '../_models/create-message';
@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseApiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getFullThread(firstUser: string, secondUser: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}message/fullthread/?firstUser=${firstUser}&secondUser=${secondUser}`
    );
  }

  createNewMessage(createMessage: CreateMessage): Observable<any> {
    return this.http.post(`${this.baseApiUrl}message`, createMessage);
  }

  getOneWayMessages(
    senderUsername: string,
    recipientUsername: string
  ): Observable<any> {
    return this.http.get<any>(
      `${this.baseApiUrl}message/?sender=${senderUsername}&recipient=${recipientUsername}`
    );
  }

  deleteMessage(messageId: GUID): Observable<any> {
    return this.http.delete<any>(`${this.baseApiUrl}message/${messageId}`);
  }

  getDMUserList(username: string): Observable<any> {
    return this.http.get<any>(`${this.baseApiUrl}message/userlist/${username}`);
  }
}
