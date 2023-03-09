import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MiniCarsPageComponent } from './mini-cars-page.component';

describe('MiniCarsPageComponent', () => {
  let component: MiniCarsPageComponent;
  let fixture: ComponentFixture<MiniCarsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MiniCarsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MiniCarsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
