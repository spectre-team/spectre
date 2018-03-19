import { TestBed, inject } from '@angular/core/testing';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';
import 'rxjs/add/observable/of';

import { FinishedAnalysesService } from './finished-analyses.service';

describe('FinishedAnalysesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        FinishedAnalysesService,
        {provide: HttpClient, useValue: {get: (url: string) => Observable.of([
          {name: 'First DiviK', id: '1'},
          {name: 'Another DiviK', id: '2'},
        ])}}
      ]
    });
  });

  it('should be created', inject([FinishedAnalysesService], (service: FinishedAnalysesService) => {
    expect(service).toBeTruthy();
  }));

  it('downloads finished analyses from the API', inject([FinishedAnalysesService], (service: FinishedAnalysesService) => {
    service.getFinished('divik').subscribe(analyses => {
      expect(analyses[0].id).toBe('1')
      expect(analyses[0].name).toBe('First DiviK')
      expect(analyses[1].id).toBe('2')
      expect(analyses[1].name).toBe('Another DiviK')
    })
  }))
});
