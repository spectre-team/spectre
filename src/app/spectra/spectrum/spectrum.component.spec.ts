/*
 * spectrum.component.spec.ts
 * Unit tests for single spectrum component.
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

import { RouterTestingModule } from '@angular/router/testing';

import { PlotlyModule } from '../../plotly/plotly.module';

import { SpectrumService } from '../shared/spectrum.service';
import { SpectrumComponent } from './spectrum.component';

describe('SpectrumComponent', () => {
  let component: SpectrumComponent;
  let fixture: ComponentFixture<SpectrumComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SpectrumComponent],
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
        SpectrumService
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpectrumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should properly define default spectrumLayout', () => {
    component.ngOnInit();
    expect(component.spectrumLayout).toEqual({ height: 500, width: 1200 });
  });

  it('should properly define spectrumLayout', () => {
    component.height = 120;
    component.width = 120;
    component.ngOnInit();
    expect(component.spectrumLayout).toEqual({ height: 120, width: 120 });
  });
});
