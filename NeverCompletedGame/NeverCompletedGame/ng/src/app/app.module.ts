import { GameReducers } from './../never-ending-game/state/index.reducer';
import { AppRoutingModule } from './app-routing.module';
import { NeverEndingGameModule } from './../never-ending-game/never-ending-game.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { NCG_BASE_URL } from 'src/never-ending-game/util/token';
import { RxNeverCompletedGameService } from 'src/never-ending-game/service/rx.never-completed-game.service';
import { GamePageModule } from './game/pages/game-page/game.page.module';
import { StoreModule } from '@ngrx/store';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NeverEndingGameModule,
    GamePageModule,
    AppRoutingModule,
    StoreModule.forRoot(GameReducers),
  ],
  providers: [
    RxNeverCompletedGameService,
    {provide: NCG_BASE_URL, useValue: ''}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
