import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { MainPageComponent } from './main-page/main-page.component';
import { LeftSideComponent } from './main-page/left-side/left-side.component';
import { RightSideComponent } from './main-page/right-side/right-side.component';
import { LoginComponent } from './login/login.component';
import { AuthService } from './_service/auth.service';
import { RegisterComponent } from './register/register.component';
import { appRoutes } from './routes';
import { StatisticsComponent } from './statistics/statistics.component';
import { UserAccountComponent } from './user-account/user-account.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserSearchComponent } from './user-search/user-search.component';
import { UserDetailsComponent } from './user-search/user-details/user-details.component';
import { UserLoginResolver } from './_resolvers/user-login.resolver';
import { UserService } from './_service/user.service';
import { UserHistoryResolver } from './_resolvers/user-history.resolver';
import { HistoryComponent } from './user-account/history/history.component';
import { NumbersComponent } from './user-account/numbers/numbers.component';
import { UserSendedBetsResolver } from './_resolvers/user-sended-bets.resolver';

@NgModule({
   declarations: [
      AppComponent,
      MainPageComponent,
      LeftSideComponent,
      RightSideComponent,
      LoginComponent,
      RegisterComponent,
      StatisticsComponent,
      UserAccountComponent,
      UserSearchComponent,
      UserDetailsComponent,
      HistoryComponent,
      NumbersComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      UserService,
      AuthGuard,
      UserLoginResolver,
      UserHistoryResolver,
      UserSendedBetsResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
