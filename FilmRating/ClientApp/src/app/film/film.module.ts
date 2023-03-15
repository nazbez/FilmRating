import { NgModule } from "@angular/core";
import { FilmComponent } from "./film.component";
import { RouterModule } from "@angular/router";

@NgModule({
    declarations: [ FilmComponent ],
    exports: [ FilmComponent ],
    imports: [
        RouterModule.forChild([
            { path: '', component: FilmComponent },
        ]),
    ]
})
export class FilmModule {}