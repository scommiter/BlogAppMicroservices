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
  }

  async logout(){ 
  }

  async register(){
      
  }
}
