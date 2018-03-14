import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

import { AlgorithmListComponent } from './algorithm-list.component';


class MockHttpClient {
  get()  {
    return new Observable(observer => {
      observer.next({analysis: ['divik']});
      observer.complete();
    });
  }
}


describe('AlgorithmListComponent', () => {
  let component: AlgorithmListComponent;
  let fixture: ComponentFixture<AlgorithmListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlgorithmListComponent ],
      providers: [
        {provide: HttpClient, useClass: MockHttpClient},
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlgorithmListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
