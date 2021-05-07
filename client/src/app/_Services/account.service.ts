import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map, tap} from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { authUser } from '../_Models/authUser.model';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  
currentUserSubject = new ReplaySubject<authUser>(1);
baseUrl = environment.apiUrl;
// currentUserExist = this.currentUserSubject.asObservable();

  constructor(private http:HttpClient) { }

  login(username:string, password:string){
    return this.http
    .post<authUser>(this.baseUrl + "account/login",
    {"userName":username, "passWord":password})
    .pipe(
      map( (res:authUser) => {
        if(res){
          localStorage.setItem("user", JSON.stringify(res))
        }
        this.setCurrentUser(res)
      }),
      );
  }

  registe(username:string, password:string){
    return this.http
    .post("https://localhost:5001/api/account/register", 
    {"userName":username, "passWord":password})
    .pipe(
      tap( (response:authUser) => {
        this.currentUserSubject.next(response);
        localStorage.setItem("user", JSON.stringify(response));
      })
    )
  }

  setCurrentUser(user:authUser){
    this.currentUserSubject.next(user)
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSubject.next(null);
  }
}
