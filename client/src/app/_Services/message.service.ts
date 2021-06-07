import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_Models/member';
import { Message } from '../_Models/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = environment.apiUrl;

  constructor(private http:HttpClient) { }

  getMessages(container:string){
    return this.http.get<any>(this.baseUrl+'message?container=' + container)
  }

  getMessageThread(username:string){
    return this.http.get<any>(this.baseUrl + 'message/thread/' + username)
  }

  sendMessage(recipientUsername:string, content:string){
    return this.http.post(this.baseUrl + "message" , {
      "recipientUsername": recipientUsername,
      "content": content
    })
  }

  deleteMessage(id:number){
    return this.http.delete(this.baseUrl+"message/"+ id);
  }
}
