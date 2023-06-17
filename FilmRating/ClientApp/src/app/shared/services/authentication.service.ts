import { HttpClient } from '@angular/common/http';
import { RegisterModel } from "../../register/register.model";
import { AuthenticationResultModel } from "../models/authentication-result.model";
import { Injectable } from "@angular/core";
import { LoginModel } from "../../login/login.model";
import { Subject } from "rxjs";
import { JwtHelperService } from "@auth0/angular-jwt";
import { SocialAuthService, SocialUser } from "@abacritt/angularx-social-login";
import { ExternalAuthModel } from "../models/external-auth.model";

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    private isAdminSub = new Subject<boolean>();
    private isCriticSub = new Subject<boolean>();
    private authChangeSub = new Subject<boolean>();
    private extAuthChangeSub = new Subject<SocialUser>();
    
    public authChanged = this.authChangeSub.asObservable();
    public isAdminChanged = this.isAdminSub.asObservable();
    public isCriticChanged = this.isCriticSub.asObservable();
    public extAuthChanged = this.extAuthChangeSub.asObservable();
    
    constructor(
        private http: HttpClient,
        private jwtHelper: JwtHelperService,
        private externalAuthService: SocialAuthService) {
        this.externalAuthService.authState.subscribe((user) => {
            this.extAuthChangeSub.next(user);
        });
    }
    
    public register = (body: RegisterModel) => {
        return this.http.post<AuthenticationResultModel>('api/Authentication/Register', body);
    }

    public login = (body: LoginModel) => {
        return this.http.post<AuthenticationResultModel>('api/Authentication/Login', body);
    }

    public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
        this.authChangeSub.next(isAuthenticated);
        this.isAdminSub.next(this.isUserAdmin());
        this.isCriticSub.next(this.isUserCritic());
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
    
    public signOutExternal = () => {
        this.externalAuthService.signOut();
    }
    
    public externalLogin = (model: ExternalAuthModel) => {
        return this.http.post<AuthenticationResultModel>('api/Authentication/ExternalLogin', model);
    }
}