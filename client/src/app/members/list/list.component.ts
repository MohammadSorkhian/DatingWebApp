import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_Models/member';
import { MembersService } from 'src/app/_Services/members.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  // styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  members: Partial<Member[]>;
  predicate: string = "liked";

  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    this.LoadLikes()
  }

  LoadLikes() {
    console.log(this.predicate)
    this.memberService
      .getLikes(this.predicate)
      .subscribe((memberList) => this.members = memberList)
  }
}