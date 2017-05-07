import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PreparationListComponent } from './preparation-list.component';

import { Http, BaseRequestOptions } from '@angular/http';

import { MockBackend } from '@angular/http/testing';

describe('PreparationListComponent', () => {
  let component: PreparationListComponent;
  let fixture: ComponentFixture<PreparationListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      providers: [
          MockBackend,
          BaseRequestOptions,
          {
            provide: Http,
            useFactory: (backendInstance: MockBackend, defaultOptions: BaseRequestOptions) => {
              return new Http(backendInstance, defaultOptions);
            },
            deps: [MockBackend, BaseRequestOptions]
          }
      ],
      declarations: [ PreparationListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PreparationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should provide description', () => {
      const compiled = fixture.debugElement.nativeElement;
      expect(compiled.querySelector('h2').textContent).toContain('preparation');
  });
});
