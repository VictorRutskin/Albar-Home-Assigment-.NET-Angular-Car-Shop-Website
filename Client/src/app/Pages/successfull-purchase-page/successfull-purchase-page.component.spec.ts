import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuccessfullPurchasePageComponent } from './successfull-purchase-page.component';

describe('SuccessfullPurchasePageComponent', () => {
  let component: SuccessfullPurchasePageComponent;
  let fixture: ComponentFixture<SuccessfullPurchasePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SuccessfullPurchasePageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SuccessfullPurchasePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
