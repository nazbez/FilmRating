import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from "../shared/services/authentication.service";
import { Router } from "@angular/router";

@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
    isExpanded = false;
    isUserAuthenticated: boolean;
    isUserAdmin: boolean;
    
    constructor(private authService: AuthenticationService, private router: Router) {
        this.authService.authChanged
            .subscribe(res => {
                this.isUserAuthenticated = res;
            });

        this.authService.isAdminChanged
            .subscribe(res => {
                this.isUserAdmin = res;
            });
    }
    
    ngOnInit(): void {
        this.authService.authChanged
            .subscribe(res => {
                this.isUserAuthenticated = res;
            });

        this.authService.isAdminChanged
            .subscribe(res => {
                this.isUserAdmin = res;
            });
    }

    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }

    public logout = () => {
        this.authService.logout();
        this.router.navigateByUrl('/login', {skipLocationChange: true}).then(() => {
            this.router.navigate(["/"]);
        });
    }
}
