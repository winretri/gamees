import { GameId, IGame } from './../model/game.interface';
import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Url } from '../util/url';
import { NCG_BASE_URL } from '../util/token';
import { Observable } from 'rxjs';
import * as uuid from 'uuid';

@Injectable()
export class RxNeverCompletedGameService {
  private readonly url: Url;
  private readonly viewUrl: Url;

  constructor(
    private http: HttpClient,
    @Inject(NCG_BASE_URL) baseUrl: string
  ) {
    this.url = new Url([baseUrl, 'api', 'command'], {});
    this.viewUrl = new Url([baseUrl, 'api', 'games'], {});
  }

  public fetchData(): Observable<string[]> {
    return this.http.get<string[]>(this.url.serialize());
  }

  public fetchGame(gameId: GameId): Observable<IGame> {
    return this.http.get<IGame>(
      this.gameUrl(gameId).serialize()
    );
  }

  private gameUrl(gameId: GameId): Url {
    return this.url.addPaths(gameId);
  }

  public openGame(): Observable<GameId> {
    const name = 'open';
    const domain = 'playing';
    const aggregateType = 'game';
    const aggregateId = uuid.v4();
    const body = { name, domain, aggregateType, aggregateId};
    return this.http.post(this.url.serialize(), body).pipe(aggregateId);
  }
}
