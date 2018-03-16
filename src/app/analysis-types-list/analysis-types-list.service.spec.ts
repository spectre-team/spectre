import { TestBed, inject, getTestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';
import 'rxjs/add/observable/of';

import {AnalysisTypesListService} from "./analysis-types-list.service";

describe('AnalysisTypesListService', () => {
  let injector: TestBed;
  let service: AnalysisTypesListService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AnalysisTypesListService]
    });
    injector = getTestBed();
    service = injector.get(AnalysisTypesListService);
    httpMock = injector.get(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created',()  => {
    expect(service).toBeTruthy();
  });
});
