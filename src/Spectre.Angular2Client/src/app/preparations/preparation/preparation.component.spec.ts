/*
 * preparation.component.spec.ts
 * Unit tests for single preparation component.
 *
   Copyright 2017 Grzegorz Mrukwa

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

import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { Http, BaseRequestOptions, Response, ResponseOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { ActivatedRoute } from '@angular/router';
import { MockActivatedRoute } from '../../../mocks/mock-activated-router';
import { RouterTestingModule } from '@angular/router/testing';
import { Observable } from 'rxjs/Observable';

import { PlotlyModule } from '../../plotly/plotly.module';

import { PreparationComponent } from './preparation.component';
import { SpectrumService } from '../../spectrums/shared/spectrum.service';
import { HeatmapService } from '../../heatmaps/shared/heatmap.service';
import {SpectrumComponent} from '../../spectrums/spectrum/spectrum.component';
import {HeatmapComponent} from '../../heatmaps/heatmap/heatmap.component';

describe('PreparationComponent', () => {
  let component: PreparationComponent;
  let fixture: ComponentFixture<PreparationComponent>;
  let mockActivatedRoute: MockActivatedRoute;

  beforeEach(async(() => {
    mockActivatedRoute = new MockActivatedRoute(Observable.of({id: '100'}));
    TestBed.configureTestingModule({
      declarations: [ PreparationComponent, SpectrumComponent, HeatmapComponent],
      imports: [RouterTestingModule, PlotlyModule],
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
          { provide: ActivatedRoute, useValue: mockActivatedRoute },
          SpectrumService,
          HeatmapService
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PreparationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should contain proper preparation number', () => {
      expect(component.id).toEqual(100);
  });
});
