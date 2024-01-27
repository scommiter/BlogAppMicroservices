import { Route } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import {canActivate} from '@angular/shared';

export const appRoutes: Route[] = [
    // { 
    //     path: '',
    //     children: [
    //       { path: 'dash-board', component: DashboardComponent, pathMatch: 'full' },
    //     ]
    // },
    {
      path: '',
      canActivate: [canActivate],
      children: [
        {path: 'dash-board', component: DashboardComponent}
      ]
    },
];
