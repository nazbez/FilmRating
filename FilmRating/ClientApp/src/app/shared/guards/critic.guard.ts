import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { AuthenticationService } from "../services/authentication.service";

@Injectable({
    providedIn: 'root'
})
export class CriticGuard  implements CanActivate {
    constructor(private authService: AuthenticationService, private router: Router) {}

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if(this.authService.isUserCritic())
            return true;

        this.router.navigate(['/'], { queryParams: { returnUrl: state.url }});

        return false;
    }
}