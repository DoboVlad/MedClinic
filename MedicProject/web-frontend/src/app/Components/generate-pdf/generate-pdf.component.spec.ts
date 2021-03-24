import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneratePdfComponent } from './generate-pdf.component';

describe('GeneratePdfComponent', () => {
  let component: GeneratePdfComponent;
  let fixture: ComponentFixture<GeneratePdfComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GeneratePdfComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GeneratePdfComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
