export interface UserFilmRatingInfoModel {
    hasRate: boolean;
    rate: number | null;
    filmId: number;
    isFavorite: boolean;
}