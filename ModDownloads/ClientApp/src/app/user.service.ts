import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private headers: HttpHeaders;
  private accessPointUrl: string = window.location.origin + "/api/users";
  private loggedIn = false;


  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    this.loggedIn = !!localStorage.getItem('auth_token');
  }
  public isLoggedIn() {
    return this.loggedIn;
  }
  public logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
  }
  public login(email, password) {

    return this.http
      .post(
        this.accessPointUrl + '/login',
        JSON.stringify({ email, password }), { headers: this.headers }
      )
      .pipe(map(res => {
        localStorage.setItem('auth_token', res["token"]);
        this.loggedIn = true;
        return true;
      }));
  }

}
