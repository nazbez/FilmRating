﻿import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { MatDialog } from "@angular/material/dialog";
import { ErrorDialogComponent } from "../../error-dialog/error-dialog.component";

@Injectable({
    providedIn: 'root'
})
export class ErrorHandlerService implements HttpInterceptor {
    constructor(private router: Router, private dialog: MatDialog) { }
    
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    let errorMessage = this.handleError(error);
                    return throwError(() => new Error(errorMessage));
                })
            )
    }

    private handleError = (error: HttpErrorResponse) : string => {
        if (error.status === 404) {
            return this.handleNotFound(error);
        }
        else if (error.status === 400){
            return this.handleBadRequest(error);
        }
        else if (error.status === 403) {
            return this.handleForbidden();
        }
    }
    
    private handleNotFound = (error: HttpErrorResponse): string => {
        this.router.navigate(['/']);
        return error.message;
    }
    
    private handleBadRequest = (error: HttpErrorResponse): string => {
        if (this.router.url === '/register' || this.router.url === '/login'){
            let message = '';
            const values = Object.values(error.error.errorMessages);
            values.map((m: string) => {
                message += m + '<br>';
            })
            return message.slice(0, -4);
        }
        if (this.router.url.startsWith('/film')) {
            return this.handleNotFound(error);
        }
        else {
            this.dialog.open(ErrorDialogComponent, {data: {errorMessage: error.error ? error.error[0]['ErrorMessage'] : error.error.message}});
            return "BadResult";
        }
    }

    private handleForbidden = () => {
        this.router.navigate(["/forbidden"], { queryParams: { returnUrl: this.router.url }});
        return "Forbidden";
    }
}