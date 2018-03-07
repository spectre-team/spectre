/*
 * preparation.service.spec.ts
 * Unit tests for service providing preparations list.
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

import { PreparationService } from './preparation.service';

import { Http, BaseRequestOptions, Response, ResponseOptions } from '@angular/http';

import { MockBackend, MockConnection } from '@angular/http/testing';

describe('PreparationService', () => {

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
          PreparationService
      ]
    });
  });

  it('should be able to inject service', inject([PreparationService], (service: PreparationService) => {
    expect(service).toBeTruthy();
  }));

  it('should receive 2 preparations', inject([PreparationService, MockBackend],
      (preparationService: PreparationService, mockBackend: MockBackend) => {
    mockBackend.connections.subscribe((connection: MockConnection) => {
      const options = new ResponseOptions({
        body: JSON.stringify([{ Id: 1, Name: 'Preparation 1' },
                              { Id: 2, Name: 'Preparation 2' }])
      });
      connection.mockRespond(new Response(options));
    });

    preparationService
      .getAll()
      .subscribe((response) => {
        expect(response.length).toEqual(2);
      });
  }));

  it('should parse preparation 1 data', inject([PreparationService, MockBackend],
      (preparationService: PreparationService, mockBackend: MockBackend) => {
    mockBackend.connections.subscribe((connection: MockConnection) => {
      const options = new ResponseOptions({
        body: JSON.stringify([{ Id: 1, Name: 'Preparation 1' },
                              { Id: 2, Name: 'Preparation 2' }])
      });
      connection.mockRespond(new Response(options));
    });

    preparationService
      .getAll()
      .subscribe((response) => {
        expect(response[0].id).toEqual('1');
        expect(response[0].name).toEqual('Preparation 1');
      });
  }));
});
