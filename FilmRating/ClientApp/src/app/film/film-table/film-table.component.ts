import { Component, OnInit, ViewChild } from "@angular/core";
import { FilmModel } from "../../shared/models/film.model";
import { MatTableDataSource } from "@angular/material/table";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { FilmService } from "../../shared/services/film.service";
import { Router } from "@angular/router";
import { GenreModel } from "../../shared/models/genre.model";
import { GenreService } from "../../shared/services/genre.service";
import { ArtistModel } from "../../shared/models/artist.model";
import { ArtistService } from "../../shared/services/artist.service";

@Component({
    selector: 'app-film-table',
    templateUrl: './film-table.component.html',
    styleUrls: ['./film-table.component.css']
})
export class FilmTableComponent implements OnInit {
    displayedColumns = ['photo', 'title', 'year', 'genre', 'rating'];
    dataSource: MatTableDataSource<FilmModel>;
    films: FilmModel[];
    genres: GenreModel[];
    directors: ArtistModel[];
    actors: ArtistModel[];
    years: number[];
    
    currentGenreFilter: number[] = [];
    currentDirectorFilter: string[] = [];
    currentActorsFilter: string[] = [];
    minYearFilter: number;
    maxYearFilter: number;
    panelOpenState: boolean = false;

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    
    constructor(
        private filmService: FilmService,
        private router: Router,
        private genreService: GenreService,
        private artistService: ArtistService) { }
    
    ngOnInit() {
        this.filmService.getAll()
            .subscribe(val => {
                this.dataSource = new MatTableDataSource(val);
                this.films = [...val];

                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
            });
        
        this.genreService.getAll().subscribe(g => {
            this.genres = g;
        });
        
        this.artistService.getDirectors().subscribe(d => {
            this.directors = d;
        });

        this.artistService.getActors().subscribe(a => {
            this.actors = a;
        });
        
        this.filmService.getYears().subscribe(y => {
            this.years = y.reverse();
            this.minYearFilter = y[0];
            this.maxYearFilter = y[y.length-1];
        })
    }

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }

    openInfoPage(id) {
        this.router.navigate(["film", id]);
    }

    applyGenreFilter(event: any) {
        this.currentGenreFilter = event.value;
        
        this.applyAdditionalFilters();
    }

    applyDirectorsFilter(event: any) {
        this.currentDirectorFilter = event.value;

        this.applyAdditionalFilters();
    }

    applyActorsFilter(event: any) {
        this.currentActorsFilter = event.value;
        
        this.applyAdditionalFilters();
    }

    applyMinYearFilter(year: any) {
        this.minYearFilter = year;
        
        this.applyAdditionalFilters();
    }

    applyMaxYearFilter(year: any) {
        this.maxYearFilter = year;

        this.applyAdditionalFilters();
    }
    
    applyAdditionalFilters() {
        this.dataSource.data = this.films.filter(f => {
            return (this.currentGenreFilter.length === 0 || this.currentGenreFilter.includes(f.genre.id)) &&
                (this.currentDirectorFilter.length === 0 || this.currentDirectorFilter.includes(f.director.id)) &&
                (this.currentActorsFilter.length === 0 || this.currentActorsFilter.some(a => f.actors
                    .some(act => act.id === a))) &&
                f.year >= this.minYearFilter &&
                f.year <= this.maxYearFilter;
        })
    }
}