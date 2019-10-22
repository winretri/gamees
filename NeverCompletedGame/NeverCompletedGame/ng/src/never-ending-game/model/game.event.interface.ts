import { GameId } from './game.interface';
export interface MakeGuessEvent {
  gameId: GameId;
  solution: string;
}
