import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, of } from "rxjs";

import { tap } from 'rxjs/operators';
import { Router } from "@angular/router";
import { environment } from '../../../environments/environment';
import jwt_decode from 'jwt-decode';
import { JwtHelperService } from "@auth0/angular-jwt";

export enum AuthRole {
  User,
  Editor,
  Admin
}

@Injectable()
export class AuthService {
  readonly TOKEN_NAME = 'access_token';

  isUserAuth$ = new BehaviorSubject<boolean>(false);
  isAdminAuth$ = new BehaviorSubject<boolean>(false);
  isEditorAuth$ = new BehaviorSubject<boolean>(false);

  constructor(private httpClient: HttpClient, private router: Router, private jwtHelperService: JwtHelperService) {
    this.isUserAuth$.next(Boolean(localStorage.getItem(this.TOKEN_NAME)))
  }

  login(login: string, password: string) {
    return this.httpClient.post<{ access_token: string, login: string, role: AuthRole }>
    (
      [environment.API_URL, 'auth', 'login'].join('/'),
      { login, password }
    ).pipe(tap(res => {
      localStorage.setItem(this.TOKEN_NAME, res.access_token);
      localStorage.setItem('login', res.login);
      
      if(res.role == AuthRole.User) {
        this.isUserAuth$.next(true);
      }
      if(res.role == AuthRole.Admin) {
        this.isAdminAuth$.next(true);
      }
      if(res.role == AuthRole.Editor) {
        this.isEditorAuth$.next(true);
      }
    }));
  }

  logout() {
    return of([]).pipe(tap(() => {
      localStorage.removeItem(this.TOKEN_NAME);
      localStorage.removeItem('login');
      
      this.isUserAuth$.next(false);
      this.isAdminAuth$.next(false);
      this.isEditorAuth$.next(false);
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
