import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminManageExistingCarsPageComponent } from './admin-manage-existing-cars-page.component';

describe('AdminManageExistingCarsPageComponent', () => {
  let component: AdminManageExistingCarsPageComponent;
  let fixture: ComponentFixture<AdminManageExistingCarsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminManageExistingCarsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminManageExistingCarsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
