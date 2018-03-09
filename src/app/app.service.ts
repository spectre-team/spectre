/*
 * app.service.ts
 * General service.
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

import { environment } from '../environments/environment';

export class Service {
  private basePreparationUrl = environment.apiPreparationUrl;
  private baseUploadUrl = environment.apiUploadUrl;
  private baseDivikUrl = environment.apiDivikUrl;
  private baseAnalysisUrl = environment.apiAnalysisUrl;

  public getBasePreparationUrl(): string { return this.basePreparationUrl; }
  public getBaseUploadUrl(): string { return this.baseUploadUrl; }
  public getBaseDivikUrl(): string { return this.baseDivikUrl; }
  public getBaseAnalysisUrl(): string { return this.baseAnalysisUrl; }


  constructor() { }
}
