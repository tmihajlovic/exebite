import { Injectable } from '@angular/core';
import { User, UserManager, UserManagerSettings } from 'oidc-client';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private manager = new UserManager(getClientSettings());
  private user: User = null;

  constructor() {
    this.manager.getUser().then(
      user => {
        this.user = user;
      }
    );
  }

  isLoggedIn(): boolean {
    return this.user != null && !this.user.expired;
  }

  getClaims(): any {
    return this.user.profile;
  }

  getAuthorizationHeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }

  startAuthentication(): Promise<void> {
    return this.manager.signinRedirect();
  }

  completeAuthentication(): Promise<void> {
    return this.manager.signinRedirectCallback().then(
      user => {
        this.user = user;
      }
    );
  }

  logout() {
    this.manager.signoutRedirect({ id_token_hint: this.user.id_token });
  }

}

export function getClientSettings(): UserManagerSettings {
  return {
    authority: 'https://localhost:5001/',
    client_id: 'Exebite.ClientApp',
    redirect_uri: 'http://localhost:4200/auth-callback',
    post_logout_redirect_uri: 'http://localhost:4200/',
    response_type: "id_token token",
    scope: "openid",
    filterProtocolClaims: true,
    loadUserInfo: true
  };

}
