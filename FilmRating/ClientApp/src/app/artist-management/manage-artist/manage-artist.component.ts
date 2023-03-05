import { Component, Inject, OnInit } from "@angular/core";
import { ArtistModel, ArtistRoleModel } from "../../shared/models/artist.model";
import { ArtistService } from "../../shared/services/artist.service";
import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import { CreateArtistModel } from "../../shared/models/create-artist.model";
import { HttpErrorResponse } from "@angular/common/http";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { UpdateArtistModel } from "../../shared/models/update-artist.model";

@Component({
    selector: 'app-manage-artist',
    templateUrl: './manage-artist.component.html',
    styleUrls: ['./manage-artist.component.css']
})
export class ManageArtistComponent implements OnInit {
    roles: ArtistRoleModel[];
    newArtistFormGroup: FormGroup;
    errorMessage: string = '';
    showError: boolean;
    isUpdating: boolean = false;
    
    constructor(
        public artistService: ArtistService,
        public dialogRef: MatDialogRef<ManageArtistComponent>,
        @Inject(MAT_DIALOG_DATA) public data: ArtistModel) {}

    ngOnInit(): void {
        this.artistService.getRoles()
            .subscribe((r) => {
                this.roles = r;
            });
        
        if (this.data !== undefined) {
            this.isUpdating = true;
        }
        
        this.newArtistFormGroup = new FormGroup({
            firstName: new FormControl(this.isUpdating ? this.data.firstName : '', [Validators.required]),
            lastName: new FormControl(this.isUpdating ? this.data.lastName : '', [Validators.required]),
            roles: new FormArray(this.isUpdating
                ? this.data.roles.map(x => new FormControl(x.id))
                : [], 
                [Validators.required])
        });
    }

    validateControl = (controlName: string) => {
        return this.newArtistFormGroup.get(controlName).invalid && this.newArtistFormGroup.get(controlName).touched
    }
    hasError = (controlName: string, errorName: string) => {
        return this.newArtistFormGroup.get(controlName).hasError(errorName)
    }
    
    onCheckChange(event) {
        const formArray: FormArray = this.newArtistFormGroup.get('roles') as FormArray;

        if (event.target.checked) {
            formArray.push(new FormControl(event.target.value));
        } else {
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

    save = (manageArtistFormValue) => {
        this.showError = false;
        
        if (this.isUpdating) {
            this.updateUser(manageArtistFormValue)
        } else {
            this.createUser(manageArtistFormValue);
        }
    }
    
    private createUser = (manageArtistFormValue) => {
        const createArtist = {... manageArtistFormValue };
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
    
    private updateUser = (manageArtistFormValue) => {
        const updatedArtist = {... manageArtistFormValue };
        const artist: UpdateArtistModel = {
            firstName: updatedArtist.firstName,
            lastName: updatedArtist.lastName,
            roleIds: updatedArtist.roles,
        };

        this.artistService.update(this.data.id, artist)
            .subscribe({
                next: (res: ArtistModel) => this.dialogRef.close(res),
                error: (err: HttpErrorResponse) => console.log(err.error.errors)
            })
    }

    isChecked(id: number) {
        return this.isUpdating && this.data.roles.some(x => x.id == id);
    }
}