import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
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

@NgModule({
   declarations: [
      AppComponent,
      MainPageComponent,
      LeftSideComponent,
      RightSideComponent,
      LoginComponent,
      RegisterComponent,
      StatisticsComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
