import {Component, OnInit} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { UsersApiService } from 'src/app/common/api/services/users.api.service';
import { User } from 'src/app/common/interfaces/user.interface';

@Component({
  selector: 'admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  title = 'app';

  updateForm !: FormGroup;

  student: User;
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'group'];
  students$: Observable<User[]>;
  clickedRow: User;

  constructor(private usersApiService: UsersApiService) {

  }

  ngOnInit() {
    this.loadUsers();

    this.updateForm = new FormGroup({
      "login": new FormControl('', Validators.required),
      "password": new FormControl('', Validators.required),
      "firstName": new FormControl('', Validators.required),
      "lastName": new FormControl('', Validators.required),
      "age": new FormControl(17, [Validators.required, Validators.min(17), Validators.max(24)]),
      "group": new FormControl('', Validators.required),
      "course": new FormControl(1, [Validators.required, Validators.min(1), Validators.max(4)])
    });
  }

  onBtnUpdateFormClick() {
    if (this.updateForm.valid) {
      const { login, password, firstName, lastName, age, group, course } = this.updateForm.value;

      this.student = {
        id: this.clickedRow.id,
        login: login,
        password: password,
        role: this.clickedRow.role,
        firstName: firstName,
        lastName: lastName,
        age: age,
        group: group,
        course: course,
        marks: this.clickedRow.marks
      }

      this.clickedRow = null;
      this.save();
      this.updateForm.reset();
    }
  }

  onBtnDeleteClick() {
    this.delete(this.clickedRow);
    this.clickedRow = null;
  }

  loadUsers() {
    this.students$ = this.usersApiService.getUsers();
  }

  save() {
    if (this.student.id == null) {
        this.usersApiService.createUser(this.student).subscribe(data => {
          this.loadUsers();
        });
    } else {
        this.usersApiService.updateUser(this.student).subscribe(data => {
          this.loadUsers();
        });
    }
  }

  editUser(user: User) {
    this.student = user;
  }

  delete(user: User) {
    this.usersApiService.deleteUser(user.id).subscribe(data => {
      this.loadUsers();
    });
  }
}