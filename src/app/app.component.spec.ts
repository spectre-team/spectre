/*
 * app.component.spec.ts
 * Unit tests for application root element.
 *
   Copyright 2017 Sebastian Pustelnik, Grzegorz Mrukwa, Daniel Babiak

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

import { TestBed, async } from '@angular/core/testing';

import { AppComponent } from './app.component';

import { Http, BaseRequestOptions } from '@angular/http';

import { PreparationListComponent } from './preparations/preparation-list/preparation-list.component';

import { MockBackend } from '@angular/http/testing';

import { RouterTestingModule } from '@angular/router/testing';
import {
  MatButtonModule,
  MatCardModule, MatExpansionModule, MatListModule, MatSidenavModule,
  MatToolbarModule
} from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {AnalysisTypesListModule} from "./analysis-types-list/analysis-types-list.module";
import {MainPageComponent} from "./main-page/main-page.component";
import {UploadComponent} from "./upload/upload.component";
import {PageNotFoundComponent} from "./page-not-found/page-not-found.component";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AnalysisViewComponent} from "./analysis-view/analysis-view/analysis-view.component";
import {NewAnalysisViewComponent} from "./new-analysis-view/new-analysis-view/new-analysis-view.component";
import {GenericFormModule} from "./generic-form/generic-form.module";
import {HttpClientModule} from "@angular/common/http";

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
          [
            RouterTestingModule,
            MatListModule,
            MatSidenavModule,
            MatToolbarModule,
            BrowserAnimationsModule,
            AnalysisTypesListModule,
            FormsModule,
            ReactiveFormsModule,
            MatCardModule,
            GenericFormModule,
            MatExpansionModule,
            MatButtonModule,
            HttpClientModule
          ]
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
      ],
      declarations: [
        AppComponent,
        PreparationListComponent,
        MainPageComponent,
        UploadComponent,
        PageNotFoundComponent,
        AnalysisViewComponent,
        NewAnalysisViewComponent,
      ],
    }).compileComponents();
  }));

  it('should create the app', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));
});
