/*
 * spectrum.service.spec.ts
 * Unit tests for service providing spectrum.
 *
   Copyright 2017 Sebastian Pustelnik, Grzegorz Mrukwa

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

import { TestBed, inject } from '@angular/core/testing';
import { Http, BaseRequestOptions, Response, ResponseOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { SpectrumService } from './spectrum.service';

describe('SpectrumService', () => {
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
          SpectrumService
      ]
    });
  });

  it('should inject SpectrumService', inject([SpectrumService], (service: SpectrumService) => {
    expect(service).toBeTruthy();
  }));

  it('should parse preparation 1 first spectrum data', inject([SpectrumService, MockBackend],
      (spectrumService: SpectrumService, mockBackend: MockBackend) => {
    mockBackend.connections.subscribe((connection: MockConnection) => {
      const options = new ResponseOptions({
        body: JSON.stringify({ Id: 1, X: 10, Y: 20, Mz: [1, 2, 3], Intensities: [1.11, 4.44, 9.99] })
      });
      connection.mockRespond(new Response(options));
    });

    spectrumService.get(1, 1).subscribe((spectrum) => {
        expect(spectrum.id).toEqual(1);
        expect(spectrum.x).toEqual(10);
        expect(spectrum.y).toEqual(20);
        expect(spectrum.mz.length).toEqual(3);
        expect(spectrum.mz).toEqual([1, 2, 3]);
        expect(spectrum.intensities.length).toEqual(3);
        expect(spectrum.intensities).toEqual([1.11, 4.44, 9.99]);
      });
  }));
});
