/*
 * heatmap.component.spec.ts
 * Unit tests for single heatmap component.
 *
 Copyright 2017 Grzegorz Mrukwa, Sebastian Pustelnik, Daniel Babiak

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
import { MockBackend} from '@angular/http/testing';

import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';

import { PlotlyModule } from '../../plotly/plotly.module';

import { HeatmapService } from '../shared/heatmap.service';
import { HeatmapComponent } from './heatmap.component';

describe('HeatmapComponent', () => {
  let component: HeatmapComponent;
  let fixture: ComponentFixture<HeatmapComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [HeatmapComponent],
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
        HeatmapService
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeatmapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should properly give default heatmapLayout', () => {
    component.ngOnInit();
    expect(component.heatmapLayout).toEqual
    ({ height: 600, width: 600, xaxis: ({ autotick: false, dtick: 1, ticklen: 1, tickwidth: 1, showticklabels: false }),
      yaxis: ({ autotick: false, dtick: 1, ticklen: 1, tickwidth: 1, showticklabels: false })});
  });

  it('should properly define heatmapLayout', () => {
    component.height = 120;
    component.width = 120;
    component.ngOnInit();
    expect(component.heatmapLayout).toEqual({ height: 120, width: 120,
      xaxis: ({ autotick: false, dtick: 1, ticklen: 1, tickwidth: 1, showticklabels: false }),
      yaxis: ({ autotick: false, dtick: 1, ticklen: 1, tickwidth: 1, showticklabels: false })});
  });

});
