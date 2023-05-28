import { Component, OnInit } from "@angular/core";
import { AuthenticationService } from "../shared/services/authentication.service";

@Component({
    selector: 'app-film',
    templateUrl: './film.component.html',
    styleUrls: ['./film.component.css']
})
export class FilmComponent implements OnInit {
    isUserAdmin: boolean;
    isUserAuthenticated: boolean;
    
    constructor(private authService: AuthenticationService)  {}

    ngOnInit(): void {
        this.isUserAdmin = this.authService.isUserAdmin();
        this.isUserAuthenticated = this.authService.isUserAuthenticated();
    }
}