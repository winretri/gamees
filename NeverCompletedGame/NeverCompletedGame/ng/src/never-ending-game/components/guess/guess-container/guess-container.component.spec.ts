import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GuessContainerComponent } from './guess-container.component';

describe('GuessContainerComponent', () => {
  let component: GuessContainerComponent;
  let fixture: ComponentFixture<GuessContainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GuessContainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GuessContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
