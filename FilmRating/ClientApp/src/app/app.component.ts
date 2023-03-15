import { Component } from '@angular/core';
import { AuthenticationService } from "./shared/services/authentication.service";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
})
export class AppComponent {
    title = 'app';

    constructor(private authService: AuthenticationService) { }

    ngOnInit(): void {
        if (this.authService.isUserAuthenticated())
            this.authService.sendAuthStateChangeNotification(true);
    }
}