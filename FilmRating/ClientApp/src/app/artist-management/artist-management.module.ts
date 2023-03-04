import { NgModule } from "@angular/core";
import { ArtistManagementComponent } from "./artist-management.component";
import { RouterModule } from "@angular/router";
import { ArtistTableComponent } from "./artist-table/artist-table.component";
import { MatTableModule } from '@angular/material/table';
import { AddArtistComponent } from "./add-artist/add-artist.component";
import { MatDialogModule } from "@angular/material/dialog";
import { MatInputModule } from "@angular/material/input";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { NgForOf, NgIf } from "@angular/common";

@NgModule({
    declarations: [ ArtistManagementComponent, ArtistTableComponent, AddArtistComponent ],
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
        ReactiveFormsModule
    ]
})
export class ArtistManagementModule {
    
}