import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService, NavigationComponent } from '@angular/shared';
import { DashboardComponent } from './dashboard/dashboard.component';
import { User } from 'oidc-client-ts';

@Component({
  standalone: true,
  imports: [
    NavigationComponent, 
    DashboardComponent,
    RouterModule],
  selector: 'angular-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit  {
  title = 'user';
  public userAuthenticated = false;
  constructor(private authService: AuthService){
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
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user')!);
    console.log("userLocalStorage", user)
    if (user) {
      this.authService.setCurrentUser(user);
    }
  }
}
