import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { Router } from '@angular/router';
import { AuthLibService } from 'auth-lib';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit{
  token!: any;
  @ViewChild('placeHolder', { read: ViewContainerRef })
  viewContainer!: ViewContainerRef;
  constructor(
    private authService: AuthLibService, 
    private router: Router){
  }

  async ngOnInit() {
    this.token = this.authService.getToken;
    await this.authService.completeAuthentication()
  }
}
