import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  logInForm !: FormGroup;
  registerForm !: FormGroup;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.logInForm = new FormGroup({
      "login": new FormControl('', Validators.required),
      "password": new FormControl('', Validators.required)
    });
    
    this.registerForm = new FormGroup({
      "login": new FormControl('', Validators.required),
      "password": new FormControl('', Validators.required),
      "firstName": new FormControl('', Validators.required),
      "lastName": new FormControl('', Validators.required),
      "age": new FormControl(17, [Validators.required, Validators.min(17), Validators.max(24)]),
      "group": new FormControl('', Validators.required),
      "course": new FormControl(1, [Validators.required, Validators.min(1), Validators.max(4)])
    });
  }

  onBtnLogInFormClick() {
    if (this.logInForm.valid) {
      const { login, password } = this.logInForm.value;
      this.authService.login(login, password).subscribe(() => {
        this.router.navigate(['/'])
      });
    }
  }

  onBtnCheckInFormClick() {

  }
}
