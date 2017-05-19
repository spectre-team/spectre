import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PreparationComponent } from './preparation.component';

import { ActivatedRoute } from '@angular/router';
import { MockActivatedRoute } from '../../mocks/mock-activated-router';
import { RouterTestingModule } from '@angular/router/testing';
import { Observable } from 'rxjs/Observable';

describe('PreparationComponent', () => {
  let component: PreparationComponent;
  let fixture: ComponentFixture<PreparationComponent>;
  let mockActivatedRoute: MockActivatedRoute;

  beforeEach(async(() => {
    mockActivatedRoute = new MockActivatedRoute(Observable.of({id: '100'}));
    TestBed.configureTestingModule({
      declarations: [ PreparationComponent ],
      imports: [RouterTestingModule],
      providers: [
          { provide: ActivatedRoute, useValue: mockActivatedRoute }
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
