import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from "../shared/services/authentication.service";
import { RegisterModel } from "./register.model";
import { PasswordConfirmationValidator } from "../shared/validators/password-confirmation.validator";
import { AuthenticationResultModel } from "../shared/models/authentication-result.model";
import { ActivatedRoute, Router } from "@angular/router";
import { ExternalAuthModel } from "../shared/models/external-auth.model";

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    private returnUrl: string;
    
    registerForm: FormGroup;
    errorMessage: string = '';
    showError: boolean;
    
    constructor(
        private authService: AuthenticationService,
        private passConfValidator: PasswordConfirmationValidator,
        private router: Router,
        private route: ActivatedRoute) {
        this.authService.extAuthChanged.subscribe(user => {
            const externalAuth: ExternalAuthModel = {
                provider: user.provider,
                idToken: user.idToken
            }
            this.validateExternalAuth(externalAuth);
        });
    }
    
    ngOnInit(): void {
        this.registerForm = new FormGroup({
            firstName: new FormControl(''),
            lastName: new FormControl(''),
            email: new FormControl('', [Validators.required, Validators.email]),
            password: new FormControl('', [Validators.required]),
            confirm: new FormControl('')
        });

        this.registerForm.get('confirm').setValidators([Validators.required,
            this.passConfValidator.validateConfirmPassword(this.registerForm.get('password'))]);

        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }
    
    public validateControl = (controlName: string) => {
        return this.registerForm.get(controlName).invalid && this.registerForm.get(controlName).touched
    }
    
    public hasError = (controlName: string, errorName: string) => {
        return this.registerForm.get(controlName).hasError(errorName)
    }
    
    public registerUser = (registerFormValue) => {
        this.showError = false;
        const formValues = { ...registerFormValue };
        const user: RegisterModel = {
            firstName: formValues.firstName,
            lastName: formValues.lastName,
            email: formValues.email,
            password: formValues.password,
            confirmPassword: formValues.confirm
        };
        this.authService.register(user)
            .subscribe({
                    next: (res: AuthenticationResultModel) => {
                        localStorage.setItem("token", res.token);
                        this.authService.sendAuthStateChangeNotification(res.success);
                        this.router.navigate([this.returnUrl]);
                    },
                error: (err: HttpErrorResponse) => {
                    this.errorMessage = err.message;
                    this.showError = true;
                }
            })
    }

    private validateExternalAuth(externalAuth: ExternalAuthModel) {
        this.authService.externalLogin(externalAuth)
            .subscribe({
                next: (res) => {
                    localStorage.setItem("token", res.token);
                    this.authService.sendAuthStateChangeNotification(res.success);
                    this.router.navigate([this.returnUrl]);
                },
                error: (err: HttpErrorResponse) => {
                    this.errorMessage = err.message;
                    this.showError = true;
                    this.authService.signOutExternal();
                }
            });
    }
}