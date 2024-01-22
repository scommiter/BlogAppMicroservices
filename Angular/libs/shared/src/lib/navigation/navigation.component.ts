import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth/auth.service';

@Component({
  selector: 'angular-navigation',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.scss',
})
export class NavigationComponent {
  constructor(public authService: AuthService) {
    
  }

  async login(){
    await this.authService.login()
    //sau khi login xong se redict toi AuthCallbackComponent. luc do navigateto neewfeed    
  }

  async logout(){
    await this.authService.signout()    
  }
}
