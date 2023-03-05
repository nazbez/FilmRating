import { Component, ViewChild } from "@angular/core";
import { ArtistModel, ArtistRoleModel } from "../../shared/models/artist.model";
import { ArtistService } from "../../shared/services/artist.service";
import { MatTable } from "@angular/material/table";
import { MatDialog } from "@angular/material/dialog";
import { ManageArtistComponent } from "../manage-artist/manage-artist.component";

@Component({
    selector: 'app-artist-table',
    templateUrl: './artist-table.component.html',
    styleUrls: ['./artist-table.component.css']
})
export class ArtistTableComponent {
    displayedColumns: string[] = ['fullName', 'jobs', 'updateItem', 'deleteItem'];
    artists: ArtistModel[];

    constructor(public artistService: ArtistService, public dialog: MatDialog) {
        artistService.getAll()
            .subscribe((a) => {
                this.artists = a;
            });
    }

    @ViewChild(MatTable) table: MatTable<ArtistModel>;

    createNewArtist() {
        const dialogRef = this.dialog.open(ManageArtistComponent, {
            width: '600px',
            data: undefined
        });

        dialogRef.afterClosed().subscribe(result => {
            if (typeof result === 'object') {
                this.artists.push(result);
                this.table.renderRows();
            }
        });
    }
    
    updateArtist(id: string) {
        let artist = this.artists.find(x => x.id === id);
        
        const dialogRef = this.dialog.open(ManageArtistComponent, {
            width: '600px',
            data: artist
        });

        dialogRef.afterClosed().subscribe(result => {
            if (typeof result === 'object') {
                this.artists = this.artists.filter(x => x.id !== id);
                this.artists.push(result);
                this.table.renderRows();
            }
        });
    }

    removeArtist(id: string) {
        this.artistService.delete(id)
            .subscribe(_ => {
                this.artists = this.artists.filter(x => x.id !== id);
                this.table.renderRows();
            })
    }
    
    mapRoles(roles: ArtistRoleModel[]) {
        if (roles.length === 0) 
            return '-'
        
        return roles.map(x => x.name).join(', ');
    }
}