import { GameId } from './game.interface';


export interface MakeGuessEvent {
  gameId: GameId;
  solution: string;
}

export interface EventReceivedEvent {
  type: string;
}
