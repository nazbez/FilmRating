import { NgModule } from "@angular/core";
import { ArtistManagementComponent } from "./artist-management.component";
import { RouterModule } from "@angular/router";
import { ArtistTableComponent } from "./artist-table/artist-table.component";
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from "@angular/material/dialog";
import { MatInputModule } from "@angular/material/input";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { NgForOf, NgIf } from "@angular/common";
import { ManageArtistComponent } from "./manage-artist/manage-artist.component";
import { MatSortModule } from "@angular/material/sort";
import { MatPaginatorModule } from "@angular/material/paginator";

@NgModule({
    declarations: [ ArtistManagementComponent, ArtistTableComponent, ManageArtistComponent ],
    exports: [ ArtistTableComponent ],
    imports: [
        MatTableModule,
        MatDialogModule,
        RouterModule.forChild([
            {path: '', component: ArtistManagementComponent},
        ]),
        MatInputModule,
        FormsModule,
        MatButtonModule,
        MatIconModule,
        MatCheckboxModule,
        NgForOf,
        NgIf,
        ReactiveFormsModule,
        MatSortModule,
        MatPaginatorModule
    ]
})
export class ArtistManagementModule { }