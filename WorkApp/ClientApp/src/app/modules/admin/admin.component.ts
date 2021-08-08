import {Component, OnInit} from '@angular/core';
import {Observable, of} from 'rxjs';

export interface Student {
  id: number,
  name: string,
  marks: number[]
}

@Component({
  selector: 'admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit{
  title = 'app';

  //TODO: insert after testing
  // students$: Observable<Student[]> = of([]);

  displayedColumns: string[] = ['id', 'name'];
  students: Student[] = [];
  clickedRow: Student;

  ngOnInit(): void {
    this.students = [
      {
        id: 1,
        name: 'Vasya',
        marks: [10, 12, 9, 11]
      },{
        id: 2,
        name: 'Masha',
        marks: [12, 10, 11, 11]
      },{
        id: 3,
        name: 'Petya',
        marks: [5, 7, 9, 2]
      },
    ]
  }
}
