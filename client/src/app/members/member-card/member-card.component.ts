import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_Models/member';
import { MembersService } from 'src/app/_Services/members.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {

  @Input()
  member:Member;

  constructor(private memberService:MembersService, private toaster:ToastrService) { }

  ngOnInit(): void {
  }

  addLike(member:Member){
    this.memberService.addLike(member.userName).subscribe( () => {
      this.toaster.success(member.knownAs + " has been added to your Following list")
    }, (err) => {this.toaster.error(err.error)})

  }
}
