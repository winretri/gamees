import { MakeGuessEvent, EventReceivedEvent, GameEvent } from './../../model/game.event.interface';
import { Action } from '@ngrx/store';
import { GameId, IGame } from '../../model';
import { HttpErrorResponse } from '@angular/common/http';

export enum GameActionTypes {
  INIT_GAME = '[GAME] Init Game',
  OPEN_GAME = '[GAME] Open Game',
  OPEN_GAME_SUCCESS = '[GAME] Open Game Success',
  OPEN_GAME_FAIL = '[GAME] Open Game Fail',
  LOAD_GAME = '[GAME] Load Game',
  LOAD_GAME_SUCCESS = '[GAME] Load Game Success',
  LOAD_GAME_FAIL = '[GAME] Load Game Fail',
  MAKE_GUESS = '[GAME] Make Guess',
  MAKE_GUESS_SUCCESS = '[GAME] Make Guess Success',
  MAKE_GUESS_FAIL = '[GAME] Make Guess Fail',
  RESET_GAME = '[GAME] Reset Game',
  EVENT_RECEIVED = '[GAME] Event Received',
}

// ---- READ ----
export class InitGame implements Action {
  readonly type = GameActionTypes.INIT_GAME;

  constructor() {
  }
}

export class OpenGame implements Action {
  readonly type = GameActionTypes.OPEN_GAME;

  constructor() {
  }
}

export class OpenGameSuccess implements Action {
  readonly type = GameActionTypes.OPEN_GAME_SUCCESS;

  constructor(public payload: GameId) {
  }
}

export class OpenGameFail implements Action {
  readonly type = GameActionTypes.OPEN_GAME_FAIL;

  constructor(public payload: GameId) {
  }
}

export class LoadGame implements Action {
  readonly type = GameActionTypes.LOAD_GAME;

  constructor(public payload: GameId) {
  }
}

export class LoadGameSuccess implements Action {
  readonly type = GameActionTypes.LOAD_GAME_SUCCESS;

  constructor(public payload: IGame) {
  }
}

export class LoadGameFail implements Action {
  readonly type = GameActionTypes.LOAD_GAME_FAIL;

  constructor(public payload: HttpErrorResponse) {
  }
}

export class MakeGuess implements Action {
  readonly type = GameActionTypes.MAKE_GUESS;

  constructor(public payload: MakeGuessEvent) {
  }
}

export class MakeGuessSuccess implements Action {
  readonly type = GameActionTypes.MAKE_GUESS_SUCCESS;

  constructor(public payload: GameId) {
  }
}

export class MakeGuessFail implements Action {
  readonly type = GameActionTypes.MAKE_GUESS_FAIL;

  constructor(public payload: HttpErrorResponse) {
  }
}

export class EventReceived implements Action {
  readonly type = GameActionTypes.EVENT_RECEIVED;

  constructor(public payload: GameEvent) {
  }
}

export class ResetGame implements Action {
  readonly type = GameActionTypes.RESET_GAME;

  constructor() {
  }
}

export type GameLoadAction = InitGame | OpenGame | OpenGameSuccess | LoadGame | LoadGameSuccess | LoadGameFail;
export type GamePlayAction = ResetGame | MakeGuess | EventReceived;
export type GameAction = GameLoadAction | GamePlayAction;
