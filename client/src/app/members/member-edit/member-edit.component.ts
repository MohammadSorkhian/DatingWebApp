import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { take } from 'rxjs/operators';
import { authUser } from 'src/app/_Models/authUser.model';
import { Member } from 'src/app/_Models/member';
import { AccountService } from 'src/app/_Services/account.service';
import { MembersService } from 'src/app/_Services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  member:Member;
  user:authUser;
  profileForm: FormGroup;

  constructor(private memberServices:MembersService, private accountService:AccountService) {
    this.accountService.currentUserSubject.pipe(take(1)).subscribe( authUser => this.user = authUser)
    // console.log(this.user)
   }

  ngOnInit(): void {
    this.memberServices.getMember(this.user.userName)
      .toPromise().then((re) => {
        this.member = re;
        console.log(re)
        this.createProfileForm()
      }
      )
  }

  createProfileForm(){
    this.profileForm = new FormGroup({
      "introduction": new FormControl(this.member.introduction),
      "lookingFor": new FormControl(this.member.lookingFor),
      "interests": new FormControl(this.member.interests),
      "city": new FormControl(this.member.city),
      "country": new FormControl(this.member.country),
    })
  }


}
