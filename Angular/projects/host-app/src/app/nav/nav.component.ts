import { Component, ComponentFactoryResolver, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { AuthLibService } from 'auth-lib';
import { RemoteLoaderService } from '../../services/remote-loader.service';
import { REMOTE_PORT } from '../../shared/constants/remote.constant';
import { Router } from '@angular/router';
import { FederatedComponentService } from '../../services/federated.service';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss'
})
export class NavComponent implements OnInit{
  token!: any;
  @ViewChild('remoteContainer', { read: ViewContainerRef }) remoteContainer!: ViewContainerRef;
  
  constructor(
    public authService: AuthLibService,
    public remoteLoaderService: RemoteLoaderService,
    private router: Router,
    private componentFactoryResolver: ComponentFactoryResolver,
    private federatedComponentService: FederatedComponentService,
    private appComponent: AppComponent
    ) {
  }
  ngOnInit(): void {
    this.token = this.authService.getToken;
  }

  async login(){
    await this.authService.login()
    //after login, redict to AuthCallbackComponent
  }

  async logout(){
    await this.authService.signout()    
  }

  async register(){
      
  }

  showFederatedComponent(): void {
    // this.federatedComponentService.setShowFederatedComponent(true);
    this.appComponent.openFederatedComponent();
  }
  // loadRemoteModule(): void {
  //   this.remoteLoaderService.loadRemoteModule<any>('chatApp', './ChatBoxModule', REMOTE_PORT.CHAT_APP_URL)
  //     .then(module => {
  //       const componentFactory = this.componentFactoryResolver.resolveComponentFactory(module.ChatBoxComponent);
  //       this.remoteContainer.clear();
  //       this.remoteContainer.createComponent(componentFactory);
  //     })
  //     .catch(error => {
  //       console.error('Error loading remote module:', error);
  //     });
  // }

}
