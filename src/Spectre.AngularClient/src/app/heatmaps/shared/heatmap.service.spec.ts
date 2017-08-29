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

  it('should parse preparation 1 first heatmap data', inject([HeatmapService, MockBackend],
    (heatmapService: HeatmapService, mockBackend: MockBackend) => {
      mockBackend.connections.subscribe((connection: MockConnection) => {
         const options = new ResponseOptions({
           body: JSON.stringify({ Id: 1, Mz: 853.23, X: [4, 5, 6], Y: [5, 8, 9 ], Intensities: [1.11, 4.44, 9.99] })
         });
        connection.mockRespond(new Response(options));
      });

      heatmapService.get(1, 1).subscribe((heatmap) => {
        expect(heatmap.data.length).toEqual(5);
        expect(heatmap.data[0].length).toEqual(3);
        expect(heatmap.data[1].length).toEqual(3);
        expect(heatmap.data[2].length).toEqual(3);
        expect(heatmap.data[3].length).toEqual(3);
        expect(heatmap.data[4].length).toEqual(3);
        expect(heatmap.data[1][1]).toEqual(4.44);
        expect(heatmap.data[4][0]).toEqual(1.11);
        expect(heatmap.data[0][2]).toEqual(9.99);
      });

      //TODO: test min column and min row
    }));
});
