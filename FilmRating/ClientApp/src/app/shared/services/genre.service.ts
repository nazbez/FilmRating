import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { GenreModel } from "../models/genre.model";

@Injectable({
    providedIn: 'root'
})
export class GenreService {
    constructor(private http: HttpClient) { }

    public getAll = () => {
        return this.http.get<GenreModel[]>('api/Genre/All');
    }
}