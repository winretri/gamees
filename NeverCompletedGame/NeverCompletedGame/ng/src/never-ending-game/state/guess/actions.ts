import { HttpErrorResponse } from '@angular/common/http';
import { IGuess, GameId } from './../../model/game.interface';
import { Action } from '@ngrx/store';


export enum GuessesActionTypes {
  RESET_GUESSES = '[GAME/GUESSES] Reset Guesses',
  LOAD_GUESSES = '[GAME/GUESSES] Load Guesses',
  LOAD_GUESSES_SUCCESS = '[GAME/GUESSES] Load Guesses Success',
  LOAD_GUESSES_FAIL = '[GAME/GUESSES] Load Guesses Fail',
}

export class ResetGuesses implements Action {
  readonly type = GuessesActionTypes.RESET_GUESSES;

  constructor() {
  }
}

export class LoadGuesses implements Action {
  readonly type = GuessesActionTypes.LOAD_GUESSES;

  constructor(public payload: {gameId: GameId, level: number}) {
  }
}

export class LoadPathsSuccess implements Action {
  readonly type = GuessesActionTypes.LOAD_GUESSES_SUCCESS;

  constructor(public payload: IGuess[]) {
  }
}

export class LoadPathsFail implements Action {
  readonly type = GuessesActionTypes.LOAD_GUESSES_FAIL;

  constructor(public payload: HttpErrorResponse) {
  }
}

export type LoadAction = ResetGuesses | LoadGuesses | LoadPathsSuccess | LoadPathsFail;

export type GuessAction = LoadAction;

