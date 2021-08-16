import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/common/interfaces/user.interface';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  student: User;

  constructor() { }

  ngOnInit() {
    
  }
}
