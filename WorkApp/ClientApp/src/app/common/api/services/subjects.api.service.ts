import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Subject } from "../../interfaces/subject.interface";

@Injectable()
export class SubjectsApiService
{
    url: string;

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string){
        this.url = baseUrl;
        this.url += 'subjects';
    }

    getSubjects() {
        return this.http.get<Subject[]>(this.url);
    }

    getSubjectById(id: number) {
        return this.http.get<Subject>(this.url + '/' + id);
    }

    createSubject(subject: Subject) {
        var body = JSON.stringify(subject);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions
        });
    }

    updateSubject(subject: Subject) {
        var body = JSON.stringify(subject);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions
        });
    }

    deleteSubject(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}