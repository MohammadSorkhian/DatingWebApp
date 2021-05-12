import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Member } from 'src/app/_Models/member';
import { MembersService } from 'src/app/_Services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  // styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  members:Member[];

  constructor(private memebrsService:MembersService, private spinnerService:NgxSpinnerService) { }

  ngOnInit(): void {
    this.loadMembers();
  }
  
  loadMembers(){
    this.spinnerService.show();
    this.memebrsService.getMembers().toPromise().then( res => {
      this.members = res;
      this.spinnerService.hide()
    }, err => this.spinnerService.hide())
  }

}
