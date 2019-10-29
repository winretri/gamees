import { map, catchError, filter, concatMap, mergeMap, switchMap } from 'rxjs/operators';
import { GameEvent } from './../model/game.event.interface';
import { EventReceived } from './../state/game/actions';
import { Store } from '@ngrx/store';
import { GameId } from './../model/game.interface';
import { Subject, Observable, of, from, merge, iif } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { NCG_BASE_URL } from '../util/token';
import { Url } from '../util/url';

@Injectable()
export class RxEventListenerService {

  private message$: Subject<GameEvent>;
  private readonly url: Url;
  private hubConnection: signalR.HubConnection;

  constructor(@Inject(NCG_BASE_URL) baseUrl: string, private store: Store<any>) {
    this.url = new Url([baseUrl, 'events'], {});
    this.message$ = new Subject<GameEvent>();
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl(this.url.serialize())
                            .build();
   }

  public startConnection(gameId: GameId): Observable<boolean> {
    const gameIdObs = of (gameId);

    const connectionStart = () => {
      console.log('CONNECTION START');
      return from(this.hubConnection.start()).pipe(
      map(() => {
        this.addEventListener();
        return true;
      }),
      catchError(err => {
        console.log(err);
        return of(false);
      })
      );
    };
    console.log('CONSTATE:' + (this.hubConnection.state === signalR.HubConnectionState.Disconnected));
    const connectionEstablished$ = of(this.hubConnection.state === signalR.HubConnectionState.Disconnected);


    const listenForGame = gameIdObs.pipe(concatMap(gId => {
      console.log('LISTEN FOR GAME ' + gId);
      this.listenForGameEvents(gId);
      return of(true);
    }));

     const v1 = connectionEstablished$.pipe(
       filter(disconnected => disconnected),
       concatMap(val => {
         return connectionStart();
       }),
       filter(connected => connected),
       concatMap(connected => listenForGame)
     );

     const v2 = connectionEstablished$.pipe(
         filter(disconnected => !disconnected),
         concatMap(val => {
           return listenForGame;
         })
     );

    // const x = connectionEstablished$.pipe(
    //   switchMap(e => iif(() => e, connectionStart().pipe(
    //     filter(con => con),
    //     concatMap(() => listenForGame)), listenForGame)));
    // return x;
    return merge(v1, v2);
  }

  public listenForGameEvents(gameId: GameId) {
    this.hubConnection.send('JoinGroup', gameId);
  }

  public stopListeningForGameEvents(gameId: GameId) {
    console.log('STOP LISTENING' + gameId);
    this.hubConnection.send('LeaveGroup', gameId);
  }

  public disconnect() {
    return this.hubConnection.stop();
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
