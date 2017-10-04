/*
 * divik.component.spec.ts
 * Unit tests for single divik component.
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
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DivikComponent } from './divik.component';
import { DivikService } from '../shared/divik.service';
import { HeatmapComponent } from '../../heatmaps/heatmap/heatmap.component';
import { MdInputModule, MdButtonModule, MdExpansionModule} from '@angular/material';

describe('DivikComponent', () => {
  let component: DivikComponent;
  let fixture: ComponentFixture<DivikComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [DivikComponent, HeatmapComponent],
      imports: [RouterTestingModule, PlotlyModule, MdInputModule, MdButtonModule, MdExpansionModule, BrowserAnimationsModule],
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
    fixture = TestBed.createComponent(DivikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
