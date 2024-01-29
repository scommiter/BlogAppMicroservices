import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [DashboardComponent],
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forChild([
      {
        path: '',
        component: DashboardComponent
      }
    ])
  ]
})
export class DashboardModule { }
