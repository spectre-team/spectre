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
import {
  MatSliderModule, MatInputModule, MatButtonModule, MatTabsModule, MatExpansionModule,
  MatSelectModule
} from '@angular/material';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import {GrowlModule, TabViewModule} from 'primeng/primeng';
import { ActivatedRoute } from '@angular/router';
import { MockActivatedRoute } from '../../../mocks/mock-activated-router';
import { RouterTestingModule } from '@angular/router/testing';
import { Observable } from 'rxjs/Observable';
import { BlockUIModule } from 'ng-block-ui';

import { PlotlyModule } from '../../plotly/plotly.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PreparationComponent } from './preparation.component';
import { SpectrumService } from '../../spectra/shared/spectrum.service';
import { HeatmapService } from '../../heatmaps/shared/heatmap.service';
import { SpectrumComponent } from '../../spectra/spectrum/spectrum.component';
import { HeatmapComponent } from '../../heatmaps/heatmap/heatmap.component';
import { MessagesService, MessagesComponent } from 'ng2-messages/ng2-messages';
import { DivikComponent } from '../../divik-results/divik/divik.component';
import { DivikService } from '../../divik-results/shared/divik.service';
import {AnalysisListComponent} from '../../algoritms/shared/analysis-list.component';
import {AnalysisComponent} from '../../algoritms/shared/analysis.component';
import {AnalysisListService} from '../../algoritms/shared/analysis-list.service';
import {AnalysisService} from '../../algoritms/shared/analysis.service';

describe('PreparationComponent', () => {
  let component: PreparationComponent;
  let fixture: ComponentFixture<PreparationComponent>;
  let mockActivatedRoute: MockActivatedRoute;

  beforeEach(async(() => {
    mockActivatedRoute = new MockActivatedRoute(Observable.of({id: '100'}));
    TestBed.configureTestingModule({
      declarations: [ PreparationComponent, SpectrumComponent, HeatmapComponent, DivikComponent, MessagesComponent, AnalysisListComponent,
        AnalysisComponent],
      imports: [RouterTestingModule, PlotlyModule, MatSliderModule, FormsModule, GrowlModule, TabViewModule, MatExpansionModule,
        MatInputModule, MatButtonModule, MatTabsModule, BlockUIModule, BrowserAnimationsModule, MatSelectModule
      ],
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
          HeatmapService,
          MessagesService,
          DivikService,
          AnalysisListService,
          AnalysisService
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
