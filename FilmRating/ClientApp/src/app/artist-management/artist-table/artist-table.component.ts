import { Component, ViewChild } from "@angular/core";
import { ArtistModel, ArtistRoleModel } from "../../shared/models/artist.model";
import { ArtistService } from "../../shared/services/artist.service";
import { MatTable } from "@angular/material/table";
import { MatDialog } from "@angular/material/dialog";
import { AddArtistComponent } from "../add-artist/add-artist.component";

@Component({
    selector: 'app-artist-table',
    templateUrl: './artist-table.component.html',
    styleUrls: ['./artist-table.component.css']
})
export class ArtistTableComponent {
    displayedColumns: string[] = ['fullName', 'jobs', 'deleteItem'];
    artists: ArtistModel[];

    constructor(public artistService: ArtistService, public dialog: MatDialog) {
        artistService.getAll()
            .subscribe((a) => {
                this.artists = a;
            });
    }

    @ViewChild(MatTable) table: MatTable<ArtistModel>;

    createNewArtist() {
        const dialogRef = this.dialog.open(AddArtistComponent, {
            width: '600px',
        });

        dialogRef.afterClosed().subscribe(result => {
            if (typeof result === 'object') {
                this.artists.push(result);
                this.table.renderRows();
            }
        });
    }
    
    mapRoles(roles: ArtistRoleModel[]) {
        if (roles.length === 0) 
            return '-'
        
        return roles.map(x => x.name).join(', ');
    }

    removeArtist(id: string) {
        this.artistService.delete(id)
            .subscribe(_ => {
                this.artists = this.artists.filter(x => x.id !== id);
                this.table.renderRows();
        })
    }
}