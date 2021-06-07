import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MessageService } from 'src/app/_Services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {

  @Input()
  username:string

  messages:Message[]

  sendMessageForm:FormGroup;

  messageContent: string;

  constructor(private messageService:MessageService) { 
  }
  
  ngOnInit(): void {
    this.loadMessagethread(this.username)

    this.sendMessageForm = new FormGroup({
      "content" : new FormControl("")
    })
  }
  
  loadMessagethread(userName){
    this.messageService.getMessageThread(userName).subscribe( mArr => {
      this.messages = mArr
    })
  }

  onMessageSend(){
    console.log(this.sendMessageForm)
    this.messageContent = this.sendMessageForm.value.content;
    this.messageService.sendMessage(this.username, this.messageContent).subscribe( res => console.log(res));
    this.sendMessageForm.reset();
  }

}
