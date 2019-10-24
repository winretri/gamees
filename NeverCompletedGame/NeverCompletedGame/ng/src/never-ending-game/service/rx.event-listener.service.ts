import { EventReceived } from './../state/game/actions';
import { Store } from '@ngrx/store';
import { GameId } from './../model/game.interface';
import { Subject, Observable } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { NCG_BASE_URL } from '../util/token';
import { Url } from '../util/url';

@Injectable()
export class RxEventListenerService {

  private message$: Subject<string>;
  private connectionEstablished$: Subject<boolean>;
  private readonly url: Url;
  private hubConnection: signalR.HubConnection;

  constructor(@Inject(NCG_BASE_URL) baseUrl: string, private store: Store<any>) {
    this.url = new Url([baseUrl, 'events'], {});
    this.message$ = new Subject<string>();
    this.connectionEstablished$ = new Subject<boolean>();
   }

  public startConnection = (gameId: GameId) => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl(this.url.serialize())
                            .build();

    this.hubConnection
      .start()
      .then(() => {
        this.listenForGameEvents(gameId);
        this.addEventListener();
        this.connectionEstablished$.next(true);
      })
      .catch(err => {
        console.log('Error while starting connection: ' + err);
        this.connectionEstablished$.next(false);
      });

    return this.connectionEstablished$.asObservable();
  }

  public listenForGameEvents(gameId: GameId) {
    this.hubConnection.send('JoinGroup', gameId);
  }

  public disconnect() {
    this.hubConnection.stop();
  }

  public getMessage(): Observable<string> {
    return this.message$.asObservable();
  }

  private addEventListener = () => {
    this.hubConnection.on('ReceiveEvent', (data: any) => {
      console.log(data);
      this.store.dispatch(new EventReceived({
        type: data,
      }));
      this.message$.next(data);
    });
  }

}
