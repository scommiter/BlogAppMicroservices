import { Component, OnInit } from '@angular/core';
import { AuthLibService } from 'auth-lib';
import { FederatedComponentService } from '../services/federated.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'host-app';
  public userAuthenticated = false;
  showFederatedComponent: boolean = false;

  constructor(
    private authService: AuthLibService,
    private federatedComponentService: FederatedComponentService){
    this.authService.loginChanged
    .subscribe(userAuthenticated => {
      this.userAuthenticated = userAuthenticated;
    })
  }

  ngOnInit(): void {
    this.authService.isAuthenticated()
    .then(userAuthenticated => {
      this.userAuthenticated = userAuthenticated;
    })
    this.authService.setUser();
  }

  openFederatedComponent() {
    this.showFederatedComponent = true;
  }

  closeFederatedComponent() {
    this.showFederatedComponent = false;
  }

  // get showFederatedComponent(): boolean {
  //   return this.federatedComponentService.showFederatedComponent$;
  // }
}
