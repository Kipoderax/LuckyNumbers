import { Routes } from '@angular/router';
import { MainPageComponent } from './main-page/main-page.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { UserAccountComponent } from './user-account/user-account.component';

export const appRoutes: Routes = [
    { path: 'home', component: MainPageComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'statystyki', component: StatisticsComponent },
    { path: 'moje-konto', component: UserAccountComponent },
    { path: '**', redirectTo: 'home', pathMatch: 'full' },
];
