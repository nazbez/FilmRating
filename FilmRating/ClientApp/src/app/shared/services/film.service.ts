import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { FilmModel } from "../models/film.model";

@Injectable({
    providedIn: 'root'
})
export class FilmService {
    constructor(private http: HttpClient) { }

    public getAll = () => {
        return this.http.get<FilmModel[]>('api/Film/All');
    }
}