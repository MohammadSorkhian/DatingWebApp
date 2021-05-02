import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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

  constructor(private accountService:AccountService, private toastr:ToastrService) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      "username" : new FormControl(null, Validators.required),
      "password" : new FormControl(null, Validators.required)
    })
  }

  register(){
    let username:string = this.registerForm.controls['username'].value;
    let password:string = this.registerForm.controls['password'].value;
    this.accountService.registe(username, password).subscribe( res => {
      console.log(res);
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

}
