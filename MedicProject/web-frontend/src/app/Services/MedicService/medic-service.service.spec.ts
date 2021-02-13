import { TestBed } from '@angular/core/testing';

import { MedicService} from './medic.service';

describe('MedicServiceService', () => {
  let service: MedicService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MedicService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
