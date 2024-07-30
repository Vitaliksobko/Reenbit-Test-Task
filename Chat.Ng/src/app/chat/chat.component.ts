import { AfterViewChecked, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ChatService } from '../../services/chat.service';
import { Router } from '@angular/router';
import { LocalService } from '../../services/local.service';
import { MessageModel } from '../../models/message.model';
import { TextSentiment, sentimentToString } from '../../enums/textSentiment'; 
import { UserModel } from '../../models/user.model';


@Component({
  selector: 'app-test-component',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'] 
})
export class TestComponentComponent implements OnInit, AfterViewChecked {
  TextSentiment = TextSentiment; 

  constructor(
    private chatService: ChatService,
    private router: Router,
    private localService: LocalService,

    
  ) {
    let token = this.localService.get(LocalService.AuthId);
    if (token) {
      this.loggedUserName = token;
    }
   
  }

  errorMessage = "";
  inputedMessage = "";
  receivedMessage: string = '';
  users: UserModel[] = [];
  messages: MessageModel[] = [];
  loggedUserName = "";
  isLoading = true;

  @ViewChild('scroll') private scrollContainer!: ElementRef;

  getSentiment(sentiment: TextSentiment) {
    return sentimentToString(sentiment);
  }

  sendMessage() {
    if (this.inputedMessage != "") {
      this.chatService.sendMessage(this.inputedMessage)
        .then(() => {
          this.inputedMessage = "";
        }).catch((err) => {
          console.log(err);
        });
    }
  }

  leaveChat() {
    this.chatService.leaveChat()
      .then(() => {
        this.router.navigate(["login"])
        this.localService.remove(LocalService.AuthId);
      }).catch((err) => {
        console.log(err);
      });
  }

  private subscribeToSignalR(): void {
    this.chatService.messages$.subscribe(data => this.messages = data);
    this.chatService.connectedUsers$.subscribe(data => this.users = data);
  }

  ngAfterViewChecked(): void {
    this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
  }

  ngOnInit(): void {
    const id = this.localService.get(LocalService.AuthId);
    if (id) {
      this.chatService.joinRoom(id)
        .then(() => {
          this.subscribeToSignalR();
          this.isLoading = false;
        })
    } else {
      this.errorMessage = "Please log in.";
      this.isLoading = false;
    }
  }
}
