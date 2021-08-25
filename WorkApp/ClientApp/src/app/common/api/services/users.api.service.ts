import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { User } from "../../interfaces/user.interface";

@Injectable()
export class UsersApiService
{
    url: string;

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string){
        this.url = baseUrl;
        this.url += 'users';
    }

    getUsers() {
        return this.http.get<User[]>(this.url);
    }

    getUserById(id: number) {
        return this.http.get<User>(this.url + '/' + id);
    }

    getUserByLoginData(login: string, password: string) {
        return this.http.get<User>(this.url + '/' + login + '&' + password);
    }

    createUser(user: User) {
        var body = JSON.stringify(user);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions
        });
    }

    updateUser(user: User) {
        return this.http.put(this.url, user);
    }

    deleteUser(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}