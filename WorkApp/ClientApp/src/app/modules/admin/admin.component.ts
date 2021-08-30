import {Component, OnInit} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { MarksApiService } from 'src/app/common/api/services/marks.api.service';
import { SubjectsApiService } from 'src/app/common/api/services/subjects.api.service';
import { UsersApiService } from 'src/app/common/api/services/users.api.service';
import { Mark } from 'src/app/common/interfaces/mark.interface';
import { Subject } from 'src/app/common/interfaces/subject.interface';
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
  subjects: Subject[];
  clickedRow: User;

  pMarks = [1,2,3,4,5,6,7,8,9,10,11,12];
  newMarkSelection = false;
  selectedMark: number;
  mark: Mark;

  constructor(private usersApiService: UsersApiService, private subjectsApiService: SubjectsApiService, private marksApiService: MarksApiService) {

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

  putSelectedMark(subject: Subject) {
    this.newMarkSelection = false;
  
    this.mark = {
      id: 0,
      sMark: Number.parseInt(this.selectedMark.toString()),
      dateTime: new Date().getTime(),
      subject: subject
    };

    console.log(this.mark);

    // this.marksApiService.createMark(this.mark).subscribe(m => {
    //   if(this.clickedRow.marks != null) {
    //     this.clickedRow.marks.push(this.mark);
    //   } else {
    //     this.clickedRow.marks = [this.mark];
    //   }
  
    //   this.editUser(this.clickedRow);
    //   console.log(this.student);
    //   this.save();
    // });
  }

  marksOfSubject(subjectId: number) {
    var marks = this.clickedRow.marks;
    if(marks != null) {
      return marks.find(m => m.subject.id == subjectId);
    }
  }

  loadUsers() {
    this.students$ = this.usersApiService.getUsers();
  }

  loadSubjects() {
    this.subjectsApiService.getSubjects().subscribe(sub =>{
      this.subjects = sub;
    });
  }

  save() {
    if (this.student.id == null) {
        this.usersApiService.createUser(this.student).subscribe(data => {
          console.log('create');
          this.loadUsers();
        });
    } else {
        this.usersApiService.updateUser(this.student).subscribe(data => {
          console.log('update');
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