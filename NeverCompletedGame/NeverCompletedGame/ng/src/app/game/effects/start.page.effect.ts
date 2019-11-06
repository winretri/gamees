import { GameId } from './../../../never-ending-game/model/game.interface';
import { concatMap, catchError, map, tap } from 'rxjs/operators';
import { Action } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { RxNeverCompletedGameService } from 'src/never-ending-game/service/rx.never-completed-game.service';
import { LocalStorageService } from './../../../never-ending-game/service/local-storage-service.service';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import * as startPageAction from '../actions/start.page.action';

@Injectable()
export class StartPageEffects {

  constructor(
    private actions$: Actions,
    private gameService: RxNeverCompletedGameService,
    private localStorage: LocalStorageService
  ) {
  }

  @Effect()
  openGame$: Observable<Action> = this.actions$.pipe(
    ofType<startPageAction.OpenGame>(
      startPageAction.StartPageActionTypes.OPEN_GAME
    ),
    concatMap((action: startPageAction.OpenGame) => {
      console.log('OPEN GAME');
      return this.gameService.openGame().pipe(
        map((gameId: GameId) => {
          // this.localStorage.storeOpenGameId(gameId);
          return new startPageAction.OpenGameSuccess(gameId);
        }),
        catchError(err => {
          return of(new startPageAction.OpenGameFail(err));
        })
      );
    })
  );

  @Effect({ dispatch: false })
  openGameSuccess$: Observable<Action> = this.actions$.pipe(
    ofType<startPageAction.OpenGameSuccess>(
      startPageAction.StartPageActionTypes.OPEN_GAME_SUCCESS
    ),
    tap((action: startPageAction.OpenGameSuccess) => {
       console.log('OPEN GAME SUCCESS');
       this.localStorage.storeOpenGameId(action.payload);
    })
  );



  @Effect()
  initGame$: Observable<Action> = this.actions$.pipe(
    ofType<startPageAction.InitGame>(
      startPageAction.StartPageActionTypes.INIT_GAME
    ),
    concatMap((action: startPageAction.InitGame) => {
     console.log('INIT GAME');
     if (this.localStorage.containsOpenGameId) {
       return of(new startPageAction.OpenGameSuccess(this.localStorage.getOpenGameId()));
     }
    })
  );
}
