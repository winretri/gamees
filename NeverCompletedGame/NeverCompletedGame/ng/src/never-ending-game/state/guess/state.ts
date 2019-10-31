import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';
import { IGuess } from '../../model';

export interface GuessState extends EntityState<IGuess> {
  loaded: boolean;
  loading: boolean;
  error: string;
  success: string;
}


export const guessAdapter: EntityAdapter<IGuess> = createEntityAdapter<IGuess>();

export const defaultGuessState: GuessState = {
  ids: [],
  entities: {},
  loaded: false,
  loading: false,
  error: '',
  success: '',
};

export const initialGuessState = guessAdapter.getInitialState(defaultGuessState);
