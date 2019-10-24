import { createFeatureSelector, createSelector } from '@ngrx/store';
import { GameState } from './state';

const getGameFeatureState = createFeatureSelector<GameState>('game');

const getGameLoaded = createSelector(
  getGameFeatureState,
  (state) => state.loaded
);

const getGameId = createSelector(
  getGameFeatureState,
  (state) => state.gameId
);

const getLastGameEvent = createSelector(
  getGameFeatureState,
  (state) => state.lastEvent
);

const getGame = createSelector(
  getGameFeatureState,
  getGameId,
  (state) => state.entities[state.gameId]
);



export const gameSelectors = {
  getGameId,
  getGame,
  getGameLoaded,
  getLastGameEvent
};

