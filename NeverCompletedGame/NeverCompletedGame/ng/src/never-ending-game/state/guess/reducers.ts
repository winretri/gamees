import { GuessState, defaultGuessState, guessAdapter, initialGuessState } from './state';
import * as guessAction from './actions';


function loadGuess(state: GuessState, action: guessAction.GuessAction): GuessState {
  switch (action.type) {
    case guessAction.GuessesActionTypes.RESET_GUESSES:
      return {
        ...defaultGuessState,
      };
    case guessAction.GuessesActionTypes.LOAD_GUESSES:
      return {
        ...state,
        loading: true,
      };
    case guessAction.GuessesActionTypes.LOAD_GUESSES_SUCCESS:
      return guessAdapter.upsertMany(action.payload, guessAdapter.removeAll({
        ...state,
        loaded: true,
        loading: false,
      }));
    case guessAction.GuessesActionTypes.LOAD_GUESSES_FAIL:
      return {
        ...state,
        loaded: false,
        loading: false,
      };
    default:
      return state;
  }
}

const reducers = [
  loadGuess,
];


export function guessReducer(
  state: GuessState = initialGuessState,
  action: guessAction.GuessAction): GuessState {
  return reducers.reduce((currentState, reducerFn) => reducerFn(currentState, action), state);
}
