import { Observable } from 'rxjs';
import { IGuess, GameId } from '../model/game.interface';
import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Url } from '../util/url';
import { NCG_BASE_URL } from '../util/token';

@Injectable()
export class RxGuessService {
  private readonly viewUrl: Url;

  private readonly url: Url;

  constructor(
    private http: HttpClient,
    @Inject(NCG_BASE_URL) baseUrl: string
  ) {
    this.viewUrl = new Url([baseUrl, 'api', 'view', 'games'], {});
  }

  public fetchGuesses(flowId: string, level: number): Observable<IGuess[]> {
    console.log(this.guessUrl(flowId, level));
    return this.http.get<IGuess[]>(this.guessUrl(flowId, level).serialize());
  }

  private gameUrl(gameId: GameId): Url {
    return this.viewUrl.addPaths(gameId);
  }

  private guessUrl(gameId: GameId, level: number) {
    return this.gameUrl(gameId).addPaths('guesses', level);
  }

}
