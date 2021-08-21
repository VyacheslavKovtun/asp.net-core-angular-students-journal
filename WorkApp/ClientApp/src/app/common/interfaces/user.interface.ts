import { Mark } from "./mark.interface";

export enum AuthRole {
    User,
    Editor,
    Admin
}

export interface User
{
    id: number,
    login: string,
    password: string,
    firstName: string,
    lastName: string,
    age: number,
    group: string,
    course: number,
    marks: Mark[]
}