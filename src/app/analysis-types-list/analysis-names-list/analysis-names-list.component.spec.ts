import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatListModule, MatExpansionModule, MatButtonModule } from '@angular/material';
import { AnalysisTypesListComponent } from "../analysis-types-list/analysis-types-list.component";

import { AnalysisNamesListComponent } from './analysis-names-list.component';
import {AnalysisTypesListService} from "../analysis-types-list.service";

describe('AnalysisNamesListComponent', () => {
  let component: AnalysisNamesListComponent;
  let fixture: ComponentFixture<AnalysisNamesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnalysisNamesListComponent, AnalysisTypesListComponent ],
      imports: [MatListModule, MatExpansionModule, MatButtonModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnalysisNamesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
