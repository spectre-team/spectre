import { TestBed, inject } from '@angular/core/testing';
import { Http, BaseRequestOptions, Response, ResponseOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { HeatmapService } from './heatmap.service';

describe('HeatmapService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
          MockBackend,
          BaseRequestOptions,
          {
            provide: Http,
            useFactory: (backendInstance: MockBackend, defaultOptions: BaseRequestOptions) => {
              return new Http(backendInstance, defaultOptions);
            },
            deps: [MockBackend, BaseRequestOptions]
          },
          HeatmapService
      ]
    });
  });

  it('should inject service', inject([HeatmapService], (service: HeatmapService) => {
    expect(service).toBeTruthy();
  }));
});
