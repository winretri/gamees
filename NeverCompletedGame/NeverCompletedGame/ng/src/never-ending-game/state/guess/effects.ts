import { RxGuessService } from '../../service/rx.guess.service';
import { Action } from '@ngrx/store';
import { Injectable } from '@angular/core';
import { concatMap, map, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { ofType, Effect, Actions } from '@ngrx/effects';

import * as guessAction from './actions';
import { IGuess } from './../../model/game.interface';



@Injectable()
export class GuessEffects {

  constructor(
    private actions$: Actions,
    private guessService: RxGuessService
  ) {
  }


@Effect()
loadGuesses$: Observable<Action> = this.actions$.pipe(
  ofType<guessAction.LoadGuesses>(
    guessAction.GuessesActionTypes.LOAD_GUESSES
  ),
  concatMap((action: guessAction.LoadGuesses) => {
    console.log('LOAD EFFING GUESSES');
    return this.guessService.fetchGuesses(action.payload.gameId, action.payload.level).pipe(
      map((guesses: IGuess[]) => new guessAction.LoadPathsSuccess(guesses)),
      catchError(err => {
        return of(new guessAction.LoadPathsFail(err));
      })
    );
  }
  )
);
    }
