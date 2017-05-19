import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { PreparationRoutingModule } from './preparation-routing.module';

@Component({
  selector: 'app-preparation',
  templateUrl: './preparation.component.html',
  styleUrls: ['./preparation.component.css']
})
export class PreparationComponent implements OnInit {

  id: number = null;

  constructor(
      private route: ActivatedRoute,
  ) { }

  ngOnInit() {
      this.route.params.subscribe(params => {
          this.id = Number.parseInt(params['id']);
      });
  }

}
