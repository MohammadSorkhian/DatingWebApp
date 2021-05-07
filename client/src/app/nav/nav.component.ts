import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { authUser } from '../_Models/authUser.model';
import { AccountService } from '../_Services/account.service';

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

  constructor(private accountService:AccountService,
              private router:Router,
              private toaster:ToastrService,){}


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
    .subscribe( () => {
      this.router.navigateByUrl("/members");
    },
      err => {
        console.log(err);
        this.toaster.error(err.error);
      });
  }


  logout(){
    this.accountService.logout();
    this.router.navigateByUrl("/");
  }


  ngOnDestroy(){
    this.subscription1.unsubscribe()
    this.subscription2.unsubscribe()
  }
  
}
