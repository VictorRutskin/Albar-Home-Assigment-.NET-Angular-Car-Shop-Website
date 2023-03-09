import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TruckCarsPageComponent } from './truck-cars-page.component';

describe('TruckCarsPageComponent', () => {
  let component: TruckCarsPageComponent;
  let fixture: ComponentFixture<TruckCarsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TruckCarsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TruckCarsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
