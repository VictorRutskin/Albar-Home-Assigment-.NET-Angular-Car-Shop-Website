import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSingleCarEditPageComponent } from './admin-single-car-edit-page.component';

describe('AdminSingleCarEditPageComponent', () => {
  let component: AdminSingleCarEditPageComponent;
  let fixture: ComponentFixture<AdminSingleCarEditPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminSingleCarEditPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminSingleCarEditPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
