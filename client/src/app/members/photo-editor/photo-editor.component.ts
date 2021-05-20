import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { authUser } from 'src/app/_Models/authUser.model';
import { Member } from 'src/app/_Models/member';
import { AccountService } from 'src/app/_Services/account.service';
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

  constructor(private accountService:AccountService) {
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
    console.log(this.user.token)
    this.uploader = new FileUploader({
      url:this.baseUrl + 'users/add-photo',
      authToken:'Bearer ' + this.user.token,
      // authToken:'Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJsaXNhIiwibmJmIjoxNjIxMDg4NTg3LCJleHAiOjE2MjE2OTMzODcsImlhdCI6MTYyMTA4ODU4N30.-tDf-dYeNMQm1A8PBqStbPZX9HAZnMZwE7qy4bG3B9MdxjzIuwrn7NLVm8LnV9NLv0dvWVxDRKvAnnztbW8J0A',
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
      }
    }
  }

}
