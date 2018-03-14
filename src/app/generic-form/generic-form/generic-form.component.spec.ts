/*
 * generic-form.component.spec.ts
 * Tests generic form.
 *
   Copyright 2018 Grzegorz Mrukwa

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
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

import { JsonSchemaFormModule, MaterialDesignFrameworkModule } from 'angular2-json-schema-form';

import { GenericFormFetchService } from '../generic-form-fetch.service';
import { GenericFormComponent } from './generic-form.component';

const schema = {
  properties: {
    AnalysisName: {
      title: 'Name of your analysis',
      type: 'string'
    }
  },
  required: ['AnalysisName'],
  title: 'DiviK',
  type: 'object'
};

const layout = [
  {
    key: 'AnalysisName',
    title: 'Name of the analysis'
  }
];

const someSchemaUrl = 'schema/url';
const someLayoutUrl = 'layout/url';

class MockFetchService {
  getSchema(schemaUrl: string): any {
    return new Observable(observer => {
      if (schemaUrl !== someSchemaUrl) {
        observer.error('Schema URL is wrong.');
      }
      observer.next(schema);
      observer.complete();
    });
  }

  getLayout(layoutUrl: string): any {
    return new Observable(observer => {
      if (layoutUrl !== someLayoutUrl) {
        observer.error('Layout URL is wrong.');
      }
      observer.next(layout);
      observer.complete();
    });
  }
}

describe('GenericFormComponent', () => {
  let fetchService: GenericFormFetchService;
  let component: GenericFormComponent;
  let fixture: ComponentFixture<GenericFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenericFormComponent ],
      imports: [
        BrowserModule,
        BrowserAnimationsModule,
        MaterialDesignFrameworkModule,
        JsonSchemaFormModule.forRoot(MaterialDesignFrameworkModule),
      ],
      providers: [
        {provide: GenericFormFetchService, useClass: MockFetchService},
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fetchService = TestBed.get(GenericFormFetchService);
    fixture = TestBed.createComponent(GenericFormComponent);
    component = fixture.componentInstance;
    component.schemaUrl = someSchemaUrl;
    component.layoutUrl = someLayoutUrl;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('downloads schema from its url', () => {
    expect(component.schema).toBe(schema);
  });

  it('downloads layout from its url', () => {
    expect(component.layout).toBe(layout);
  })
});
