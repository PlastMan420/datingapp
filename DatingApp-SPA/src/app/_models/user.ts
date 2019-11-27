import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    knownAs: string;
    age: number;
    gender: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    city: string;
    country: string;
    // Detailed Dto:
    interests?: string; // optionals need to come after requireds. mixing will return an error
    introduction?: string;
    lookingFor?: string;
    photos?: Photo[];
}
