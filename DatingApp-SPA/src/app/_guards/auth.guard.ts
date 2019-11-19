import { Injectable } from '@angular/core';
import { CanActivate, UrlTree, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { AlertifyService } from '../services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authservice: AuthService, private router: Router, private alertify: AlertifyService) {}

  canActivate(): boolean | UrlTree {
    if (this.authservice.loggedIn()) {
      return true;
    }
    this.alertify.error('You aint supposed to be here');
    this.router.navigate(['/home']);
    return false;
  }
}
