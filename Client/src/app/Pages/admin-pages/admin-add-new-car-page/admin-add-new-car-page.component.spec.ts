import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAddNewCarPageComponent } from './admin-add-new-car-page.component';

describe('AdminAddNewCarPageComponent', () => {
  let component: AdminAddNewCarPageComponent;
  let fixture: ComponentFixture<AdminAddNewCarPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminAddNewCarPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminAddNewCarPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
