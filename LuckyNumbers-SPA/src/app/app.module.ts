import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MainPageComponent } from './main-page/main-page.component';
import { LeftSideComponent } from './main-page/left-side/left-side.component';
import { CenterSideComponent } from './main-page/center-side/center-side.component';
import { RightSideComponent } from './main-page/right-side/right-side.component';

@NgModule({
   declarations: [
      AppComponent,
      MainPageComponent,
      LeftSideComponent,
      CenterSideComponent,
      RightSideComponent
   ],
   imports: [
      BrowserModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
