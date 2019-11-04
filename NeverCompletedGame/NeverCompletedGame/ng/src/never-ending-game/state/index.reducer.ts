import { Action } from '@ngrx/store';
import { GameAction } from './game/actions';
import { GameState } from './game/state';
import { GuessAction } from './guess/actions';
import { GuessState } from './guess/state';
import { guessReducer } from './guess/reducers';
import { gameReducer } from './game/reducers';
import { signalrReducer, BaseSignalRStoreState } from 'ngrx-signalr';

export interface NgcGameState {
  game: GameState;
  guess: GuessState;
}

export type Reducer<S, A> = (s: S, a: A) => S;

export interface GameReducersType {
  game: Reducer<GameState, GameAction>;
  guess: Reducer<GuessState, GuessAction>;
  signalr: Reducer<BaseSignalRStoreState, Action>;
}


export const GameReducers: GameReducersType = {
  game: gameReducer,
  guess: guessReducer,
  signalr: signalrReducer,
};
