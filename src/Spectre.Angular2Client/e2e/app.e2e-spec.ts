import { SpectreAngular2ClientPage } from './app.po';

describe('spectre.angular2-client App', () => {
  let page: SpectreAngular2ClientPage;

  beforeEach(() => {
    page = new SpectreAngular2ClientPage();
  });

  it('should have "Spectre Client" title', () => {
      page.navigateTo();
      expect(page.getTitle()).toEqual('Spectre Client');
  });

  it('should display name of the project', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Spectre');
  });
});
