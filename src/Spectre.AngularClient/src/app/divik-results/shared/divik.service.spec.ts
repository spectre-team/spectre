import { TestBed, inject } from '@angular/core/testing';
import { Http, BaseRequestOptions, Response, ResponseOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import {DivikService} from './divik.service';

describe('DivikService', () => {
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
          DivikService
      ]
    });
  });

  it('should inject service', inject([DivikService], (service: DivikService) => {
    expect(service).toBeTruthy();
  }));

  it('should parse preparation 1 first heatmap data', inject([DivikService, MockBackend],
    (divikService: DivikService, mockBackend: MockBackend) => {
      mockBackend.connections.subscribe((connection: MockConnection) => {
         const options = new ResponseOptions({
           body: JSON.stringify({ Id: 1, X: [4, 5, 6], Y: [5, 8, 9 ], Data: [1.11, 4.44, 9.99] })
         });
        connection.mockRespond(new Response(options));
      });

      divikService.get(1, 1, 1).subscribe((divik) => {
        expect(divik.data.length).toEqual(5);
        expect(divik.data[0].length).toEqual(3);
        expect(divik.data[1].length).toEqual(3);
        expect(divik.data[2].length).toEqual(3);
        expect(divik.data[3].length).toEqual(3);
        expect(divik.data[4].length).toEqual(3);
        expect(divik.data[1][1]).toEqual(4.44);
        expect(divik.data[4][0]).toEqual(1.11);
        expect(divik.data[0][2]).toEqual(9.99);
      });
    }));
});
