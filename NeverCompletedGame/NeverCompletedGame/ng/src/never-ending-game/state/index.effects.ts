import { GuessEffects } from './guess/effects';
import { GameEffects } from './game/effects';
import { SignalREffects } from 'ngrx-signalr';

export const gameEffects = [
  GameEffects,
  GuessEffects,
  SignalREffects,
];
