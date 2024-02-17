import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { loadRemoteModule } from '@angular-architects/module-federation';
import { REMOTE_PORT } from '../shared/constants/remote.constant';
import { AuthGuard } from '../../../auth-lib/src/lib/auth.guard';

const routes: Routes = [
  { path: 'login', component: AppComponent },
  { 
      path: 'home', 
      component: HomeComponent ,
      canActivate: [AuthGuard]
  },
  // another way to transfer data
  // {
  //   path: 'user-dashboard',
  //   loadChildren: () => 
  //     loadRemoteModule({
  //       remoteEntry: REMOTE_PORT.USER_APP_URL,
  //       remoteName: 'userApp',
  //       exposedModule: "./DashboardModule",
  //     }).then(m => m.DashboardModule).catch(err => console.log(err)),
  //     data: {
  //       inputText: JSON.parse(localStorage.getItem('user')!)
  //     }
  // },
  {
      path: 'user-dashboard',
      loadChildren: () => {
        return loadRemoteModule({
          remoteEntry: REMOTE_PORT.USER_APP_URL,
          remoteName: 'userApp',
          exposedModule: "./DashboardModule",
        }).then(m => m.DashboardModule).catch(err => console.log(err));
      }
  },
  {
    path: 'postApp',
    loadChildren: () => {
      return loadRemoteModule({
        remoteEntry: REMOTE_PORT.POST_APP_URL,
        remoteName: 'postApp',
        exposedModule: "./PostModule",
      }).then(m => m.PostModule).catch(err => console.log(err));
    }
  },
  {
    path: 'postApp',
    loadChildren: () => {
      return loadRemoteModule({
        remoteEntry: REMOTE_PORT.POST_APP_URL,
        remoteName: 'postApp',
        exposedModule: "./PostDetailModule",
      }).then(m => m.PostDetailModule).catch(err => console.log(err));
    }
  },
  {
    path: 'chatApp',
    loadChildren: () => {
      return loadRemoteModule({
        remoteEntry: REMOTE_PORT.CHAT_APP_URL,
        remoteName: 'chatApp',
        exposedModule: "ChatBoxModule",
      }).then(m => m.ChatBoxModule).catch(err => console.log(err));
    }
  },
  {
    path: 'chatApp',
    loadComponent: () => 
      loadRemoteModule({
        type: 'module',
        remoteEntry: REMOTE_PORT.CHAT_APP_URL,
        exposedModule: './ChatComponent'
      })
      .then(m => m.ChatComponent)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
