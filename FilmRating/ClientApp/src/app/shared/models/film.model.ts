import { GenreModel } from "./genre.model";
import { ArtistModel } from "./artist.model";

export interface FilmModel {
    id: number,
    title: string,
    year: number,
    rating: number
    photoPath: string,
    genre: GenreModel,
    director: ArtistModel,
    actors: ArtistModel[],
}