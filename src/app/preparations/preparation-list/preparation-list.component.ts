/*
 * preparation-list.component.ts
 * Preparations list component.
 *
   Copyright 2017 Sebastian Pustelnik, Grzegorz Mrukwa

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
import { Preparation } from '../shared/preparation';
import { PreparationService } from '../shared/preparation.service';

@Component({
  selector: 'app-preparation-list',
  templateUrl: './preparation-list.component.html',
  styleUrls: ['./preparation-list.component.css'],
  providers: [PreparationService]
})
export class PreparationListComponent implements OnInit {
  preparations: Preparation[] = [];
  constructor(private _preparationService: PreparationService) {
  }


  ngOnInit() {
    this._preparationService
      .getAll()
      .subscribe(p => this.preparations = p);
  }

}
