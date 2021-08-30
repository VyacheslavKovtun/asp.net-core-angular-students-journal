import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { UsersApiService } from '../common/api/services/users.api.service';
import { AuthRole, User } from '../common/interfaces/user.interface';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  logInForm !: FormGroup;
  registerForm !: FormGroup;
  user: User = null;

  constructor(private authService: AuthService, private usersApiService: UsersApiService, private router: Router) { }

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
        this.router.navigate(['/']);
        alert('You are welcome!');
      });
    }
  }

  onBtnCheckInFormClick() {
    if (this.registerForm.valid) {
      const { login, password, firstName, lastName, age, group, course } = this.registerForm.value;
      
      this.usersApiService.getUserByLoginData(login, password).subscribe(u => {
        this.user = u;

        if(this.user == null) {
          this.user = {
            id: 0,
            login: login,
            password: password,
            role: AuthRole.User,
            firstName: firstName,
            lastName: lastName,
            age: age,
            group: group,
            course: course,
            marks: null
          }
  
          this.usersApiService.createUser(this.user).subscribe(data => {
            console.log('User was created: ' + data);
            this.router.navigate(['/']);
          });
  
          alert('User was created');
        }
        else {
          alert('User already exists!');
        }
      });
    }
  }
}
