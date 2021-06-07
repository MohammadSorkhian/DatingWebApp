import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, OnInit } from '@angular/core';
import { MessageService } from 'src/app/_Services/message.service';

@Component({
  selector: 'app-messages-component',
  templateUrl: './messages-component.component.html',
  // styleUrls: ['./messages-component.component.css']
})
export class MessagesComponentComponent implements OnInit {

  messages:Partial<Message[]> = [];
  container: string = "outbox";

  constructor(private messageService:MessageService) { 
  }
  
  ngOnInit(): void {
    this.loadMessages()
  }

  loadMessages(){
    this.messageService.getMessages(this.container)
    .subscribe( messageList => this.messages = messageList);
  }

  deleteMessage(id:number){
    var mesgIndex = this.messages.findIndex( m => +m.id === id)
    this.messageService.deleteMessage(id).subscribe( () => {
      this.messages.splice(mesgIndex, 1);
    });
  }

}
