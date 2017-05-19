import { ActivatedRoute, ActivatedRouteSnapshot, Params } from '@angular/router';
import { Observable } from 'rxjs/Observable';

class MockActivatedRouteSnapshot extends ActivatedRouteSnapshot {
    params: Observable<Params>;
    constructor(parameters: Observable<Params>) {
        super();
        this.params = parameters;
    }

    toString(): string {
        return '';
    }
}

export class MockActivatedRoute extends ActivatedRoute {
  params: Observable<Params>;

  constructor(parameters: Observable<Params>) {
    super();
    this.params = parameters;
    this.snapshot = new MockActivatedRouteSnapshot(this.params);
  }
}
