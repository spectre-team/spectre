import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import {
  MatCardModule,
  MatToolbarModule,
} from '@angular/material';

import { ModellingComponent } from './modelling.component';

describe('ModellingComponent', () => {
  let component: ModellingComponent;
  let fixture: ComponentFixture<ModellingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModellingComponent ],
      imports: [
        MatCardModule,
        MatToolbarModule,
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModellingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
