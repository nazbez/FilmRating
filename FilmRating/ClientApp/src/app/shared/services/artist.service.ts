import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ArtistModel, ArtistRoleModel } from "../models/artist.model";
import { CreateArtistModel } from "../models/create-artist.model";

@Injectable({
    providedIn: 'root'
})
export class ArtistService {
    constructor(private http: HttpClient) { }

    public create = (newArtist: CreateArtistModel) => {
        return this.http.post<ArtistModel>('api/Artist', newArtist);
    }
    
    public delete = (id: string) => {
        return this.http.delete('api/Artist/' + id);
    }
    
    public getAll = () => {
        return this.http.get<ArtistModel[]>('api/Artist/All');
    }
    
    public getRoles = () => {
        return this.http.get<ArtistRoleModel[]>('api/Artist/Roles');
    }
}