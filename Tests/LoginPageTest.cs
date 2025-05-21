using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;


namespace PlaywrightDemo.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

public class LoginPageTest : PageTest
{
    [Test]
    public async Task Login_ToSite_WithValidCredential()
    {
        await Page.GotoAsync(TestsConstants.BaseUrl);

        await Page.FillAsync("input[name='username']", TestsConstants.ValidUsername);
        await Page.FillAsync("input[name='password']", TestsConstants.ValidPassword);

        await Page.ClickAsync("button[type='submit']");
        //Expect the URL contain "dashboard" 
        await Expect(Page).ToHaveURLAsync(new Regex("dashboard"));
    }

    [Test]
    public async Task Logout_FromDashBoard()
    {
        await Page.GotoAsync(TestsConstants.BaseUrl);
        await Page.FillAsync("input[name='username']", TestsConstants.ValidUsername);
        await Page.FillAsync("input[name='password']", TestsConstants.ValidPassword);
        await Page.ClickAsync("button[type='submit']");
        await Page.ClickAsync(".oxd-topbar-header-userarea");
        await Page.ClickAsync("a.oxd-userdropdown-link[href*='logout']");
        //Expect the URL contain "login", means returns to login page
        await Expect(Page).ToHaveURLAsync(new Regex("login"));
    }

    [Test]
    public async Task Login_WihtBlank_Credentials()
    {
        await Page.GotoAsync(TestsConstants.BaseUrl);
        await Page.FillAsync("input[name='username']", "");
        await Page.FillAsync("input[name='password']", "");
        await Page.ClickAsync("button[type='submit']");

        var alert = await Page.WaitForSelectorAsync(".oxd-input-field-error-message");
        var message = await alert.InnerTextAsync();
        //Pops a "Required" message
        Assert.That(message, Does.Contain("Required"));
    }

    [TestCase("ad", "123")]
    [TestCase("Admin", "wrong")]
    [TestCase("Jane", "admin123")]
    public async Task Login_WithInvalidCredentials_ShouldFail(string username, string password)
    {
        await Page.GotoAsync(TestsConstants.BaseUrl);
        await Page.FillAsync("input[name='username']", username);
        await Page.FillAsync("input[name='password']", password);
        await Page.ClickAsync("button[type='submit']");

        var alert = await Page.WaitForSelectorAsync(".oxd-alert-content-text");
        var message = await alert.InnerTextAsync();

        Assert.That(message, Does.Contain("Invalid credentials"));
    }
}