/*
 * app.po.ts
 * Helpers for e2e testing of main component.
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

import { browser, element, by } from 'protractor';

export class SpectreAngular2ClientPage {
  navigateTo() {
    return browser.get('/');
  }

  getTitle() {
      return browser.getTitle();
  }

  getParagraphText() {
    return element(by.css('app-root h1')).getText();
  }
}
