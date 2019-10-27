import { GameId } from './game.interface';


export interface GameEvent {
     AggregateId: string;
     DomainEventName: string;
     Id: string;
}

export interface MakeGuessEvent {
  gameId: GameId;
  solution: string;
}

export interface EventReceivedEvent {
  type: string;
}
