import { TestBed } from '@angular/core/testing';

import { RxNeverCompletedGameService } from "./rx.never-completed-game.service";

describe('Rx.NeverCompletedGameService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
      const service: RxNeverCompletedGameService = TestBed.get(RxNeverCompletedGameService);
    expect(service).toBeTruthy();
  });
});
