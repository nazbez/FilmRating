import { Component } from "@angular/core";
import { ArtistService } from "../shared/services/artist.service";
import { ArtistModel } from "../shared/models/artist.model";

@Component({
    selector: 'app-artist-management',
    templateUrl: './artist-management.component.html',
    styleUrls: ['./artist-management.component.css']
})
export class ArtistManagementComponent {
    artists: ArtistModel[];
    
    constructor(artistService: ArtistService) {
        artistService.getAll()
            .subscribe((a) => {
                this.artists = a;
            });
    }
}