import { Injectable } from '@angular/core';
import { User, UserManager, UserManagerSettings } from 'oidc-client-ts';
import { Constants } from '../../shared/constants';
import { ReplaySubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  private _userManager: UserManager;
  private _user!: User;

  private _loginChangedSubject = new Subject<boolean>();
  //when the user’s status changes, inform component 
  public loginChanged = this._loginChangedSubject.asObservable();
  
  constructor() {
    this._userManager = new UserManager(this.idpSettings);
   }

  public login = () => {
    return this._userManager.signinRedirect();
  }

  async signout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null!);
    await this._userManager.signoutRedirect();
  }

  public isAuthenticated = (): Promise<boolean> => {
    return this._userManager.getUser()
    .then(user => {
      if(this._user !== user){
        this._loginChangedSubject.next(this.checkUser(user!));
      }
  
      this._user = user!;
        
      return this.checkUser(user!);
    })
  }

  async completeAuthentication() {
    this._user = await this._userManager.signinRedirectCallback();
    this.setCurrentUser(this._user)
  }
  
  private checkUser = (user : User): boolean => {
    return !!user && !user.expired;
  }

  private get idpSettings() : UserManagerSettings {
    return {
      authority: Constants.idpAuthority,// url identity server
      client_id: Constants.clientId,
      redirect_uri: `${Constants.clientRoot}/dash-board`,
      post_logout_redirect_uri: Constants.clientRoot,
      response_type: 'code',
      scope: 'openid profile email address roles userAPI postAPI notificationAPI chatAPI',
      filterProtocolClaims: true,
      loadUserInfo: true,
      automaticSilentRenew: true,
      silent_redirect_uri: `${Constants.clientRoot}/silent-refresh.html`
    };
  }

  setCurrentUser(user: User){
    if(user){  
      this._user = user    
      localStorage.setItem('user', JSON.stringify(user));
      this.currentUserSource.next(user); 
    }
  }

  getUsername(): string{
    return this.getDecodedToken().sub
  }

  private getDecodedToken() {
    return JSON.parse(atob(this._user.access_token.split('.')[1]));
  }
}
