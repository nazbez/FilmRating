﻿import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { FilmModel } from "../models/film.model";
import { FilmDetailsModel } from "../models/film-details.model";

@Injectable({
    providedIn: 'root'
})
export class FilmService {
    constructor(private http: HttpClient) { }

    public getAll = () => {
        return this.http.get<FilmModel[]>('api/Film/All');
    }
    
    public getMyFavourite = () => {
        return this.http.get<FilmModel[]>('api/Film/IsFavourite/All');
    }
    
    public get = (id: number) => {
        return this.http.get<FilmDetailsModel>(`api/Film/${id}`)
    }
    
    public getRating = (id: number) => {
        return this.http.get<number>(`api/Film/${id}/Rating`)
    }
    
    public delete = (id: number) => {
        return this.http.delete(`api/Film/${id}`);
    }
    
    public create = (form: FormData) => {
        return this.http.post("api/Film", form)
    }
    
    public update = (id: number, form: FormData) => {
        return this.http.put(`api/Film/${id}`, form);
    }
    
    public getYears = () => {
        return this.http.get<number[]>("api/Film/Years");
    }
}