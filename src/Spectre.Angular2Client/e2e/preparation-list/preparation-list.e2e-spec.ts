import {SpectreAngular2ClientPreparationListPage} from './preparation-list.po';

describe('spectre.angular2-client preparation-list', () => {
    let page: SpectreAngular2ClientPreparationListPage;

    beforeEach(() => {
        page = new SpectreAngular2ClientPreparationListPage();
    });

    it('should have header which indicates, what is it', () => {
        page.navigateTo();
        expect(page.getHeader()).toContain('preparations');
    });

    it('should contain sample preparation', () => {
        page.navigateTo();
        expect(page.getListText()).toContain(
            'Head & neck cancer, patient 1, tumor region only');
    });
});
