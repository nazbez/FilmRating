import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { AuthenticationService } from "../services/authentication.service";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class GuestGuard implements CanActivate {
    constructor(private authService: AuthenticationService, private router: Router) {}

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (!this.authService.isUserAuthenticated()) {
            return true;
        }
        
        this.router.navigate(['/'], { queryParams: { returnUrl: state.url }});

        return false;
    }
}