import { Route } from '@angular/router';
import { NewsComponent } from './news/news.component';

export const appRoutes: Route[] = [
    { 
        path: '',
        children: [
          { path: 'news', component: NewsComponent, pathMatch: 'full' },
        ]
      }
];
