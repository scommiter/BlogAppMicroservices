import { Route } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';

export const appRoutes: Route[] = [
    { 
        path: '',
        children: [
          { path: 'dash-board', component: DashboardComponent, pathMatch: 'full' },
        ]
      }
];
