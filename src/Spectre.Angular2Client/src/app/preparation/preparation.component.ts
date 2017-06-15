/*
 * preparation.component.ts
 * Component of single preparation.
 *
   Copyright 2017 Grzegorz Mrukwa

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

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
  public Layout: any;
  public Data: any;
  public Options: any;

  constructor(
      private route: ActivatedRoute,
  ) { }

  ngOnInit() {
      this.route.params.subscribe(params => {
          this.id = Number.parseInt(params['id']);
      this.Data = [
          {
              x: [1, 2, 3, 4, 5],
              y: [1, 2, 4, 8, 16],
              name: 'Sample data'
          }];
      this.Layout = {
          height: 500,
          width: 1200
      };
      });
  }

}
