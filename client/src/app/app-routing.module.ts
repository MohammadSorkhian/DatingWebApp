import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';

const routes: Routes = [
  {path:"" , component:HomeComponent, pathMatch:"full" },
  {path:"members" , component:MemberListComponent , canActivate:[AuthGuard]},
  {path:"members/:Username" , component:MemberDetailComponent, canActivate:[AuthGuard] },
  {path:"member/edit" , component: MemberEditComponent},
  // {path:"messages" , component:MessagesComponent },
  {path:"**" , component:HomeComponent, pathMatch:"full" },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
