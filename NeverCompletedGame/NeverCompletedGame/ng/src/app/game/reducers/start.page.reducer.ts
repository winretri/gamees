import { GameId } from './../../../never-ending-game/model/game.interface';
import { Action } from '@ngrx/store';
import * as StartPageAction from '../actions/start.page.action';

export interface State {
  openGameId: GameId;
  opening: boolean;
  opened: boolean;
}

export const initialState: State = {
  openGameId: null,
  opening: false,
  opened: false,
};


export function reducer(state = initialState, action: Action): State {
  switch (action.type) {
    case StartPageAction.StartPageActionTypes.INIT_GAME:
        return {
          ...state,
          opened: false,
          opening: false,
          openGameId: null,
        };
    case StartPageAction.StartPageActionTypes.OPEN_GAME:
        return {
          ...state,
          opened: false,
          opening: true,
          openGameId: null,
        };
    case StartPageAction.StartPageActionTypes.OPEN_GAME_SUCCESS:
        return {
          ...state,
          opened: true,
          opening: false,
          openGameId: (action as StartPageAction.OpenGameSuccess).payload,
        };
    default: {
      return {
        ...state,
        opened: false,
        opening: false,
        openGameId: null,
      };
    }
  }
}

export const getOpenGameId = (state: State) => state.openGameId;
export const getGameOpened = (state: State) => state.openGameId !== null;
