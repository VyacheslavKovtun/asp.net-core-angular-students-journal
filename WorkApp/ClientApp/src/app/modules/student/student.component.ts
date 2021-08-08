import { Component, OnInit } from '@angular/core';
import {Student} from '../admin/admin.component';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  student: Student;

  constructor() { }

  ngOnInit() {
    this.student = {
      id: 1,
      name: 'Vasya',
      marks: [12, 10, 11, 11]
    };
  }
}
