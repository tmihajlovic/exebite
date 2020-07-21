import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(public authService: AuthService, public router: Router) { }

  async canActivate(): Promise<boolean> {
    let isLoggedIn = await this.authService.isLoggedIn();
    if (!isLoggedIn) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
}
