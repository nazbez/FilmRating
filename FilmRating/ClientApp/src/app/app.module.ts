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
import { environment } from "../environments/environment";
import { FooterComponent } from "./footer/footer.component";
import { FilmInfoComponent } from "./film/film-info/film-info.component";
import { NgOptimizedImage } from "@angular/common";
import { MatButtonModule } from "@angular/material/button";
import { ConfirmationDialogComponent } from "./confirmation-dialog/confirmation-dialog.component";
import { MatDialogModule } from "@angular/material/dialog";
import { ManageFilmFormComponent } from "./film/manage-film-form/manage-film-form.component";
import { MatIconModule } from "@angular/material/icon";
import { MatSelectModule } from "@angular/material/select";

export function tokenGetter() {
    return localStorage.getItem("token");
}

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        RegisterComponent,
        LoginComponent,
        FooterComponent,
        FilmInfoComponent,
        ConfirmationDialogComponent,
        ManageFilmFormComponent
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
            },
            {
                path: 'film/:id',
                component: FilmInfoComponent
            }
        ]),
        JwtModule.forRoot({
            config: {
                tokenGetter: tokenGetter,
                allowedDomains: [environment.apiUrl],
                disallowedRoutes: []
            }
        }),
        ReactiveFormsModule,
        MatCheckboxModule,
        NgOptimizedImage,
        MatButtonModule,
        MatDialogModule,
        MatIconModule,
        MatSelectModule,
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