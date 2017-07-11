/*
 * spectrum.component.ts
 * Component of single spectrum.
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

import {Component, Input, OnInit} from '@angular/core';

import { SpectrumService } from '../shared/spectrum.service';
import { Spectrum } from '../shared/spectrum';


@Component({
  selector: 'app-spectrum',
  templateUrl: './spectrum.component.html',
  styleUrls: ['./spectrum.component.css']
})

export class SpectrumComponent implements OnInit {

  @Input() public id: number;
  public SpectrumLayout: any;
  public SpectrumData: any;

  public Options: any;

  constructor(
    private spectrumService: SpectrumService,
  ) { }

  ngOnInit() {
      this.spectrumService
        .get(this.id, 1)
        .subscribe(spectrum => this.SpectrumData = this.toSpectrumDataset(spectrum));
      console.log('[SpectrumComponent] layout setup');
      this.SpectrumLayout = this.defaultSpectrumLayout();
      this.SpectrumData = this.defaultData();
      this.Options = [];
      console.log('[SpectrumComponent] plot layout set');
  }

  toSpectrumDataset(spectrum: Spectrum) {
    return [{
      x: spectrum.mz,
      y: spectrum.intensities,
      name: `Spectrum ${spectrum.id}, (X=${spectrum.x},Y=${spectrum.y})`
    }];
  }

  defaultSpectrumLayout() {
    return {
      height: 500,
      width: 1200
    };
  }

  defaultData() {
    return [{}];
  }
}
