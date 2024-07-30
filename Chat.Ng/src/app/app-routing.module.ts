import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TestComponentComponent } from './chat/chat.component';
import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { authGuard } from '../guards/auth.guard';

const routes: Routes = [
  { path: "registration", component: RegistrationComponent},
  { path: "login", component: LoginComponent },
  { path: "chat", component: TestComponentComponent,canActivate: [authGuard] },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
