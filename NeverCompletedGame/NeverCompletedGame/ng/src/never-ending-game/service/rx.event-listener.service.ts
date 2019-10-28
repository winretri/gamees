import { GameEvent } from './../model/game.event.interface';
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

  private message$: Subject<GameEvent>;
  private connectionEstablished$: Subject<boolean>;
  private readonly url: Url;
  private hubConnection: signalR.HubConnection;

  constructor(@Inject(NCG_BASE_URL) baseUrl: string, private store: Store<any>) {
    this.url = new Url([baseUrl, 'events'], {});
    this.message$ = new Subject<GameEvent>();
    this.connectionEstablished$ = new Subject<boolean>();
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl(this.url.serialize())
                            .build();
   }

  public startConnection = (gameId: GameId) => {


    if (this.hubConnection.state === signalR.HubConnectionState.Disconnected) {
      this.hubConnection
      .start().then(() => {
        console.log('RECONECTED TO GAME ' + gameId);
        this.addEventListener();
        this.listenForGameEvents(gameId);
        this.connectionEstablished$.next(true);
      })
      .catch(err => {
        console.log('Error while starting connection: ' + err);
        this.connectionEstablished$.next(false);
      });
    } else {
      console.log('LISTEN TO GAME ' + gameId);
      this.listenForGameEvents(gameId);
      this.connectionEstablished$.next(true);
    }

    return this.connectionEstablished$.asObservable();
  }

  public listenForGameEvents(gameId: GameId) {
    this.hubConnection.send('JoinGroup', gameId);
  }

  public stopListeningForGameEvents(gameId: GameId) {
    this.hubConnection.send('LeaveGroup', gameId);
  }

  public disconnect() {
    this.hubConnection.stop();
  }

  public getMessage(): Observable<GameEvent> {
    return this.message$.asObservable();
  }

  private addEventListener = () => {
    this.hubConnection.on('ReceiveEvent', (data: any) => {
      data = JSON.parse(data);
      console.log(data);
      this.store.dispatch(new EventReceived(data));
      this.message$.next(data);
    });
  }

}
