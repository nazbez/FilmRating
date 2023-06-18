export interface ArtistModel {
    id: string;
    firstName: string;
    lastName: string;
    roles: ArtistRoleModel[];
}

export interface ArtistRoleModel {
    id: number;
    name: string;
}

export interface ArtisteTableItemModel {
    id: string;
    fullName: string;
    roles: ArtistRoleModel[];
}