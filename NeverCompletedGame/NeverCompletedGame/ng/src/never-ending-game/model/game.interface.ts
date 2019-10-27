
export type GameId = string;

export interface IGame {
  readonly id: GameId;

  readonly level: number;

  readonly question: string;

  readonly score: number;

  readonly completed: boolean;
}
