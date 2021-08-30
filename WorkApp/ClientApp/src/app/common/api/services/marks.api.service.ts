import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Mark } from "../../interfaces/mark.interface";

@Injectable()
export class MarksApiService
{
    url: string;

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string){
        this.url = baseUrl;
        this.url += 'marks';
    }

    getMark() {
        return this.http.get<Mark[]>(this.url);
    }

    getMarkById(id: number) {
        return this.http.get<Mark>(this.url + '/' + id);
    }

    //get mark by DateTime

    createMark(mark: Mark) {
        var body = JSON.stringify(mark);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions
        });
    }

    updateMark(mark: Mark) {
        var body = JSON.stringify(mark);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions
        });
    }

    deleteMark(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}