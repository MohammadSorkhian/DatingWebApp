import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { interval } from 'rxjs';
import { Member } from 'src/app/_Models/member';
import { MembersService } from 'src/app/_Services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {

  member:Member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];


  constructor(private memberService:MembersService, private activatedRoute:ActivatedRoute) { }

  ngOnInit(): void {

    this.loadMember()

    this.galleryOptions = [
      {
        width:"500px",
        height:"500px",
        imagePercent: 100,
        thumbnailsColumns:4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview:false,
      }
    ]

  }


  getImages(){
    const imageUrls = [];
    for(let photo of this.member.photo){
      imageUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url,
      })
    }
    return imageUrls;
  }
  

  loadMember() {

    let userName: string;
    this.activatedRoute.params.subscribe(params => userName = params['Username'])

    this.memberService.getMember(userName).toPromise().then( (user) => {
      this.member = user;
      this.galleryImages = this.getImages()
    },
      (err) => console.log(err)
    )
  }

}
