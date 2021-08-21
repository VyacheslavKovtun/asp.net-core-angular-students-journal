import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, of } from "rxjs";

import { tap } from 'rxjs/operators';
import { Router } from "@angular/router";
import { environment } from '../../../environments/environment';
import jwt_decode from 'jwt-decode';
import { JwtHelperService } from "@auth0/angular-jwt";
import { UsersApiService } from "src/app/common/api/services/users.api.service";
import { AuthRole, User } from "src/app/common/interfaces/user.interface";


@Injectable()
export class AuthService {
  readonly TOKEN_NAME = 'access_token';

  isUserAuth$ = new BehaviorSubject<boolean>(false);
  isAdminAuth$ = new BehaviorSubject<boolean>(false);
  isEditorAuth$ = new BehaviorSubject<boolean>(false);
  userId: number;

  constructor(private httpClient: HttpClient, private router: Router, private jwtHelperService: JwtHelperService, private usersApiService: UsersApiService) {
    const role = localStorage.getItem('role');
    if(role === AuthRole.Admin.toString()) {
      this.isAdminAuth$.next(true);
    }
    else {
      this.isUserAuth$.next(Boolean(localStorage.getItem(this.TOKEN_NAME)))
      if(this.isUserAuth$) {
        this.userId = Number.parseInt(localStorage.getItem('id'));
      }
    }
  }

  login(login: string, password: string) {
    return this.httpClient.post<{ access_token: string, login: string, role: AuthRole, userId: number }>
    (
      [environment.API_URL, 'auth', 'login'].join('/'),
      { login, password }
    ).pipe(tap(res => {
      localStorage.setItem(this.TOKEN_NAME, res.access_token);
      localStorage.setItem('login', res.login);
      localStorage.setItem('id', res.userId.toString());
      localStorage.setItem('role', res.role.toString());
      
      if(res.role == AuthRole.User) {
        this.isUserAuth$.next(true);
        this.isAdminAuth$.next(false);
        this.isEditorAuth$.next(false);
        this.userId = res.userId;
      }
      if(res.role == AuthRole.Admin) {
        this.isAdminAuth$.next(true);
        this.isUserAuth$.next(false);
        this.isEditorAuth$.next(false);
      }
      if(res.role == AuthRole.Editor) {
        this.isEditorAuth$.next(true);
        this.isAdminAuth$.next(false);
        this.isUserAuth$.next(false);
      }
    }));
  }

  logout() {
    return of([]).pipe(tap(() => {
      localStorage.removeItem(this.TOKEN_NAME);
      localStorage.removeItem('login');
      localStorage.removeItem('id');
      localStorage.removeItem('role');
      
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
