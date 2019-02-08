import { TestBed, inject } from '@angular/core/testing';

import { ModService } from './mod.service';

describe('ModService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ModService]
    });
  });

  it('should be created', inject([ModService], (service: ModService) => {
    expect(service).toBeTruthy();
  }));
});
