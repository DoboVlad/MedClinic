import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveAccountComponent } from './approve-account.component';

describe('ApproveAccountComponent', () => {
  let component: ApproveAccountComponent;
  let fixture: ComponentFixture<ApproveAccountComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApproveAccountComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
