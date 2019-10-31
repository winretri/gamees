import { TestBed } from '@angular/core/testing';

import { RxGuessService } from './rx.guess.service';

describe('RxGuessService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RxGuessService = TestBed.get(RxGuessService);
    expect(service).toBeTruthy();
  });
});
