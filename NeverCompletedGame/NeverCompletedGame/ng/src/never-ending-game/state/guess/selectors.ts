import { createSelector, createFeatureSelector } from '@ngrx/store';
import { GuessState } from './state';

const getGuessFeatureState = createFeatureSelector<GuessState>('guess');

const getGuessEntities = createSelector(
  getGuessFeatureState,
  (state: GuessState, ) => Object.values(state.entities)
);

export const guessSelectors = {
  getGuessEntities
};

