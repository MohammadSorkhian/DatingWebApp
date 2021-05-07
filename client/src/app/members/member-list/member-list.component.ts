import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_Models/member';
import { MembersService } from 'src/app/_Services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  // styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  members:Member[];

  constructor(private memebrsService:MembersService) { }

  ngOnInit(): void {
    this.loadMembers();
  }
  
  loadMembers(){
    this.memebrsService.getMembers().subscribe( res => this.members = res)
  }

}
