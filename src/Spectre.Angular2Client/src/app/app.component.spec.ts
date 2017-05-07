import { TestBed, async } from '@angular/core/testing';

import { AppComponent } from './app.component';

import { Http, BaseRequestOptions } from '@angular/http';

import { PreparationListComponent } from './preparation-list/preparation-list.component';

import { MockBackend } from '@angular/http/testing';

describe('AppComponent', () => {
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
      declarations: [
        AppComponent,
        PreparationListComponent
      ],
    }).compileComponents();
  }));

  it('should create the app', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));

  it(`should have as title 'Spectre'`, async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('Spectre');
  }));

  it('should render title in a h1 tag', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('h1').textContent).toContain('Spectre');
  }));
});
