import { GuessAction } from './guess/actions';
import { GuessState } from './guess/state';
import { GameAction, gameReducer, GameState } from './game';
import { guessReducer } from './guess/reducers';

export interface NgcGameState {
  game: GameState;
  guess: GuessState;
}

export type Reducer<S, A> = (s: S, a: A) => S;

export interface GameReducersType {
  game: Reducer<GameState, GameAction>;
  guess: Reducer<GuessState, GuessAction>;
}


export const GameReducers: GameReducersType = {
  game: gameReducer,
  guess: guessReducer,
};
