import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { AppComponent } from '../../../../app.component';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss',
})
export class NavComponent implements OnInit {
  token!: any;
  constructor(
    public authService: AuthService,
    private router: Router,
    private appComponent: AppComponent
  ) {}
  ngOnInit(): void {
    this.token = this.authService.getToken;
  }

  async login() {
    await this.authService.login();
    //after login, redict to AuthCallbackComponent
  }

  async logout() {
    await this.authService.signout();
  }

  async register() {}
}
