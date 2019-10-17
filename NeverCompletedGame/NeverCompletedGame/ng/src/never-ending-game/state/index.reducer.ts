import { GameAction, gameReducer, GameState } from './game';

export interface NgcGameState {
  game: GameState;
}

export type Reducer<S, A> = (s: S, a: A) => S;

export interface GameReducersType {
  game: Reducer<GameState, GameAction>;
}


export const GameReducers: GameReducersType = {
  game: gameReducer,
};
