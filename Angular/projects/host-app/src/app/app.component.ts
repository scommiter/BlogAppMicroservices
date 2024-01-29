import { Component, OnInit } from '@angular/core';
import { User } from 'oidc-client-ts';
import { AuthLibService } from 'auth-lib';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'host-app';
  public userAuthenticated = false;
  constructor(private authService: AuthLibService){
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
    console.log(this.authService.getToken)
  }
}
