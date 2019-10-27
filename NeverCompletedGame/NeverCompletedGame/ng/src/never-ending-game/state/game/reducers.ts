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
          error: ''
        };
    case gameAction.GameActionTypes.LOAD_GAME:
      return {
        ...state,
        loading: true,
      };
    case gameAction.GameActionTypes.LOAD_GAME_SUCCESS:
      return gameAdapter.upsertOne(action.payload, {
        ...state,
        gameId: action.payload.id,
        loaded: true,
        loading: false,
      });
    case gameAction.GameActionTypes.LOAD_GAME_FAIL:
      return {
        ...state,
        loaded: false,
        loading: false,
      };
    case gameAction.GameActionTypes.MAKE_GUESS:
      return {
        ...state,
        lastEvent: null,
      };
    case gameAction.GameActionTypes.EVENT_RECEIVED:
      return {
        ...state,
        lastEvent: action.payload,
      };
    case gameAction.GameActionTypes.RESET_GAME:
      return initialGameState;
    default:
      return state;
  }
}

export const  reducers = [
  initGameReducer,
];

export function gameReducer(
  state: GameState = initialGameState,
  action: gameAction.GameAction): GameState {
  return reducers.reduce((currentState, reducerFn) => reducerFn(currentState, action), state);
}
