import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { FilmService } from "../../shared/services/film.service";
import { FilmDetailsModel } from "../../shared/models/film-details.model";

@Component({
    selector: 'app-film-info',
    templateUrl: './film-info.component.html',
    styleUrls: ['./film-info.component.css']   
})
export class FilmInfoComponent implements OnInit {
    filmDetails: FilmDetailsModel;
    id: number;
    isLoading: boolean = true;
    
    constructor(
        private activateRoute: ActivatedRoute,
        private filmService: FilmService) {
        this.id = activateRoute.snapshot.params['id'];
    }

    ngOnInit(): void {
        this.filmService.get(this.id).subscribe(f => {
            this.filmDetails = f;
            this.isLoading = false
        });
    }
}