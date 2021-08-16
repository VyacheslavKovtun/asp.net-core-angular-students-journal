import { Mark } from "./mark.interface";

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