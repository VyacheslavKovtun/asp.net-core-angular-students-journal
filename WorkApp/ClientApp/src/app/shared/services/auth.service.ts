import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, of } from "rxjs";

import { tap } from 'rxjs/operators';
import { Router } from "@angular/router";
import { environment } from '../../../environments/environment';
import jwt_decode from 'jwt-decode';
import { JwtHelperService } from "@auth0/angular-jwt";


@Injectable()
export class AuthService {
  readonly TOKEN_NAME = 'access_token';

  isUserAuth$ = new BehaviorSubject<boolean>(false);

  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private jwtHelperService: JwtHelperService
  ) {
    this.isUserAuth$.next(Boolean(localStorage.getItem(this.TOKEN_NAME)))
  }


  login(username: string, password: string) {
    return this.httpClient.post<{ access_token: string, username: string }>
    (
      [environment.API_URL, 'auth', 'login'].join('/'),
      { username, password }
    ).pipe(tap(res => {
      // запись данных в LocalStorage
      localStorage.setItem(this.TOKEN_NAME, res.access_token);
      localStorage.setItem('username', res.username);
      // уведомление об авторизации пользователя
      this.isUserAuth$.next(true);
    }));
  }

  logout() {
    // TODO: Server logout
    return of([]).pipe(tap(() => {
      // очистка данных в LocalStorage
      localStorage.removeItem(this.TOKEN_NAME);
      localStorage.removeItem('username');
      // уведомление об выходе пользователя
      this.isUserAuth$.next(false);
      this.router.navigate(['/login']);
    }))
  }


  isUserAuth(): boolean {
    const token = localStorage.getItem(this.TOKEN_NAME);
    if (token && !this.jwtHelperService.isTokenExpired(token)) {
      return true;
    }
    this.logout().subscribe();
    return false;
  }
}
