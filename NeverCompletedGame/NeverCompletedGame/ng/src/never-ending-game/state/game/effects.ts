import { GameId } from './../../model/game.interface';
import { LocalStorageService } from './../../service/local-storage-service.service';
import { Injectable } from '@angular/core';

import { Actions, Effect, ofType } from '@ngrx/effects';
import { Action } from '@ngrx/store';

import { Observable, of } from 'rxjs';
import { catchError, concatMap, map } from 'rxjs/operators';

import * as gameAction from './actions';

import { RxNeverCompletedGameService } from '../../service';
import { IGame } from '../../model';

@Injectable()
export class GameEffects {

  constructor(
    private actions$: Actions,
    private gameService: RxNeverCompletedGameService,
    private localStorage: LocalStorageService
  ) {
  }

  @Effect()
  openGame$: Observable<Action> = this.actions$.pipe(
    ofType<gameAction.OpenGame>(
      gameAction.GameActionTypes.OPEN_GAME
    ),
    concatMap((action: gameAction.OpenGame) =>
      this.gameService.openGame().pipe(
        map((gameId: GameId) => new gameAction.OpenGameSuccess(gameId)),
        catchError(err => {
          return of(new gameAction.OpenGameFail(err));
        })
      )
    )
  );

  @Effect()
  initGame$: Observable<Action> = this.actions$.pipe(
    ofType<gameAction.InitGame>(
      gameAction.GameActionTypes.INIT_GAME
    ),
    concatMap((action: gameAction.InitGame) => {
     if (this.localStorage.containsOpenGameId) {
       return of(new gameAction.LoadGame(this.localStorage.getOpenGameId()));
     } else {
       return of(new gameAction.OpenGame());
     }
    })
  );

}
