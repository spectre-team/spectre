import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnalysisTypesListComponent } from './analysis-types-list.component';

describe('AnalysisTypesListComponent', () => {
  let component: AnalysisTypesListComponent;
  let fixture: ComponentFixture<AnalysisTypesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnalysisTypesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnalysisTypesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
