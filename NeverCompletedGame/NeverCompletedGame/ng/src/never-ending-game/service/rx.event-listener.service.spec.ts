import { TestBed } from '@angular/core/testing';

import { RxEventListenerService } from './rx.event-listener.service';

describe('Rx.EventListenerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RxEventListenerService = TestBed.get(RxEventListenerService);
    expect(service).toBeTruthy();
  });
});
