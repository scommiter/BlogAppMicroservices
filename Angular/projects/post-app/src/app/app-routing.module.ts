import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostComponent } from './post/post.component';
import { PostDetailComponent } from './post-detail/post-detail.component';

const routes: Routes = [
  { path: '', component: PostComponent },
  // { 
  //   path: 'post/detail', 
  //   pathMatch: 'full',
  //   loadChildren: () => 
  //     import('./post-detail/post-detail.module').then(
  //       (m) => m.PostDetailModule
  //     )
  // },
  { path: '/post/detail', component: PostDetailComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
