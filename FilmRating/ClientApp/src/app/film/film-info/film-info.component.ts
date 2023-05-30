import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router} from "@angular/router";
import { FilmService } from "../../shared/services/film.service";
import { FilmDetailsModel } from "../../shared/models/film-details.model";
import { AuthenticationService } from "../../shared/services/authentication.service";
import { ConfirmationDialogComponent } from "../../confirmation-dialog/confirmation-dialog.component";
import { MatDialog } from "@angular/material/dialog";
import { ManageFilmFormComponent } from "../manage-film-form/manage-film-form.component";
import { ArtistModel } from "../../shared/models/artist.model";
import { RateFilmComponent } from "../rate-film/rate-film.component";
import { RatingUserRateModel } from "../../shared/models/rating-user-rate.model";
import { RatingService } from "../../shared/services/rating.service";

@Component({
    selector: 'app-film-info',
    templateUrl: './film-info.component.html',
    styleUrls: ['./film-info.component.css']   
})
export class FilmInfoComponent implements OnInit {
    filmDetails: FilmDetailsModel;
    id: number;
    isUserAdmin: boolean;
    isUserCritic: boolean;
    isUserAuthenticated: boolean;
    isLoading: boolean = true;
    photoPath: string;
    rating: number;
    ratingUserRate: RatingUserRateModel;
    timeStamp: number;
    
    constructor(
        private activateRoute: ActivatedRoute,
        private filmService: FilmService,
        private authService: AuthenticationService,
        private ratingService: RatingService,
        private dialog: MatDialog,
        private router: Router) {
        this.id = activateRoute.snapshot.params['id'];
    }

    ngOnInit(): void {
        this.isUserAdmin = this.authService.isUserAdmin();
        this.isUserCritic = this.authService.isUserCritic();
        this.isUserAuthenticated = this.authService.isUserAuthenticated();
        
        if (this.isUserCritic && this.isUserAuthenticated) {
            this.ratingService.getUserFilmRate(this.id)
                .subscribe(r => {
                    this.ratingUserRate = r;
                });
        }
        
        this.filmService.get(this.id).subscribe(f => {
            this.filmDetails = f;
            this.rating = f.rating;
            this.setLinkPicture(f.photoPath);
            this.isLoading = false;
        });
    }
    
    showActors(actors: ArtistModel[]): string {
        return actors
            .map(a => `${a.firstName} ${a.lastName}`)
            .join(', ');
    }

    deleteFilm() {
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
            width: '250px',
            data: 'Delete film'
        });

        dialogRef.afterClosed().subscribe(res => {
            if (res) {
                this.filmService.delete(this.id)
                    .subscribe(_ => this.router.navigate(['/']));
            }
        });
    }

    update() {
        const dialogRef = this.dialog.open(ManageFilmFormComponent, {
            width: '600px',
            data: this.filmDetails
        });

        dialogRef.afterClosed().subscribe(result => 
        {
            if (typeof result === 'object') {
                this.filmDetails = result;
                this.setLinkPicture(result.photoPath);
                this.isLoading = false; 
            }
        });
    }

    getLinkPicture() {
        if(this.timeStamp) {
            return this.photoPath + '?' + this.timeStamp;
        }
        
        return this.photoPath;
    }

    setLinkPicture(url: string) {
        this.photoPath = url;
        this.timeStamp = (new Date()).getTime();
    }

    rateFilm() {
        const dialogRef = this.dialog.open(RateFilmComponent, {
            width: '600px',
            data: this.ratingUserRate
        });

        dialogRef.afterClosed().subscribe(result =>
        {
            if (typeof result === 'number') {
                this.ratingUserRate.hasRate = true;
                this.ratingUserRate.rate = result;
                this.filmService.getRating(this.id)
                    .subscribe(r => this.rating = r);
                this.isLoading = false;
            }
        });
    }
}