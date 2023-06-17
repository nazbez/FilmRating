import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { UserFilmRatingInfoModel } from "../models/user-film-rating-info.model";
import { RatingCreateModel } from "../models/rating-create.model";
import { RatingUpdateModel } from "../models/rating-update.model";
import {RatingIsFavouriteUpdateModel} from "../models/rating-is-favourite-update.model";

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
    
    public updateIsFavorite = (model: RatingIsFavouriteUpdateModel) => {
        return this.http.put('api/Rating/IsFavourite', model);
    }
    
    public getUserFilmRatingInfo = (filmId: number) => {
        return this.http.get<UserFilmRatingInfoModel>(`api/Rating/Film/${filmId}/My/Info`);
    }
    
    public getOptions = () => {
        return this.http.get<number[]>('api/Rating/Options');
    }
}