import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

constructor(private http: HttpClient) { } // injecting http client module into constructor
login(model: any) {
  // post(address, method, will besent to the body, options)
  return this.http.post(this.baseUrl + 'login', model) // returns an observable. you need to subscribe to it.
    .pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }
      })
    );
}
  register(model: any) { // model is an object that will store username and password
    return this.http.post(this.baseUrl + 'register', model); // localhost:5000/api/auth/register
    // 'post' method will return an observable so you need to 'subscribe' to it inside the component you need to use.
    // in our case. the 'register' component
  }
  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token); // this will return false if token is invalid.
  }

} // class


