import * as gameAction from './actions';
import { gameAdapter, GameState, initialGameState } from './state';

function initGameReducer(state: GameState, action: gameAction.GameAction): GameState {
  switch (action.type) {
    case gameAction.GameActionTypes.INIT_GAME:
        return {
          ...state,
          loaded: false,
          loading: false,
          opened: false,
          opening: false,
          gameId: null,
          error: ''
        };
    case gameAction.GameActionTypes.OPEN_GAME:
        return {
          ...state,
          loaded: false,
          loading: false,
          opened: false,
          opening: true,
          gameId: null,
          error: ''
        };
    case gameAction.GameActionTypes.OPEN_GAME_SUCCESS:
        return {
          ...state,
          loaded: false,
          loading: false,
          opened: false,
          opening: true,
          gameId: action.payload,
          error: ''
        };
    default:
      return state;
  }
}

export const  reducers = [
  initGameReducer,
];

export function flowReducer(
  state: GameState = initialGameState,
  action: gameAction.GameAction): GameState {
  return reducers.reduce((currentState, reducerFn) => reducerFn(currentState, action), state);
}
