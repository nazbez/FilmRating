import { HttpClient } from '@angular/common/http';
import { RegisterModel } from "../../register/register.model";
import { AuthenticationResultModel } from "../models/authentication-result.model";
import { Injectable } from "@angular/core";
import { LoginModel } from "../../login/login.model";
import { Subject } from "rxjs";
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    private isAdminSub = new Subject<boolean>()
    private authChangeSub = new Subject<boolean>();
    
    public authChanged = this.authChangeSub.asObservable();
    public isAdminChanged = this.isAdminSub.asObservable();
    
    constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }
    
    public register = (body: RegisterModel) => {
        return this.http.post<AuthenticationResultModel>('api/Authentication/Register', body);
    }

    public login = (body: LoginModel) => {
        return this.http.post<AuthenticationResultModel>('api/Authentication/Login', body);
    }

    public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
        this.authChangeSub.next(isAuthenticated);
        this.isAdminSub.next(this.isUserAdmin());
    }

    public logout = () => {
        localStorage.removeItem("token");
        this.sendAuthStateChangeNotification(false);
    }

    public isUserAuthenticated = (): boolean => {
        const token = localStorage.getItem("token");

        return token && !this.jwtHelper.isTokenExpired(token);
    }

    public isUserAdmin = (): boolean => {
        const token = localStorage.getItem("token");
        
        if (!token) 
            return false;
        
        const decodedToken = this.jwtHelper.decodeToken(token);
        const role = decodedToken['role'];
        return role === 'Administrator';
    }

    public isUserCritic = (): boolean => {
        const token = localStorage.getItem("token");

        if (!token)
            return false;

        const decodedToken = this.jwtHelper.decodeToken(token);
        const role = decodedToken['role'];
        return role === 'Critic';
    }
}