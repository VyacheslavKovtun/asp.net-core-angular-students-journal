import { Subject } from "./subject.interface";

export interface Mark
{
    id: number,
    sMark: number,
    dateTime: number,
    subject: Subject
}