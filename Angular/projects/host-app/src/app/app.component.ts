import { Component, OnInit } from '@angular/core';
import { AuthLibService } from 'auth-lib';
import { ChatBoxService } from 'shared-lib';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  title = 'host-app';
  public userAuthenticated = false;
  showFederatedComponent: boolean = false;

  constructor(
    private authService: AuthLibService,
    public chatBoxService: ChatBoxService
  ) {
    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  ngOnInit(): void {
    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
    this.authService.setUser();
    console.log('check', this.chatBoxService.closeChat$);
    this.chatBoxService.closeChat$.subscribe(() => {
      console.log('Đã nhận tín hiệu đóng!');
      this.showFederatedComponent = false;
    });
  }

  openFederatedComponent() {
    this.showFederatedComponent = true;
  }

  onChatClose() {
    console.log('Chat closed');
    this.showFederatedComponent = false;
  }
}
