import { SpectreAngular2ClientPage } from './app.po';

describe('spectre.angular2-client App', () => {
  let page: SpectreAngular2ClientPage;

  beforeEach(() => {
    page = new SpectreAngular2ClientPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });

  it('should fail', () => expect(false).toEqual(true));
});
