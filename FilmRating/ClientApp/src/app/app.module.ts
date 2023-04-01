import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RegisterComponent } from "./register/register.component";
import { LoginComponent } from "./login/login.component";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorHandlerService } from "./shared/services/error-handler.service";
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuard } from "./shared/guards/auth.guard";
import { AdminGuard } from "./shared/guards/admin.guard";

export function tokenGetter() {
    return localStorage.getItem("token");
}

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        RegisterComponent,
        LoginComponent
    ],
    imports: [
        BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            {
                path: '', 
                loadChildren: () => import('./film/film.module').then(m => m.FilmModule),
                pathMatch: 'full'
            },
            {path: 'register', component: RegisterComponent},
            {path: 'login', component: LoginComponent},
            {
                path: 'artist-management',
                loadChildren: () => import('./artist-management/artist-management.module').then(m => m.ArtistManagementModule),
                canActivate: [AuthGuard, AdminGuard]
            }
        ]),
        JwtModule.forRoot({
            config: {
                tokenGetter: tokenGetter,
                allowedDomains: ["film-rating.local.com:7057", "film-rating.local.com:5095" ],
                disallowedRoutes: []
            }
        }),
        ReactiveFormsModule,
        MatCheckboxModule,
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: ErrorHandlerService,
            multi: true
        }],
    bootstrap: [AppComponent]
})
export class AppModule { }