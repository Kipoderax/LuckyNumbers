import { Routes } from '@angular/router';
import { MainPageComponent } from './main-page/main-page.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { StatisticsComponent } from './statistics/statistics.component';

export const appRoutes: Routes = [
    { path: 'home', component: MainPageComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'statystyki', component: StatisticsComponent },
    { path: '**', redirectTo: 'home', pathMatch: 'full' },
];
