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
import { SpectrumService } from './spectrum.service';

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
      private spectrumService: SpectrumService
  ) { }

  ngOnInit() {
      this.route.params.subscribe(params => {
        console.log('[PreparationComponent] ngOnInit');
        this.id = Number.parseInt(params['id']);
        console.log('[PreparationComponent] parsed id');
        this.spectrumService.get(this.id, 1).subscribe(((spectrum) => {
            console.log('[PreparationComponent] spectrum data read:');
            console.log(spectrum);
            this.Data = [{
                    x: spectrum.mz,
                    y: spectrum.intensities,
                    name: `Spectrum ${spectrum.id}, (X=${spectrum.x},Y=${spectrum.y})`
                }];
            console.log('[PreparationComponent] assigned data:');
            console.log(this.Data);
        }));
        console.log('[PreparationComponent] layout setup');
        this.Layout = {
          height: 500,
          width: 1200
        };
        this.Data = [{
                x: [1, 2, 3, 4],
                y: [1, 4, 9, 16],
                name: `Sample data`
        }];
        this.Options = [];
        console.log('[PreparationComponent] plot layout set');
      });
  }

}
