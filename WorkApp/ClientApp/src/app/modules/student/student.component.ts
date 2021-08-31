import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MarksApiService } from 'src/app/common/api/services/marks.api.service';
import { UsersApiService } from 'src/app/common/api/services/users.api.service';
import { Mark, Subject } from 'src/app/common/interfaces/mark.interface';
import { User } from 'src/app/common/interfaces/user.interface';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  studentId: number;
  student: User;

  subjects: string[];

  userMarks: Mark[];
  subjectMarks: Mark[];

  constructor(private router: Router, private activatedRoute: ActivatedRoute, private usersApiService: UsersApiService, private marksApiService: MarksApiService) {
    this.activatedRoute.params.subscribe(p => {
      this.studentId = p.studentId;
      this.usersApiService.getUserById(this.studentId).subscribe(s => {
        this.student = s;

        this.marksApiService.getMarksByUserId(this.student.id).subscribe(m => {
          this.userMarks = m;
          this.student.marks = this.userMarks;
        });
      });
    });
  }

  ngOnInit() {
    this.loadSubjects();
  }

  loadSubjects() {
    this.subjects = ['Math','English', 'Chemistry', 'Physics', 'PE', 'History', 'Literature'];
  }

  getSubjectFromString(subjectString: string)
  {
    switch(subjectString)
    {
      case 'Math':
        return Subject.Math;
        break;
      case 'English':
        return Subject.English;
        break;
      case 'Chemistry':
        return  Subject.Chemistry;
        break;
      case 'Physics':
        return Subject.Physics;
        break;
      case 'PE':
        return Subject.PE;
        break;
      case 'History':
        return Subject.History;
        break;
      case 'Literature':
        return Subject.Literature;
        break;
    }
  }

  marksOfSubject(subject: string) {
    this.subjectMarks = [];

    if(this.userMarks != null) {
      this.userMarks.forEach(m => {
        if(m.subject == this.getSubjectFromString(subject)) {
          this.subjectMarks.push(m);
        }
      });
    }

    return this.subjectMarks;
  }
}
