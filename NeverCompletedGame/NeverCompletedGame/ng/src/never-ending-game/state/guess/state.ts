
import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';
import { IGuess } from '../../model';

export interface GuessState extends EntityState<IGuess> {
  loaded: boolean;
  loading: boolean;
  error: string;
  success: string;
}


export const guessAdapter: EntityAdapter<IGuess> = createEntityAdapter<IGuess>({ sortComparer: sortByDate });

function sortByDate(ob1: IGuess, ob2: IGuess): number {
  const d1 = new Date(ob1.guessTime);
  const d2 = new Date(ob2.guessTime);

  const same = d1.getTime() === d2.getTime();
  if (same) {
    return 0;
  }

  // Check if the first is greater than second
  if (d2 > d1) {
    return 1;
  }

  // Check if the first is less than second
  if (d2 < d1) {
    return -1;
  }
}

export const defaultGuessState: GuessState = {
  ids: [],
  entities: {},
  loaded: false,
  loading: false,
  error: '',
  success: '',
};

export const initialGuessState = guessAdapter.getInitialState(defaultGuessState);
