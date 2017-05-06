import { SpectreAngular2ClientPage } from './app.po';

describe('spectre.angular2-client App', () => {
  let page: SpectreAngular2ClientPage;

  beforeEach(() => {
    page = new SpectreAngular2ClientPage();
  });

  it('should display name of the project', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Spectre');
  });
});
