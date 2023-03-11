import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotAuthoarizedPageComponent } from './not-authoarized-page.component';

describe('NotAuthoarizedPageComponent', () => {
  let component: NotAuthoarizedPageComponent;
  let fixture: ComponentFixture<NotAuthoarizedPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NotAuthoarizedPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NotAuthoarizedPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
