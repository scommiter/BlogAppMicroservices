import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NewsComponent } from './news/news.component';
import { AuthService, NavigationComponent } from '@angular/shared';
import { User } from 'oidc-client-ts';

@Component({
  standalone: true,
  imports: [
    NewsComponent, 
    NavigationComponent,
    RouterModule
  ],
  selector: 'angular-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'post';
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
