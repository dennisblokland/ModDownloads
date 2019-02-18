// auth.guard.ts
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { UserService } from './user.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private user: UserService, private router: Router, public jwtHelper: JwtHelperService) {}

  canActivate() {
    const token = localStorage.getItem('auth_token');
    if (!this.user.isLoggedIn() || this.jwtHelper.isTokenExpired(token))
    {
      this.user.logout();
       this.router.navigate(['login']);
       return false;
    }

    return true;
  }
}
