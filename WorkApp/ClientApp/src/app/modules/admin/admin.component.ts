import {Component, OnInit} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { MarksApiService } from 'src/app/common/api/services/marks.api.service';
import { UsersApiService } from 'src/app/common/api/services/users.api.service';
import { Mark, Subject } from 'src/app/common/interfaces/mark.interface';
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
  subjects: string[];
  clickedRow: User;

  pMarks = [1,2,3,4,5,6,7,8,9,10,11,12];
  newMarkSelection = false;
  selectedMark: number;
  markSubject: Subject;
  userMarks: Mark[];
  subjectMarks: Mark[];
  mark: Mark;
  savedMark: Mark;

  constructor(private usersApiService: UsersApiService, private marksApiService: MarksApiService) {

  }

  ngOnInit() {
    this.loadUsers();
    this.loadSubjects();

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

  onBtnPutMarkClick() {
    if(!this.newMarkSelection) {
      this.newMarkSelection = true;
    } else {
      this.newMarkSelection = false;
    }
  }

  putSelectedMark(subject: string) {
    this.newMarkSelection = false;
  
    this.markSubject = this.getSubjectFromString(subject);

    this.mark = {
      id: 0,
      sMark: Number.parseInt(this.selectedMark.toString()),
      dateTime: new Date().getTime(),
      subject: this.markSubject,
      userId: this.clickedRow.id
    };

    this.marksApiService.createMark(this.mark).subscribe(m => {
      this.marksApiService.getMarkByDateTime(this.mark.dateTime).subscribe(mark => {
        this.savedMark = mark;

        if(this.clickedRow.marks != null) {
          this.clickedRow.marks.push(this.savedMark);
        } else {
          this.clickedRow.marks = [this.savedMark];
        }
    
        this.editUser(this.clickedRow);
      });
    });
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

  selectUserClick() {
    this.marksApiService.getMarksByUserId(this.clickedRow.id).subscribe(m => {
      this.userMarks = m;
      this.clickedRow.marks = this.userMarks;
    });
  }

  loadUsers() {
    this.students$ = this.usersApiService.getUsers();
  }

  loadSubjects() {
    this.subjects = ['Math','English', 'Chemistry', 'Physics', 'PE', 'History', 'Literature'];
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