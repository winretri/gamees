import { GameId } from './../../../never-ending-game/model/game.interface';
import { Action } from '@ngrx/store';

export enum StartPageActionTypes {
  INIT_GAME = '[STARTPAGE] Init Game',
  OPEN_GAME = '[STARTPAGE] Open Game',
  OPEN_GAME_SUCCESS = '[STARTPAGE] Open Game Success',
  OPEN_GAME_FAIL = '[STARTPAGE] Open Game Fail',
  RESET_GAME = '[STARTPAGE] Reset Game',
}
// ---- READ ----
export class InitGame implements Action {
  readonly type = StartPageActionTypes.INIT_GAME;

  constructor() {
  }
}

export class OpenGame implements Action {
  readonly type = StartPageActionTypes.OPEN_GAME;

  constructor() {
  }
}

export class OpenGameSuccess implements Action {
  readonly type = StartPageActionTypes.OPEN_GAME_SUCCESS;

  constructor(public payload: GameId) {
  }
}

export class OpenGameFail implements Action {
  readonly type = StartPageActionTypes.OPEN_GAME_FAIL;

  constructor(public payload: GameId) {
  }
}

export class ResetGame implements Action {
  readonly type = StartPageActionTypes.RESET_GAME;

  constructor(public payload: GameId) {
  }
}
