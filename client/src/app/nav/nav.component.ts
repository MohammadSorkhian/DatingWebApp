import { stringify } from '@angular/compiler/src/util';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { authUser } from '../Models/authUser.model';
import { AccountService } from '../Services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  // styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit, OnDestroy {

  LoginForm:FormGroup; 
  subscription1:Subscription;
  subscription2:Subscription;
  currentUser:authUser;
  // CurrentUser:Observable<authUser>;

  constructor(private accountService:AccountService) { }



  ngOnInit(): void {

    this.LoginForm = new FormGroup({
      'userName': new FormControl(null, Validators.required),
      'passWord': new FormControl(null, Validators.required),
    });

    this.subscription1 =  this.accountService.currentUserSubject
      .subscribe(res => {
        this.currentUser = res;
      });
  }


  Login(){
    let username:string = this.LoginForm.controls['userName'].value;
    let password:string = this.LoginForm.controls['passWord'].value;

    this.subscription2 = this.accountService
    .login(username, password)
    .subscribe(error => console.log(error));
  }


  logout(){
    this.accountService.logout();
  }


  ngOnDestroy(){
    this.subscription1.unsubscribe()
    this.subscription2.unsubscribe()
  }
  
}
