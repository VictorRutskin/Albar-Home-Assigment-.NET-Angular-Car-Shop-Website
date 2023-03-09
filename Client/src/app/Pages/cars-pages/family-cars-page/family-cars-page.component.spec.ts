import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FamilyCarsPageComponent } from './family-cars-page.component';

describe('FamilyCarsPageComponent', () => {
  let component: FamilyCarsPageComponent;
  let fixture: ComponentFixture<FamilyCarsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FamilyCarsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FamilyCarsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
