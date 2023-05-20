import { Component } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { AddFilmFormComponent } from "../add-film-form/add-film-form.component";

@Component({
    selector: 'app-add-film',
    templateUrl: './add-film.component.html',
    styleUrls: ['./add-film.component.css']
})
export class AddFilmComponent {

    constructor(public dialog: MatDialog) {
    }
    
    createNewFilm() {
        const dialogRef = this.dialog.open(AddFilmFormComponent, {
            width: '600px',
            data: undefined
        });

        dialogRef.afterClosed().subscribe(result => {
        });
    }
}