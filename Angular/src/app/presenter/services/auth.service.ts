import { Injectable } from '@angular/core';
import { User, UserManager, UserManagerSettings } from 'oidc-client-ts';
import { PORT } from '../../core/constants/port.const';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _userManager: UserManager;
  private _user!: User;
  private _token!: any;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  private get idpSettings(): UserManagerSettings {
    return {
      authority: PORT.idpAuthority, // url identity server
      client_id: PORT.clientId,
      redirect_uri: `${PORT.clientHost}/home`,
      post_logout_redirect_uri: PORT.clientHost,
      response_type: 'code',
      scope:
        'openid profile email address roles userAPI postAPI notificationAPI chatAPI',
      filterProtocolClaims: true,
      loadUserInfo: true,
      automaticSilentRenew: true,
      silent_redirect_uri: `${PORT.clientHost}/silent-refresh.html`,
    };
  }

  constructor() {
    this._userManager = new UserManager(this.idpSettings);
  }

  public login = () => {
    return this._userManager.signinRedirect();
  };

  async signout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null!);
    await this._userManager.signoutRedirect();
  }

  async completeAuthentication() {
    this._user = await this._userManager.signinRedirectCallback();
    this.setCurrentUser(this._user);
  }

  setCurrentUser(user: User) {
    if (user) {
      this._user = user;
      localStorage.setItem('user', JSON.stringify(user));
      this.currentUserSource.next(user);
    }
  }

  public get getToken(): string {
    return this._token;
  }

  getUsername(): string | null {
    const decodedToken = this.getDecodedToken();
    return decodedToken?.sub ?? null;
  }

  private getDecodedToken() {
    return JSON.parse(atob(this._user.access_token.split('.')[1]));
  }
}
