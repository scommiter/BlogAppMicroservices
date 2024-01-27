import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService, NavigationComponent } from '@angular/shared';
import { DashboardComponent } from './dashboard/dashboard.component';

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
  constructor(private authService: AuthService){

  }

  ngOnInit(): void {
  }
}
