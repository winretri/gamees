import { RxEventListenerService } from './../../service/rx.event-listener.service';
import { GameId } from './../../model/game.interface';
import { LocalStorageService } from './../../service/local-storage-service.service';
import { Injectable } from '@angular/core';

import { Actions, Effect, ofType } from '@ngrx/effects';
import { Action } from '@ngrx/store';

import { Observable, of, merge } from 'rxjs';
import { catchError, concatMap, map, tap } from 'rxjs/operators';

import * as gameAction from './actions';

import { RxNeverCompletedGameService } from '../../service';
import { IGame } from '../../model';


@Injectable()
export class GameEffects {

  constructor(
    private actions$: Actions,
    private gameService: RxNeverCompletedGameService,
    private localStorage: LocalStorageService,
    private eventListener: RxEventListenerService
  ) {
  }

  @Effect()
  openGame$: Observable<Action> = this.actions$.pipe(
    ofType<gameAction.OpenGame>(
      gameAction.GameActionTypes.OPEN_GAME
    ),
    concatMap((action: gameAction.OpenGame) => {
      console.log('OPEN GAME');
      return this.gameService.openGame().pipe(
        map((gameId: GameId) => new gameAction.OpenGameSuccess(gameId)),
        catchError(err => {
          return of(new gameAction.OpenGameFail(err));
        })
      );
    })
  );

  @Effect()
  openGameSuccess$: Observable<Action> = this.actions$.pipe(
    ofType<gameAction.OpenGameSuccess>(
      gameAction.GameActionTypes.OPEN_GAME_SUCCESS
    ),
    concatMap((action: gameAction.OpenGameSuccess) => {
      console.log('OPEN GAME SUCCESS');
       this.localStorage.storeOpenGameId(action.payload);
       return of(new gameAction.LoadGame(action.payload));
    })
  );

  @Effect()
  initGame$: Observable<Action> = this.actions$.pipe(
    ofType<gameAction.InitGame>(
      gameAction.GameActionTypes.INIT_GAME
    ),
    concatMap((action: gameAction.InitGame) => {
     console.log('INIT GAME');
     if (this.localStorage.containsOpenGameId) {
       return of(new gameAction.LoadGame(this.localStorage.getOpenGameId()));
     } else {
       return of(new gameAction.OpenGame());
     }
    })
  );

  @Effect()
  resetGame$: Observable<Action> = this.actions$.pipe(
    ofType<gameAction.ResetGame>(
      gameAction.GameActionTypes.RESET_GAME
    ),
    concatMap((action: gameAction.ResetGame) => {
      console.log('RESET GAME');
      this.localStorage.resetOpenGame();
      if (action.payload) {
         this.eventListener.stopListeningForGameEvents(action.payload);
      }
      return of(new gameAction.InitGame());
    })
  );

  @Effect()
  loadGame$: Observable<Action> = this.actions$.pipe(
    ofType<gameAction.LoadGame>(
      gameAction.GameActionTypes.LOAD_GAME
    ),
    concatMap((action: gameAction.LoadGame) => {
      console.log('LOAD GAME');
      return this.gameService.fetchGame(action.payload).pipe(
        map((game: IGame) => {
          console.log('RETURN LOAD GAME SUCCESS');
          return new gameAction.LoadGameSuccess(game);
        }
          ),
        catchError(err => {
          console.log('LOAD GAME FAIL');
          return of(new gameAction.LoadGameFail(err));
        })
      );
      }
    )
  );

  @Effect()
  reloadGame$: Observable<Action> = this.actions$.pipe(
    ofType<gameAction.ReloadGame>(
      gameAction.GameActionTypes.RELOAD_GAME
    ),
    concatMap((action: gameAction.ReloadGame) => {
      console.log('RELOAD GAME');
      return this.gameService.fetchGame(action.payload).pipe(
        map((game: IGame) => {
          console.log('RETURN RELOAD GAME SUCCESS');
          return new gameAction.ReloadGameSuccess(game);
        }
          ),
        catchError(err => {
          console.log('LOAD REGAME FAIL');
          return of(new gameAction.ReloadGameFail(err));
        })
      );
      }
    )
  );

  @Effect()
  makeGuess$: Observable<Action> = this.actions$.pipe(
    ofType<gameAction.MakeGuess>(
      gameAction.GameActionTypes.MAKE_GUESS
    ),
    concatMap((action: gameAction.MakeGuess) =>
      this.gameService.makeGuess(action.payload.gameId, action.payload.solution).pipe(
        map((gameId: GameId) => new gameAction.MakeGuessSuccess(gameId)),
        catchError(err => {
          return of(new gameAction.MakeGuessFail(err));
        })
      )
    )
  );


  @Effect()
  loadGameSuccess$: Observable<Action> = this.actions$.pipe(
    ofType<gameAction.LoadGameSuccess>(
      gameAction.GameActionTypes.LOAD_GAME_SUCCESS
    ),
    tap(_ => console.log('aaaa')),
    concatMap((action: gameAction.LoadGameSuccess) => {
      console.log('LOAD GAME SUCCESS');
      return this.eventListener.startConnection(action.payload.id).pipe(
        map((b) => {
          return ({type: 'success ' + action.payload.id});
        }),
        catchError(err => {
          console.log('ERROR' + err);
          return of({type: 'ecx'});
      })
      );
    }
    )
  );





}
