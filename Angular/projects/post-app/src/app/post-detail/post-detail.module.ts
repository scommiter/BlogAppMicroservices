import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PostDetailComponent } from './post-detail.component';
import { ConverterPipe } from '../pipes/convert.pipe';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: 'post/detail',
        component: PostDetailComponent
      }
    ])
  ]
})
export class PostDetailModule { }
