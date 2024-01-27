import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NewsComponent } from './news/news.component';
import { AuthService, NavigationComponent } from '@angular/shared';

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
  constructor(private authService: AuthService){
  }

  ngOnInit(): void {
  }
}
