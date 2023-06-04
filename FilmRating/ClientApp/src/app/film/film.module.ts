import { NgModule } from "@angular/core";
import { FilmComponent } from "./film.component";
import { RouterModule } from "@angular/router";
import { FilmTableComponent } from "./film-table/film-table.component";
import { MatInputModule } from "@angular/material/input";
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSortModule } from "@angular/material/sort";
import { MatIconModule } from "@angular/material/icon";
import { DecimalPipe, NgForOf, NgIf } from "@angular/common";
import { MatButtonModule } from "@angular/material/button";
import { MatDialogModule } from "@angular/material/dialog";
import { AddFilmComponent } from "./add-film/add-film.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatSelectModule } from "@angular/material/select";
import { RateFilmComponent } from "./rate-film/rate-film.component";
import { MatExpansionModule } from "@angular/material/expansion";

@NgModule({
    declarations: [ FilmComponent, FilmTableComponent, AddFilmComponent, RateFilmComponent ],
    exports: [ FilmComponent ],
    imports: [
        RouterModule.forChild([
            {path: '', component: FilmComponent},
        ]),
        MatInputModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatIconModule,
        NgIf,
        MatButtonModule,
        MatDialogModule,
        FormsModule,
        NgForOf,
        ReactiveFormsModule,
        MatSelectModule,
        DecimalPipe,
        MatExpansionModule,
    ]
})
export class FilmModule {}