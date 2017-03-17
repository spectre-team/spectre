import { Spectre.Angular2ClientPage } from './app.po';

describe('spectre.angular2-client App', () => {
  let page: Spectre.Angular2ClientPage;

  beforeEach(() => {
    page = new Spectre.Angular2ClientPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
