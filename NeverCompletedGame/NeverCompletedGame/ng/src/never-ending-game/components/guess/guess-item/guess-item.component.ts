import { Subject } from 'rxjs';
import { IGuess } from './../../../model/game.interface';
import { Component, OnInit, Input, ElementRef, ViewChild, OnDestroy } from '@angular/core';

@Component({
  selector: 'ncg-guess-item',
  templateUrl: './guess-item.component.html',
  styleUrls: ['./guess-item.component.scss']
})
export class GuessItemComponent implements OnInit, OnDestroy {

  @ViewChild('guessRef', { static: false })
  pathRef: ElementRef<HTMLDivElement>;

  @Input()
  public guess: IGuess;

  private destroy$ = new Subject<null>();

  constructor() { }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
  }


}
