import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  
  constructor(private http:HttpClient) { }

  login(username:string, password:string){
    return this.http
    .post("https://localhost:5001/api/account/login",
    {"userName":username, "passWord":password})
  }
}
