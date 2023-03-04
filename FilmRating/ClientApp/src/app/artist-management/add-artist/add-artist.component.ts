import { Component, OnInit } from "@angular/core";
import {ArtistModel, ArtistRoleModel} from "../../shared/models/artist.model";
import { ArtistService } from "../../shared/services/artist.service";
import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import { CreateArtistModel } from "../../shared/models/create-artist.model";
import { HttpErrorResponse } from "@angular/common/http";
import { MatDialogRef } from "@angular/material/dialog";

@Component({
    selector: 'app-add-artist',
    templateUrl: './add-artist.component.html',
    styleUrls: ['./add-artist.component.css']
})
export class AddArtistComponent implements OnInit {
    roles: ArtistRoleModel[];

    newArtistFormGroup: FormGroup;
    errorMessage: string = '';
    showError: boolean;
    
    constructor(public artistService: ArtistService, public dialogRef: MatDialogRef<AddArtistComponent>) {}

    ngOnInit(): void {
        this.artistService.getRoles()
            .subscribe((r) => {
                this.roles = r;
            });
        
        this.newArtistFormGroup = new FormGroup({
            firstName: new FormControl("", [Validators.required]),
            lastName: new FormControl("", [Validators.required]),
            roles: new FormArray([], [Validators.required])
        })
    }

    validateControl = (controlName: string) => {
        return this.newArtistFormGroup.get(controlName).invalid && this.newArtistFormGroup.get(controlName).touched
    }
    hasError = (controlName: string, errorName: string) => {
        return this.newArtistFormGroup.get(controlName).hasError(errorName)
    }
    
    onCheckChange(event) {
        const formArray: FormArray = this.newArtistFormGroup.get('roles') as FormArray;

        if(event.target.checked){
            formArray.push(new FormControl(event.target.value));
        }
        else{
            let i: number = 0;

            formArray.controls.forEach((ctrl: FormControl) => {
                if(ctrl.value == event.target.value) {
                    formArray.removeAt(i);
                    return;
                }
                
                i++;
            });
        }
    }

    save = (createArtistFormValue) => {
        this.showError = false;
        const createArtist = {... createArtistFormValue };
        const newArtist: CreateArtistModel = {
            firstName: createArtist.firstName,
            lastName: createArtist.lastName,
            roleIds: createArtist.roles,
        };
        
        this.artistService.create(newArtist)
            .subscribe({
                next: (res: ArtistModel) => this.dialogRef.close(res),
                error: (err: HttpErrorResponse) => console.log(err.error.errors)
            })
    }
}