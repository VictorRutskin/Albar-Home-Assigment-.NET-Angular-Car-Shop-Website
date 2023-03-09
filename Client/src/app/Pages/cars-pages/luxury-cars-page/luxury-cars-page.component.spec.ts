import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LuxuryCarsPageComponent } from './luxury-cars-page.component';

describe('LuxuryCarsPageComponent', () => {
  let component: LuxuryCarsPageComponent;
  let fixture: ComponentFixture<LuxuryCarsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LuxuryCarsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LuxuryCarsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
