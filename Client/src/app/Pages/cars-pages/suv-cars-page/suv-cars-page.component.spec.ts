import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuvCarsPageComponent } from './suv-cars-page.component';

describe('SuvCarsPageComponent', () => {
  let component: SuvCarsPageComponent;
  let fixture: ComponentFixture<SuvCarsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SuvCarsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SuvCarsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
