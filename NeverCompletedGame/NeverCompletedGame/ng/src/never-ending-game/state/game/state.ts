import { GameEvent } from './../../model/game.event.interface';

import { GameId } from './../../model/game.interface';
import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';
import { IGame,  } from '../../model';

export interface GameState extends EntityState<IGame> {
  loaded: boolean;
  loading: boolean;
  opened: boolean;
  opening: boolean;
  gameId: GameId;
  error: string;
  lastEvent: GameEvent;
}

export const gameAdapter: EntityAdapter<IGame> = createEntityAdapter<IGame>();

export const defaultGame: GameState = {
  ids: [],
  entities: {},
  loaded: false,
  loading: false,
  opened: false,
  opening: false,
  gameId: null,
  error: '',
  lastEvent: null,
};
export const initialGameState = gameAdapter.getInitialState(defaultGame);
