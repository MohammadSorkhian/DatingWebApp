import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './user.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  // styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  users: User[];

  constructor(private http:HttpClient){}

  ngOnInit() {
    this.getUsers();
  }

  getUsers(){
    this.http
    .get<User[]>('https://localhost:5001/api/users')
    .subscribe(response => this.users = response, error => console.log(error))
  }
}
