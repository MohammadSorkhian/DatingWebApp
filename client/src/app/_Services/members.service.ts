import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { delay, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_Models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  members:Member[] = [];
  baseUrl = environment.apiUrl;
  // header = new HttpHeaders({
  //   Authorization: "Bearer " + JSON.parse(localStorage.getItem('user')).token
  // })

  constructor(private http:HttpClient) { }

  getMembers() {
    if (this.members.length > 0) {
      return of(this.members);
    }
    return this.http.get<Member[]>(this.baseUrl + "users").pipe(
      delay(500),
      tap(m => this.members = m))
    // return this.http.get<Member[]>(this.baseUrl + "users", {headers:this.header})
  }

  getMember(username: string) {
    if (this.members.length > 0)
      return of(this.members.find(m => m.userName === username))

    return this.http.get<Member>(this.baseUrl + "users/" + username)
    // return this.http.get<Member>(this.baseUrl + "users/" + username , {headers:this.header})
  }

  updateMember(member:Member){
    return this.http.put(this.baseUrl + "users", member).pipe(
      tap( () => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    )
  }

    setMainPicture(id:number){
    return this.http.get(this.baseUrl + "users/set-main-photo/" + id)
  }

  deletePhoto(photoId:number){
    return this.http.delete(this.baseUrl + "users/delete-photo/" + photoId)

  }

  addLike(userName:string){
    return this.http.post(this.baseUrl + 'likes/' + userName, {})
  }

  getLikes(predicate:string ){
    return this.http.get<Member[]>(this.baseUrl + 'likes?predicate=' + predicate);
  }

}
