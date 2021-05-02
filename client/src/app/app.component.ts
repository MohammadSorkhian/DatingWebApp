import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { authUser } from './_Models/authUser.model';
import { User } from './_Models/user.model';
import { AccountService } from './_Services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  // styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  users: User[];

  constructor(private http:HttpClient, private accountService:AccountService){}

  ngOnInit() {
    this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser(){
    const user:authUser = JSON.parse(localStorage.getItem('user'))
    this.accountService.setCurrentUser(user);
  }

  getUsers(){
    this.http
    .get<User[]>('https://localhost:5001/api/users')
    .subscribe(response => this.users = response, error => console.log(error))
  }
}
