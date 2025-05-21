using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;
using PlaywrightDemo.Pages;


namespace PlaywrightDemo.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

public class LoginPageTest : PageTest
{
    [Test]
    public async Task Login_ToSite_WithValidCredential()
    {
        var loginPage = new LoginPage(Page);
        await loginPage.NavigateAsync(TestsConstants.BaseUrl);
        await loginPage.LoginAsync(TestsConstants.ValidUsername, TestsConstants.ValidPassword);

        //Expect the URL contain "dashboard" 
        await Expect(Page).ToHaveURLAsync(new Regex("dashboard"));
    }

    [Test]
    public async Task Logout_FromDashBoard()
    {

        var loginPage = new LoginPage(Page);
        await loginPage.NavigateAsync(TestsConstants.BaseUrl);
        await loginPage.LoginAsync(TestsConstants.ValidUsername, TestsConstants.ValidPassword);
        await loginPage.LogoutAsync();
        //Expect the URL contain "login", means returns to login page
        await Expect(Page).ToHaveURLAsync(new Regex("login"));
    }

    [Test]
    public async Task Login_WihtBlank_Credentials()
    {
        var loginPage = new LoginPage(Page);
        await loginPage.NavigateAsync(TestsConstants.BaseUrl);
        await loginPage.LoginAsync("", "");

        var message = await loginPage.GetAlertMessageRequiredAssync();
        //Pops a "Required" message
        Assert.That(message, Does.Contain("Required"));
    }

    [TestCase("ad", "123")]
    [TestCase("Admin", "wrong")]
    [TestCase("Jane", "admin123")]
    public async Task Login_WithInvalidCredentials_ShouldFail(string username, string password)
    {
        var loginPage = new LoginPage(Page);
        await loginPage.NavigateAsync(TestsConstants.BaseUrl);
        await loginPage.LoginAsync(username, password);

        var message = await loginPage.GetAlertMessageWrongCredentialsAssync();

        Assert.That(message, Does.Contain("Invalid credentials"));
    }
}