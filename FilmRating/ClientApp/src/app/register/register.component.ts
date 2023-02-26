import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from "../shared/services/authentication.service";
import { RegisterModel } from "./register.model";
@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    registerForm: FormGroup;
    constructor(private authService: AuthenticationService) { }
    ngOnInit(): void {
        this.registerForm = new FormGroup({
            firstName: new FormControl(''),
            lastName: new FormControl(''),
            email: new FormControl('', [Validators.required, Validators.email]),
            password: new FormControl('', [Validators.required]),
            confirm: new FormControl('')
        });
    }
    public validateControl = (controlName: string) => {
        return this.registerForm.get(controlName).invalid && this.registerForm.get(controlName).touched
    }
    public hasError = (controlName: string, errorName: string) => {
        return this.registerForm.get(controlName).hasError(errorName)
    }
    public registerUser = (registerFormValue) => {
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
                next: (_) => console.log("Successful registration"),
                error: (err: HttpErrorResponse) => console.log(err.error.errors)
            })
    }
}