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
