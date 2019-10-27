import { environment } from './../environments/environment';
import { GameReducers } from './../never-ending-game/state/index.reducer';
import { AppRoutingModule } from './app-routing.module';
import { NeverEndingGameModule } from './../never-ending-game/never-ending-game.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NCG_BASE_URL } from 'src/never-ending-game/util/token';
import { RxNeverCompletedGameService } from 'src/never-ending-game/service/rx.never-completed-game.service';
import { GamePageModule } from './game/pages/game-page/game.page.module';
import { StoreModule } from '@ngrx/store';
import { GameEffects } from 'src/never-ending-game/state';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

const effects = [
  GameEffects,
];

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NeverEndingGameModule,
    GamePageModule,
    AppRoutingModule,
    EffectsModule.forRoot(effects),
    StoreModule.forRoot(GameReducers),
    StoreDevtoolsModule.instrument({
      maxAge: 50,
      logOnly: environment.production
    }),
  ],
  providers: [
    RxNeverCompletedGameService,
    {provide: NCG_BASE_URL, useValue: ''}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
