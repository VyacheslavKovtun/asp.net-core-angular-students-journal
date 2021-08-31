export enum Subject {
    Math,
    English,
    Chemistry,
    Physics,
    PE,
    History,
    Literature
}

export interface Mark
{
    id: number,
    sMark: number,
    dateTime: number,
    subject: Subject,
    userId: number
}