import { Component, OnInit, ViewChild } from "@angular/core";
import { ArtistModel, ArtistRoleModel } from "../../shared/models/artist.model";
import { ArtistService } from "../../shared/services/artist.service";
import { MatTableDataSource } from "@angular/material/table";
import { MatDialog } from "@angular/material/dialog";
import { ManageArtistComponent } from "../manage-artist/manage-artist.component";
import { ConfirmationDialogComponent } from "../../confirmation-dialog/confirmation-dialog.component";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";

@Component({
    selector: 'app-artist-table',
    templateUrl: './artist-table.component.html',
    styleUrls: ['./artist-table.component.css']
})
export class ArtistTableComponent implements OnInit {
    displayedColumns: string[] = ['fullName', 'jobs', 'updateItem', 'deleteItem'];
    dataSource: MatTableDataSource<ArtistModel>;

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;

    constructor(public artistService: ArtistService, public dialog: MatDialog) {}
    
    ngOnInit() {
        this.artistService.getAll()
            .subscribe((a) => {
                this.dataSource = new MatTableDataSource(a);

                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
            });
    }

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }

    sortData(sort: any) {
        const data = this.dataSource.data;
        if (!sort.active || sort.direction === '') {
            this.dataSource.data = data;
            return;
        }

        this.dataSource.data = data.sort((a, b) => {
            const isAsc = sort.direction === 'asc';
            switch (sort.active) {
                case 'fullName':
                    return this.compare(`${a.firstName} ${a.lastName}`, `${b.firstName} ${b.lastName}`, isAsc);
                case 'jobs':
                    return this.compare(this.mapRoles(a.roles), this.mapRoles(b.roles), isAsc);
                default:
                    return 0;
            }
        });
    }

    createNewArtist() {
        const dialogRef = this.dialog.open(ManageArtistComponent, {
            width: '600px',
            data: undefined
        });

        dialogRef.afterClosed().subscribe(result => {
            if (typeof result === 'object') {
                const newData = [ ...this.dataSource.data ];
                newData.push(result);
                this.dataSource.data = newData;
            }
        });
    }
    
    updateArtist(id: string) {
        let artist = this.dataSource.data.find(x => x.id === id);
        
        const dialogRef = this.dialog.open(ManageArtistComponent, {
            width: '600px',
            data: artist
        });

        dialogRef.afterClosed().subscribe(result => {
            if (typeof result === 'object') {
                const newData = [ ...this.dataSource.data ];
                let index = newData.findIndex(x => x.id === id);
                newData[index] = result;
                this.dataSource.data = newData;
            }
        });
    }

    removeArtist(id: string) {
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
            width: '250px',
            data: 'Delete artist'
        });

        dialogRef.afterClosed().subscribe(res => {
            if (res) {
                this.artistService.delete(id)
                    .subscribe(_ => {
                        this.dataSource.data = this.dataSource.data.filter(x => x.id !== id);
                    })
            }
        });
    }
    
    mapRoles(roles: ArtistRoleModel[]) {
        if (roles.length === 0) 
            return '-'
        
        return roles.map(x => x.name).join(', ');
    }

    private compare(a: number | string, b: number | string, isAsc: boolean) {
        return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
    }
}