import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { MyDutiesComponent } from './my-duties/my-duties.component';
import { MyHomeComponent } from './my-home/my-home.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'login', component: LoginComponent},
    {path: 'my-duties', component: MyDutiesComponent},
    {path: 'my-home', component: MyHomeComponent}
];
