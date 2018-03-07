/*
 * plotly.component.ts
 * Component wrapping Plotly library.
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

import { AfterViewInit, ChangeDetectorRef, Component, OnChanges, OnInit, Input } from '@angular/core';
import { GuidService } from './guid.service';

declare var Plotly: any;

@Component({
  selector: 'app-plotly',
  templateUrl: './plotly.component.html',
  styleUrls: ['./plotly.component.css'],
  providers: [ GuidService ]
})
export class PlotlyComponent implements OnInit, AfterViewInit, OnChanges {

  @Input() data: any;
  @Input() layout: any;
  @Input() options: any;
  @Input() displayRawData: boolean;
  @Input() onClickFunction: Function;
  randomId: string;
  initialized: boolean;

  constructor(
      private cdRef: ChangeDetectorRef,
      private guid: GuidService
  ) {
    this.randomId = guid.next();
    this.initialized = false;
  }

  ngOnInit() {
    console.log('[PlotlyComponent] ngOnInit plotly-' + this.randomId);
  }

  ngOnChanges() {
    if (this.initialized) {
      console.log('[PlotlyComponent] update plotly-' + this.randomId);
      console.log('[PlotlyComponent] data: ');
      console.log(this.data);
      console.log('[PlotlyComponent] layout: ');
      console.log(this.layout);
      const div = document.getElementById('plotly-' + this.randomId);
      Plotly.deleteTraces(div, 0);
      Plotly.addTraces(div, this.data);
      console.log('[PlotlyComponent] updated plotly-' + this.randomId);
    }
  }

  ngAfterViewInit() {
      console.log('[PlotlyComponent] nfAfterViewInit');
      Plotly.newPlot('plotly-' + this.randomId, this.data, this.layout, this.options);
      const plotlyElement = <PlotHTMLElement> document.getElementById('plotly-' + this.randomId);
      plotlyElement.on('plotly_click', (event) => this.onClickFunction(event));
      this.initialized = true;
      this.cdRef.detectChanges();
      console.log('[PlotlyComponent] created plotly-' + this.randomId);
  }

}
