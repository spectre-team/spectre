/*
 * divik.component.ts
 * Component of single divik result.
 *
   Copyright 2017 Grzegorz Mrukwa, Sebastian Pustelnik, Daniel Babiak

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

import { Component, Input, OnInit} from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

import { Heatmap } from '../../heatmaps/shared/heatmap';
import { DivikService } from '../shared/divik.service';
import { DivikConfig } from '../shared/divik-config';
import 'rxjs/Rx';


@Component({
  selector: 'app-divik',
  templateUrl: './divik.component.html',
  styleUrls: ['./divik.component.css'],
})
export class DivikComponent implements OnInit {
  public data: any;
  public divikConfig: DivikConfig;
  public downloadJsonHref: SafeUrl;
  public divikConfigKeys: Array<string>;

  @Input() public preparationId;
  constructor(
      private divikService: DivikService,
      private sanitizer: DomSanitizer,
  ) { }

  ngOnInit() {
    this.divikService
      .get(this.preparationId, 1, 1)
      .subscribe(heatmap => this.data = this.toHeatmapDataset(heatmap));
    this.divikService
      .getConfig(this.preparationId, 1)
      .subscribe(config => this.buildConfigInfo(config));
  }

  toHeatmapDataset(heatmap: Heatmap) {
    return [{
      z: heatmap.data,
      type: 'heatmap'
    }];
  }

  buildConfigInfo(config: DivikConfig) {
    this.divikConfig = config;
    this.divikConfigKeys = Object.keys(this.divikConfig);
    this.generateDownloadJsonUri();
  }

  changeLevel(value) {
    if (value > 0 && value <= this.divikConfig.Level) {
      this.divikService
        .get(this.preparationId, 1, value)
        .subscribe(heatmap => this.data = this.toHeatmapDataset(heatmap));
    }
  }

  generateDownloadJsonUri() {
    const theJSON = JSON.stringify(this.divikConfig);
    this.downloadJsonHref = this.sanitizer.bypassSecurityTrustUrl('data:text/json;charset=UTF-8,' + encodeURIComponent(theJSON));
  }
}
