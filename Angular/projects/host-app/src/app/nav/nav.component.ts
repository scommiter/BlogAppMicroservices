import { Component, OnInit } from '@angular/core';
import { AuthLibService } from 'auth-lib';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss'
})
export class NavComponent implements OnInit{
  token!: any;
  
  constructor(public authService: AuthLibService) {
  }
  ngOnInit(): void {
    this.token = this.authService.getToken;
  }

  async login(){
    await this.authService.login()
    //after login, redict to AuthCallbackComponent
  }

  async logout(){
    await this.authService.signout()    
  }

  async register(){
      
  }
}
