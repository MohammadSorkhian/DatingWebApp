import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { authUser } from 'src/app/_Models/authUser.model';
import { Member } from 'src/app/_Models/member';
import { Photo } from 'src/app/_Models/photo';
import { AccountService } from 'src/app/_Services/account.service';
import { MembersService } from 'src/app/_Services/members.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  @Input()
  member:Member
  uploader:FileUploader;
  hasBaseDropZoneOver:boolean = false;
  baseUrl:string = environment.apiUrl;
  user: authUser;

  constructor(private accountService:AccountService,
              private memberService:MembersService) {
    this.accountService.currentUserSubject
    .pipe(take(1))
    .subscribe( user => this.user = user);
   }

  ngOnInit(): void {
    this.initializeUploader();
    // this.uploader = new FileUploader({
    //   url:this.baseUrl+"users/add-photo"
    // });
  }

  fileOverBase(e:any){
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader(){
    this.uploader = new FileUploader({
      url:this.baseUrl + 'users/add-photo',
      authToken:'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType:['image'],
      removeAfterUpload: true,
      autoUpload:false,
      maxFileSize: 10 * 1024 * 1024
    })
    this.uploader.onAfterAddingAll = (file) => {
      file.withcredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response){
        const photo = JSON.parse(response);
        this.member.photo.push(photo);
        if( photo.isMain){
          this.member.photoUrl = photo.url;
          this.member.photoUrl = photo.url;
          this.accountService.setCurrentUser(this.user);
        }
      }
    }
  }

  setMainPhoto(photo: Photo) {
    this.memberService.setMainPicture(photo.id).subscribe((res) => {
      console.log(res)
      this.user.photoUrl = photo.url;
      this.accountService.setCurrentUser(this.user);
      this.member.photoUrl = photo.url;
      this.member.photo.forEach(p => {
        if (p.isMain) p.isMain = false;
        if (p.id === photo.id) p.isMain = true;
      })
    })
  }

  deletePhoto(photo:Photo){
    this.memberService.deletePhoto(photo.id).subscribe( (res) => {
      var wasMain = photo.isMain? true: false;
      var photoIndex = this.member.photo.indexOf(photo)
      this.member.photo.splice(photoIndex,1);
      if(wasMain) {
        this.member.photo[0].isMain = true;
        this.member.photoUrl = this.member.photo[0].url;
      }
    }, err => console.log(err))
  }

}
