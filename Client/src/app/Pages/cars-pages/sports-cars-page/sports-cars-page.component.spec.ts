import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SportsCarsPageComponent } from './sports-cars-page.component';

describe('SportsCarsPageComponent', () => {
  let component: SportsCarsPageComponent;
  let fixture: ComponentFixture<SportsCarsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SportsCarsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SportsCarsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
