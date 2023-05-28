export interface CreateFilmModel {
    title: string,
    year: number,
    shortDescription: string,
    budget: number,
    durationInMinutes: number,
    genreId: number,
    directorId: string,
    actorsIds: string[],
    photo: File
}