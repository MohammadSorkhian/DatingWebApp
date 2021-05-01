import { stringify } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../Services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  // styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  LoginForm:FormGroup; 
  loggedIn:boolean;

  constructor(private accountService:AccountService) { }


  ngOnInit(): void {

    this.LoginForm = new FormGroup({
      'userName': new FormControl(null, Validators.required),
      'passWord': new FormControl(null, Validators.required),
    });
  }


  Login(){
    let username:string = this.LoginForm.controls['userName'].value;
    let password:string = this.LoginForm.controls['passWord'].value;
    this.accountService
    .login(username, password)
    .subscribe(response=>{console.log(response)},
    error => console.log(error)
    );

    
  }
  
}
