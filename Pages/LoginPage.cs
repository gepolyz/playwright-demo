
using Microsoft.Playwright;

namespace PlaywrightDemo.Pages;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateAsync(string url)
    {
        await _page.GotoAsync(url);
    }

    public async Task LoginAsync(string username, string password)
    {
        await _page.FillAsync("input[name='username']", username);
        await _page.FillAsync("input[name='password']", password);
        await _page.ClickAsync("button[type='submit']");
    }

    public async Task LogoutAsync()
    {
        await _page.ClickAsync(".oxd-topbar-header-userarea");
        await _page.ClickAsync("a.oxd-userdropdown-link[href*='logout']");
    }

    public async Task<string> GetAlertMessageRequiredAssync()
    {
        return await (await _page.WaitForSelectorAsync(".oxd-input-field-error-message")).InnerTextAsync();
    }

    public async Task<string> GetAlertMessageWrongCredentialsAssync()
    {
        return await (await _page.WaitForSelectorAsync(".oxd-alert-content-text")).InnerTextAsync();
    }

}