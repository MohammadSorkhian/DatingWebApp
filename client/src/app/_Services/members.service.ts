import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_Models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  // header = new HttpHeaders({
  //   Authorization: "Bearer " + JSON.parse(localStorage.getItem('user')).token
  // })

  constructor(private http:HttpClient) { }

  getMembers(){
    // return this.http.get<Member[]>(this.baseUrl + "users", {headers:this.header})
    return this.http.get<Member[]>(this.baseUrl + "users")
  }

  getMember(username:string){
    // return this.http.get<Member>(this.baseUrl + "users/" + username , {headers:this.header})
    return this.http.get<Member>(this.baseUrl + "users/" + username)
  }
}
