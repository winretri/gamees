import { GameEvent } from './../../model/game.event.interface';
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

const getLastGuessSuccessful = createSelector(
  getLastGameEvent,
  (state: GameEvent) => state != null && state.DomainEventName === 'LevelSucceeded'
);

const getLastGuessWrong = createSelector(
  getLastGameEvent,
  (state: GameEvent) => state != null && state.DomainEventName === 'LevelFailed'
);


const getGame = createSelector(
  getGameFeatureState,
  getGameId,
  (state) => state.entities[state.gameId]
);

const getGameCompleted = createSelector(
  getLastGameEvent,
  getGame,
  (gameEvent, game) => gameEvent != null  && gameEvent.DomainEventName === 'GameCompleted' || game != null && game.completed
);

export const gameSelectors = {
  getGameId,
  getGame,
  getGameLoaded,
  getLastGameEvent,
  getGameCompleted,
  getLastGuessWrong,
  getLastGuessSuccessful
};

