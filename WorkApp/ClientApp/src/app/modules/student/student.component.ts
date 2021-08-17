import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersApiService } from 'src/app/common/api/services/users.api.service';
import { User } from 'src/app/common/interfaces/user.interface';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  studentId: number;
  student: User;

  constructor(private router: Router, private activatedRoute: ActivatedRoute, private usersApiService: UsersApiService) {
    this.activatedRoute.params.subscribe(p => {
      this.studentId = p.studentId;
      this.usersApiService.getUserById(this.studentId).subscribe(s => {
        this.student = s;
      });
    });
  }

  ngOnInit() {
  }
}
