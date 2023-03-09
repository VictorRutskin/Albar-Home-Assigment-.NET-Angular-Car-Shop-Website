import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarsCardsComponent } from './cars-cards.component';

describe('CarsCardsComponent', () => {
  let component: CarsCardsComponent;
  let fixture: ComponentFixture<CarsCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarsCardsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarsCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
