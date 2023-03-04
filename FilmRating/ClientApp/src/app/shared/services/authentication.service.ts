import { HttpClient } from '@angular/common/http';
import { RegisterModel } from "../../register/register.model";
import { AuthenticationResultModel } from "../models/authentication-result.model";
import { Injectable } from "@angular/core";
import { LoginModel } from "../../login/login.model";

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    constructor(private http: HttpClient) { }
    
    public register = (body: RegisterModel) => {
        return this.http.post<AuthenticationResultModel>('api/Authentication/Register', body);
    }

    public login = (body: LoginModel) => {
        return this.http.post<AuthenticationResultModel>('api/Authentication/Login', body);
    }
}