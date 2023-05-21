import { Component, Inject, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { CreateFilmModel } from "../../shared/models/create-film.model";
import { FilmService } from "../../shared/services/film.service";
import { GenreService } from "../../shared/services/genre.service";
import { GenreModel } from "../../shared/models/genre.model";
import { ArtistService } from "../../shared/services/artist.service";
import { ArtistModel } from "../../shared/models/artist.model";
import { HttpErrorResponse } from "@angular/common/http";
import { FilmModel } from "../../shared/models/film.model";
import { FilmDetailsModel } from "../../shared/models/film-details.model";

@Component({
    selector: 'app-manage-film-form',
    templateUrl: './manage-film-form.component.html',
    styleUrls: ['./manage-film-form.component.css']
})
export class ManageFilmFormComponent implements OnInit {
    filmFormGroup: FormGroup;
    errorMessage: string = '';
    showError: boolean;
    genres: GenreModel[];
    directors: ArtistModel[];
    actors: ArtistModel[];
    years: number[];
    isUpdating: boolean = false;
    
    film: CreateFilmModel;

    constructor(
        public filmService: FilmService,
        public genreService: GenreService,
        public artistService: ArtistService,
        public dialogRef: MatDialogRef<ManageFilmFormComponent>,
        @Inject(MAT_DIALOG_DATA) public data: FilmDetailsModel) {
    }

    ngOnInit(): void {
        this.genreService.getAll()
            .subscribe((g) => {
                this.genres = g;
            });
        
        this.artistService.getDirectors()
            .subscribe((d) => {
                this.directors = d;
            })

        this.artistService.getActors()
            .subscribe((a) => {
                this.actors = a;
            })
        
        this.filmService.getYears()
            .subscribe((y) => {
                this.years = y
            })
        
        if (this.data !== undefined) {
            this.isUpdating = true;
        }
        
        this.filmFormGroup = new FormGroup({
            title: new FormControl(this.isUpdating ? this.data.title : '', [Validators.required]),
            year: new FormControl(this.isUpdating ? this.data.year : undefined, [Validators.required]),
            shortDescription: new FormControl(this.isUpdating ? this.data.shortDescription : '', [Validators.required]),
            budget: new FormControl(this.isUpdating ? this.data.budget : '', [Validators.required, Validators.min(0)]),
            durationInMinutes: new FormControl(this.isUpdating ? this.data.duration : '', [Validators.required, Validators.min(1)]),
            genre: new FormControl(this.isUpdating ? this.data.genre.id : undefined, [Validators.required, Validators.min(1)]),
            director: new FormControl(this.isUpdating ? this.data.director.id : '', [Validators.required]),
            actors: new FormControl(this.isUpdating ? this.data.actors.map(x => x.id) : [], [Validators.required]),
            photo: new FormControl('', this.isUpdating ? [] : [Validators.required]),
            photoSource: new FormControl('', this.isUpdating ? [] : [Validators.required])
        });
    }
    
    validateControl = (controlName: string) => {
        return this.filmFormGroup.get(controlName).invalid && this.filmFormGroup.get(controlName).touched
    }
    hasError = (controlName: string, errorName: string) => {
        return this.filmFormGroup.get(controlName).hasError(errorName)
    }
    
    save(value) {
        this.showError = false;

        if (this.isUpdating) {
            this.updateFilm(value);
        } else {
            this.createFilm(value);
        }
    }

    createFilm(value) {
        const createFilm = {... value };
        let photoValue = createFilm.photoSource;
        const formData = new FormData();
        formData.append('title', createFilm.title);
        formData.append('year', createFilm.year);
        formData.append('shortDescription', createFilm.shortDescription);
        formData.append('budget', createFilm.budget);
        formData.append('durationInMinutes', createFilm.durationInMinutes);
        formData.append('genreId', createFilm.genre);
        formData.append('directorId', createFilm.director);
        createFilm.actors.forEach(a => formData.append('actorIds', a));
        formData.append('photo', photoValue, photoValue.name);

        this.filmService.create(formData)
            .subscribe({
                next: (res: FilmModel) => this.dialogRef.close(res),
                error: (err: HttpErrorResponse) => console.log(err.error.errors)
            })
    }
    
    updateFilm(value) {
        const createFilm = {... value };
        let photoValue = createFilm.photoSource;
        const formData = new FormData();
        formData.append('title', createFilm.title);
        formData.append('year', createFilm.year);
        formData.append('shortDescription', createFilm.shortDescription);
        formData.append('budget', createFilm.budget);
        formData.append('durationInMinutes', createFilm.durationInMinutes);
        formData.append('genreId', createFilm.genre);
        formData.append('directorId', createFilm.director);
        createFilm.actors.forEach(a => formData.append('actorIds', a));
        if (photoValue) {
            formData.append('photo', photoValue, photoValue.name);
        }

        this.filmService.update(this.data.id, formData)
            .subscribe({
                next: (res: FilmModel) => 
                {
                    this.dialogRef.close(res);
                },
                error: (err: HttpErrorResponse) => console.log(err.error.errors)
            })
    }
    
    onFileChange(event) {
        if (event.target.files.length > 0) {
            const file = event.target.files[0];
            this.filmFormGroup.patchValue({
                photoSource: file
            });
        }
    }
}