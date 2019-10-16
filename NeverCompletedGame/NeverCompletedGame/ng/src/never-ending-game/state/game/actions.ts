import { Action } from '@ngrx/store';
import { GameId, IGame } from '../../model';
import { HttpErrorResponse } from '@angular/common/http';

export enum GameActionTypes {
  INIT_GAME = '[GAME] Init Game',
  OPEN_GAME = '[GAME] Open Game',
  OPEN_GAME_SUCCESS = '[GAME] Open Game Success',
  LOAD_GAME = '[GAME] Load Game',
  LOAD_GAME_SUCCESS = '[GAME] Load Game Success',
  LOAD_GAME_FAIL = '[GAME] Load Game Fail',
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

export type GameLoadAction = InitGame | OpenGame | OpenGameSuccess | LoadGame | LoadGameSuccess | LoadGameFail;
export type GameAction = GameLoadAction;
