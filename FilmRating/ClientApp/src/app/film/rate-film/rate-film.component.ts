import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { RatingService } from "../../shared/services/rating.service";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { RatingUserRateModel } from "../../shared/models/rating-user-rate.model";
import { HttpErrorResponse } from "@angular/common/http";

@Component({
    selector: 'app-rate-film',
    templateUrl: './rate-film.component.html',
    styleUrls: ['./rate-film.component.css']
})
export class RateFilmComponent implements OnInit {
    rateFormGroup: FormGroup;
    rates: number[];
    
    constructor(
        public ratingService: RatingService,
        public dialogRef: MatDialogRef<RateFilmComponent>,
        @Inject(MAT_DIALOG_DATA) public data: RatingUserRateModel) {}

    ngOnInit(): void {
        this.ratingService.getOptions().subscribe(r => {
            this.rates = r;
        });

        this.rateFormGroup = new FormGroup({
            rate: new FormControl(this.data.hasRate ? this.data.rate : undefined, [Validators.required, Validators.min(0)]),
        });
    }

    save(value) {
        const result = {... value };
        let isUpdating = this.data.hasRate;
        if (isUpdating) {
            this.ratingService.update({filmId: this.data.filmId, rate: result.rate})
                .subscribe({
                    next: () =>
                    {
                        this.dialogRef.close(result.rate);
                    },
                    error: (err: HttpErrorResponse) => console.log(err.error.errors)
                });
        } else {
            this.ratingService.create({filmId: this.data.filmId, rate: result.rate})
                .subscribe({
                    next: () =>
                    {
                        this.dialogRef.close(result.rate);
                    },
                    error: (err: HttpErrorResponse) => console.log(err.error.errors)
                });
        }
    }

    validateControl = (controlName: string) => {
        return this.rateFormGroup.get(controlName).invalid && this.rateFormGroup.get(controlName).touched;
    }
    
    hasError = (controlName: string, errorName: string) => {
        return this.rateFormGroup.get(controlName).hasError(errorName);
    }
}