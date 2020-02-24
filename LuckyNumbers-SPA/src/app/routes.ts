import { Routes } from '@angular/router';
import { MainPageComponent } from './main-page/main-page.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { UserAccountComponent } from './user-account/user-account.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserSearchComponent } from './user-search/user-search.component';
import { UserDetailsComponent } from './user-search/user-details/user-details.component';
import { UserLoginResolver } from './_resolvers/user-login.resolver';
import { HistoryComponent } from './user-account/history/history.component';
import { UserHistoryResolver } from './_resolvers/user-history.resolver';

export const appRoutes: Routes = [
    { path: 'home', component: MainPageComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'statystyki', component: StatisticsComponent },
    { path: 'moje-konto', component: UserAccountComponent, canActivate: [AuthGuard], resolve: {user: UserLoginResolver} },
    { path: 'historia', component: HistoryComponent, canActivate: [AuthGuard], resolve: {history: UserHistoryResolver} },
    { path: 'wyszukiwarka', component: UserSearchComponent },
    { path: 'gracz/:username', component: UserDetailsComponent },
    { path: '**', redirectTo: 'home', pathMatch: 'full' },
];
