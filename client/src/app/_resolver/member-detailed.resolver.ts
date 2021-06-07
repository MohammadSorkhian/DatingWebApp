import { Injectable } from '@angular/core';
import {Resolve, ActivatedRouteSnapshot} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Member } from '../_Models/member';
import { MembersService } from '../_Services/members.service';

@Injectable({
  providedIn: 'root'
})
export class MemberDetailedResolver implements Resolve<Member> {

  constructor(private memberService:MembersService){}

  resolve(route: ActivatedRouteSnapshot): Observable<Member> {
    var username:string = route.params['Username']
    return this.memberService.getMember(username)
  }
}
