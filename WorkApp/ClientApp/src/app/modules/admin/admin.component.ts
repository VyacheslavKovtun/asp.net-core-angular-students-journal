import {Component, OnInit} from '@angular/core';
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

  student: User;
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'group'];
  students$: Observable<User[]>;
  clickedRow: User;

  constructor(private usersApiService: UsersApiService) {

  }

  ngOnInit() {
    this.loadUsers();
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