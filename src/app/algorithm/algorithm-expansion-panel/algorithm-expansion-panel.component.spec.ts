import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlgorithmExpansionPanelComponent } from './algorithm-expansion-panel.component';

describe('AlgorithmExpansionPanelComponent', () => {
  let component: AlgorithmExpansionPanelComponent;
  let fixture: ComponentFixture<AlgorithmExpansionPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlgorithmExpansionPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlgorithmExpansionPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
