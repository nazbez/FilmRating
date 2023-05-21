﻿import { Component } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { AddFilmFormComponent } from "../add-film-form/add-film-form.component";
import { Router } from "@angular/router";

@Component({
    selector: 'app-add-film',
    templateUrl: './add-film.component.html',
    styleUrls: ['./add-film.component.css']
})
export class AddFilmComponent {

    constructor(public dialog: MatDialog, private router: Router) {
    }
    
    createNewFilm() {
        const dialogRef = this.dialog.open(AddFilmFormComponent, {
            width: '600px',
            data: undefined
        });

        dialogRef.afterClosed().subscribe(result => {
            this.router.navigate(['film', result.id])
        });
    }
}