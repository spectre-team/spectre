import { browser, element, by } from 'protractor';

export class SpectreAngular2ClientPreparationListPage {
    navigateTo() {
        return browser.get('/');
    }

    getHeader() {
        return element(by.css('app-preparation-list h2')).getText();
    }

    getListText() {
        return element(by.css('app-preparation-list div.preparation-list'))
            .getText();
    }
}
