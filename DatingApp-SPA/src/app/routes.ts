import {Routes} from '@angular/router';

import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list-resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent}, // localhost:4200/ redirects to home
    {
        path: '', // localhost:4200/
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard], // route guard. 'canActivate' takes an array
        children: [ // protected children routes
            { path: 'members', component: MemberListComponent, resolve: {users: MemberListResolver}},
            { path: 'members/:id', component: MemberDetailComponent, resolve: {user: MemberDetailResolver}},
            { path: 'messages', component: MessagesComponent},
            { path: 'lists', component: ListsComponent},
        ] // Array of childrouts
    },
    { path: '**', redirectTo: '', pathMatch: 'full'},
    // wildcard route '**': anything that does't match goes to home. it must be at the bottom.
];
