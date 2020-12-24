import { TestBed } from '@angular/core/testing';

import { UserchatService } from './userchat.service';

describe('UserchatService', () => {
  let service: UserchatService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserchatService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
