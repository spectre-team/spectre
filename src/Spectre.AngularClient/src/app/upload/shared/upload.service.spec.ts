/*
 * upload.service.spec.ts
 * Unit tests for service providing upload.
 *
   Copyright 2018 Sebastian Pustelnik

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
import { UploadService } from "./upload.service";

describe('UploadService', () => {
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
          UploadService
      ]
    });
  });

  it('should inject UploadService', inject([UploadService], (service: UploadService) => {
    expect(service).toBeTruthy();
  }));

  it('success after return 200', inject([UploadService, MockBackend],
      (uploadService: UploadService, mockBackend: MockBackend) => {
    mockBackend.connections.subscribe((connection: MockConnection) => {
      const options = new ResponseOptions({
        status: 200
      });
      connection.mockRespond(new Response(options));
    });

        uploadService.uploadData('www.link.com', 'name').subscribe((value) => {
        expect(value).toEqual(true);
      });
  }));

  it('error after server failure', inject([UploadService, MockBackend],
    (uploadService: UploadService, mockBackend: MockBackend) => {
      mockBackend.connections.subscribe((connection: MockConnection) => {
        const options = new ResponseOptions({
          status: 500
        });
        connection.mockRespond(new Response(options));
      });

      uploadService.uploadData('www.link.com', 'name').subscribe(() => {},
          error => expect(error).toEqual(false)
    );
    }));
});
