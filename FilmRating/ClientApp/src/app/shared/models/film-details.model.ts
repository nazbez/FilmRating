import { GenreModel } from "./genre.model";
import { ArtistModel } from "./artist.model";

export interface FilmDetailsModel 
{
    id: number,
    title: string,
    year: number,
    shortDescription: string,
    budget: number,
    duration: number,
    rating: number,
    genre: GenreModel,
    director: ArtistModel,
    actors: ArtistModel[],
    photoPath: string
}