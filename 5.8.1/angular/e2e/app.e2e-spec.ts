import { RChatTemplatePage } from './app.po';

describe('RChat App', function() {
  let page: RChatTemplatePage;

  beforeEach(() => {
    page = new RChatTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
