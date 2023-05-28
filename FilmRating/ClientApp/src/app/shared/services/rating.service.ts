import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { RatingUserRateModel } from "../models/rating-user-rate.model";
import { RatingCreateModel } from "../models/rating-create.model";
import { RatingUpdateModel } from "../models/rating-update.model";

@Injectable({
    providedIn: 'root'
})
export class RatingService {
    constructor(private http: HttpClient) { }
    
    public create = (model: RatingCreateModel) => {
        return this.http.post('api/Rating', model);
    }

    public update = (model: RatingUpdateModel) => {
        return this.http.put('api/Rating', model);
    }
    
    public getUserFilmRate = (filmId: number) => {
        return this.http.get<RatingUserRateModel>(`api/Rating/Film/${filmId}/My`);
    }
    
    public getOptions = () => {
        return this.http.get<number[]>('api/Rating/Options');
    }
}