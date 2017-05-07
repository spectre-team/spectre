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
        expect(response[0].id).toEqual(1);
        expect(response[0].name).toEqual('Preparation 1');
      });
  }));
});
