export * from '../../never-ending-game/state/index.reducer';

import { Action, createSelector } from '@ngrx/store';
import * as StartPageReducer from '../game/reducers/start.page.reducer';


import { gameReducer } from 'src/never-ending-game/state/game/reducers';
import { guessReducer } from 'src/never-ending-game/state/guess/reducers';
import { signalrReducer } from 'ngrx-signalr';


import { Reducer, GameReducersType, NgcGameState } from 'src/never-ending-game/state/index.reducer';

export interface State extends NgcGameState {
  startPage: StartPageReducer.State;
}

export interface ReducersType extends GameReducersType {
  startPage: Reducer<StartPageReducer.State, Action>;
}

export const reducers: ReducersType = {
  startPage: StartPageReducer.reducer,
  game: gameReducer,
  guess: guessReducer,
  signalr: signalrReducer,
};

export const getStartPageState = (state: State) => state.startPage;
export const getOpenGameId = createSelector(getStartPageState, StartPageReducer.getOpenGameId);

