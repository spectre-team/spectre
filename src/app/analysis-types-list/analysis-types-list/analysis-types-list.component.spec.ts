import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import {MatExpansionModule, MatButtonModule, MatListModule} from "@angular/material";

import { AnalysisTypesListComponent } from './analysis-types-list.component';
import {AnalysisNamesListComponent} from "../analysis-names-list/analysis-names-list.component";
import {AnalysisTypesListService} from "../analysis-types-list.service";
import {HttpClientModule} from "@angular/common/http";

describe('AnalysisTypesListComponent', () => {
  let component: AnalysisTypesListComponent;
  let fixture: ComponentFixture<AnalysisTypesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnalysisTypesListComponent, AnalysisNamesListComponent ],
      imports: [MatListModule, MatExpansionModule, MatButtonModule, HttpClientModule],
      providers: [AnalysisTypesListService]
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
