import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_Services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  // styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output()
  cancelRegister = new EventEmitter<void>();

  registerForm:FormGroup;

  constructor(
    private accountService:AccountService, 
    private toastr:ToastrService,
    private router:Router) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      "username" : new FormControl("", Validators.required),
      "password" : new FormControl("", [Validators.required, Validators.minLength(3)]),
      "PasswordConfirm" : new FormControl("", [
        Validators.required, 
        Validators.minLength(3),
        this.passwordConfirmValidator])
    })
    this.registerForm.controls.password.valueChanges.subscribe( () => {
      this.registerForm.controls.PasswordConfirm.updateValueAndValidity()
    })
  }

  register(){
    let username:string = this.registerForm.controls['username'].value;
    let password:string = this.registerForm.controls['password'].value;
    this.accountService.registe(username, password).subscribe( res => {
      console.log(res);
      this.router.navigateByUrl("/members")
      this.cancel();
    },
      err => {
        console.log(err);
        this.toastr.error(err.error)
      }
      )
  }

  cancel(){
    this.cancelRegister.emit();
  }

  passwordConfirmValidator(control: AbstractControl): { [key: string]: any } | null {
    const pass = control.parent?.controls['password'].value
    if (pass === control.value) {
      return null
    }
    else
      return { "passwordConfirm": false }
  }

}
