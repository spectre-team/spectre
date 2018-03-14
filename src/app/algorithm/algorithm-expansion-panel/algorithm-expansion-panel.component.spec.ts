import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClient } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

import {
  MatExpansionModule,
  MatDividerModule,
} from '@angular/material';

import { AlgorithmListComponent } from '../algorithm-list/algorithm-list.component';
import { AlgorithmExpansionPanelComponent } from './algorithm-expansion-panel.component';


class MockHttpClient {
  get()  {
    return new Observable(observer => {
      observer.next({analysis: ['divik']});
      observer.complete();
    });
  }
}


describe('AlgorithmExpansionPanelComponent', () => {
  let mockHttpClient: HttpClient;
  let component: AlgorithmExpansionPanelComponent;
  let fixture: ComponentFixture<AlgorithmExpansionPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AlgorithmExpansionPanelComponent,
        AlgorithmListComponent,
      ],
      imports: [
        BrowserAnimationsModule,
        MatExpansionModule,
        MatDividerModule,
      ],
      providers: [
        {provide: HttpClient, useClass: MockHttpClient},
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    mockHttpClient = TestBed.get(HttpClient);
    fixture = TestBed.createComponent(AlgorithmExpansionPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
