import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '@angular/shared';
import { Router } from '@angular/router';

@Component({
  selector: 'angular-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {

  constructor(
    private authService: AuthService, 
    private router: Router){
      console.log("this is dashboard");
  }

  async ngOnInit() {
    await this.authService.completeAuthentication()
  }
}
