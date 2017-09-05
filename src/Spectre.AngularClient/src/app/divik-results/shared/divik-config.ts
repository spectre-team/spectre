/*
 * divik-config.ts
 * Interface describing Divik result config.
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
export interface DivikConfig {
  'Max K': number;
  Level: number;
  'Using levels': boolean;
  Amplitude: boolean;
  Variance: boolean;
  'Percent size limit': number;
  'Feature preservation limit': number;
  Metric: string;
  'Plotting partitions': boolean;
  'Plotting recursively': boolean;
  'Plotting decomposition': boolean;
  'Plotting decomposition recursively': boolean;
  'Max decomposition components': number;
}
