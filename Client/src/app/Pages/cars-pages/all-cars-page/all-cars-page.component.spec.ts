import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllCarsPageComponent } from './all-cars-page.component';

describe('AllCarsPageComponent', () => {
  let component: AllCarsPageComponent;
  let fixture: ComponentFixture<AllCarsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AllCarsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllCarsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
