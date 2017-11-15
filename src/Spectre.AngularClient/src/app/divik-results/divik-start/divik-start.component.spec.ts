/*
 * divik-start.component.spec.ts
 * Unit tests for single divik component.
 *
 Copyright 2017 Sebastian Pustelnik

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
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DivikStartComponent } from './divik-start.component';
import { DivikService } from '../shared/divik.service';
import { HeatmapComponent } from '../../heatmaps/heatmap/heatmap.component';
import {MatInputModule, MatButtonModule, MatExpansionModule, MatSelectModule} from '@angular/material';

describe('DivikStartComponent', () => {
  let component: DivikStartComponent;
  let fixture: ComponentFixture<DivikStartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [DivikStartComponent, HeatmapComponent],
      imports: [RouterTestingModule, PlotlyModule, MatInputModule, MatSelectModule,
        MatButtonModule, MatExpansionModule, BrowserAnimationsModule],
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
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DivikStartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
