import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleCarPageComponent } from './single-car-page.component';

describe('SingleCarPageComponent', () => {
  let component: SingleCarPageComponent;
  let fixture: ComponentFixture<SingleCarPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SingleCarPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SingleCarPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
