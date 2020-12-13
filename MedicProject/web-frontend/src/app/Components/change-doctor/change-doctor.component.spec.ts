import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeDoctorComponent } from './change-doctor.component';

describe('ChangeDoctorComponent', () => {
  let component: ChangeDoctorComponent;
  let fixture: ComponentFixture<ChangeDoctorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeDoctorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeDoctorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
