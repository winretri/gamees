import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Url } from '../util/url';
import { NCG_BASE_URL } from '../util/token';
import { Observable } from 'rxjs';

@Injectable()
export class RxNeverCompletedGameService {
  private readonly url: Url;

  constructor(
    private http: HttpClient,
    @Inject(NCG_BASE_URL) baseUrl: string
  ) {
    this.url = new Url([baseUrl, 'api', 'command'], {});
  }

  public fetchData(): Observable<string[]> {
    return this.http.get<string[]>(this.url.serialize());
  }

  public openGame(): Observable<string[]> {
    return this.http.get<string[]>(this.url.serialize());
  }
}
