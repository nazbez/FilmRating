import { NgModule } from "@angular/core";
import { FilmComponent } from "./film.component";
import { RouterModule } from "@angular/router";
import { FilmTableComponent } from "./film-table/film-table.component";
import { MatInputModule } from "@angular/material/input";
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSortModule } from "@angular/material/sort";
import { MatIconModule } from "@angular/material/icon";
import { NgIf } from "@angular/common";
import { MatButtonModule } from "@angular/material/button";

@NgModule({
    declarations: [ FilmComponent, FilmTableComponent ],
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
    ]
})
export class FilmModule {}