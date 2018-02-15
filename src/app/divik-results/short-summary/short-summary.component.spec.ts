import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShortSummaryComponent } from './short-summary.component';

describe('ShortSummaryComponent', () => {
  let component: ShortSummaryComponent;
  let fixture: ComponentFixture<ShortSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShortSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShortSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
