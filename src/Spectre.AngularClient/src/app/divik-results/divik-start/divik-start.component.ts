/*
 * divik-start.component.ts
 * Component to start divik task
 *
   Copyright 2017 Sebastian Pustelnik

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

import { Component, Input, OnInit } from '@angular/core';

import { DivikService } from '../shared/divik.service';
import { DivikConfig } from '../shared/divik-config';
import 'rxjs/Rx';


@Component({
  selector: 'app-divik-start',
  templateUrl: './divik-start.component.html',
  styleUrls: ['./divik-start.component.css'],
})
export class DivikStartComponent implements OnInit {
  public divikConfig: DivikConfig;
  public divikConfigKeys: Array<string>;
  public datasets = [ {filename:  'Peptyd1'}, {filename: 'Peptyd2'}, {filename: 'Peptyd3'}, {filename: 'Peptyd4'},
    {filename: 'Lipid1'}, {filename: 'Lipid2'}, {filename: 'Lipid3'}, {filename: 'Lipid4'}];
  public selectedValue = 'Peptyd1';

  constructor(private divikService: DivikService) {
  }

  ngOnInit() {
    this.createDivikConfig();
  }

  startDivik() {
    this.divikService.startDivik(this.selectedValue, this.divikConfig);
  }

  changeValue(key, value) {
    this.divikConfig[key] = value;
  }

  createDivikConfig() {
    this.divikConfig = <DivikConfig>({
      'Max K': 10,
      Level: 3,
      'Using levels': true,
      Amplitude: true,
      Variance: true,
      'Percent size limit': 0.001,
      'Feature preservation limit': 0.05,
      Metric: 'Pearson',
      'Plotting partitions': false,
      'Plotting recursively': false,
      'Plotting decomposition': false,
      'Plotting decomposition recursively': false,
      'Max decomposition components': 10
    });
    this.divikConfigKeys = Object.keys(this.divikConfig);
  }
}
