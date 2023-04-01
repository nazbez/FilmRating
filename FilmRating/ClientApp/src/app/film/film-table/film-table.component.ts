﻿import { Component, OnInit, ViewChild } from "@angular/core";
import { FilmModel } from "../../shared/models/film.model";
import { MatTableDataSource } from "@angular/material/table";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { FilmService } from "../../shared/services/film.service";

@Component({
    selector: 'app-film-table',
    templateUrl: './film-table.component.html',
    styleUrls: ['./film-table.component.css']
})
export class FilmTableComponent implements OnInit {
    displayedColumns = ['title', 'year', 'genre', 'rating'];
    dataSource: MatTableDataSource<FilmModel>;

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    
    constructor(private filmService: FilmService) { }
    
    ngOnInit() {
        this.filmService.getAll()
            .subscribe(val => {
                this.dataSource = new MatTableDataSource(val);

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
}