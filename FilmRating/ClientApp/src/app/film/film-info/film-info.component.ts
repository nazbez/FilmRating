import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router} from "@angular/router";
import { FilmService } from "../../shared/services/film.service";
import { FilmDetailsModel } from "../../shared/models/film-details.model";
import { AuthenticationService } from "../../shared/services/authentication.service";
import { ConfirmationDialogComponent } from "../../confirmation-dialog/confirmation-dialog.component";
import { MatDialog } from "@angular/material/dialog";
import { ManageFilmFormComponent } from "../manage-film-form/manage-film-form.component";
import { ArtistModel } from "../../shared/models/artist.model";

@Component({
    selector: 'app-film-info',
    templateUrl: './film-info.component.html',
    styleUrls: ['./film-info.component.css']   
})
export class FilmInfoComponent implements OnInit {
    filmDetails: FilmDetailsModel;
    id: number;
    isUserAdmin: boolean;
    isUserAuthenticated: boolean;
    isLoading: boolean = true;
    
    constructor(
        private activateRoute: ActivatedRoute,
        private filmService: FilmService,
        private authService: AuthenticationService,
        private dialog: MatDialog,
        private router: Router) {
        this.id = activateRoute.snapshot.params['id'];
    }

    ngOnInit(): void {
        this.isUserAdmin = this.authService.isUserAdmin();
        this.isUserAuthenticated = this.authService.isUserAuthenticated();
        
        this.filmService.get(this.id).subscribe(f => {
            this.filmDetails = f;
            this.isLoading = false;
        });
    }
    
    showActors(actors: ArtistModel[]): string {
        return actors
            .map(a => `${a.firstName} ${a.lastName}`)
            .join(',');
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
            this.filmService.get(this.id).subscribe(f => {
                this.filmDetails = f;
                this.isLoading = false;
            });
        });
    }
}