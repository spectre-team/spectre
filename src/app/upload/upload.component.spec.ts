/*
 * upload.component.spec.ts
 * Unit tests for single upload component.
 *
 Copyright 2018 Sebastian Pustelnik

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
import { UploadComponent } from "./upload.component";
import { UploadService } from "./shared/upload.service";
import { FormsModule } from "@angular/forms";
import { By } from "@angular/platform-browser";


describe('UploadComponent', () => {
  let component: UploadComponent;
  let fixture: ComponentFixture<UploadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [UploadComponent],
      imports: [RouterTestingModule, FormsModule],
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
        UploadService
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('invalid form when text fields are empty', () => {
    let form = fixture.debugElement.query(By.css('form'));
    expect(form.nativeElement.valid).toBeFalsy();
  });

  it('enabled button when text fields are not empty', () => {
    component.nameInput='name';
    component.linkInput='www.link.com';
    fixture.detectChanges();
    let button = fixture.debugElement.query(By.css('button'));
    expect(button.nativeElement.disabled).toBeFalsy();
  })
});
