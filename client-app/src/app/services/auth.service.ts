import { Injectable } from '@angular/core';
import { User, UserManager, UserManagerSettings, Profile } from 'oidc-client';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private manager = new UserManager(getClientSettings());

  private async getUser(): Promise<User> {
    return await this.manager.getUser();
  }

  async isLoggedIn(): Promise<boolean> {
    const user = await this.getUser();
    return user != null && !user.expired;
  }

  async getClaims(): Promise<Profile> {
    const user = await this.getUser();
    return user.profile;
  }

  async getAuthorizationHeaderValue(): Promise<string> {
    const user = await this.getUser();
    return `${user.token_type} ${user.access_token}`;
  }

  startAuthentication(): Promise<void> {
    return this.manager.signinRedirect();
  }

  completeAuthentication(): Promise<User> {
    return this.manager.signinRedirectCallback();
  }

  async logout(): Promise<void> {
    if (await this.isLoggedIn()) {
      const user = await this.getUser();
      this.manager.signoutRedirect({ id_token_hint: user.id_token });
    }
  }

}

export function getClientSettings(): UserManagerSettings {
  return {
    authority: environment.identityServerBaseUrl,
    client_id: 'Exebite.ClientApp',
    redirect_uri: `${environment.portalBaseUrl}/auth-callback`,
    post_logout_redirect_uri: environment.portalBaseUrl,
    response_type: 'id_token token',
    scope: 'openid',
    filterProtocolClaims: true,
    loadUserInfo: true
  };

}
