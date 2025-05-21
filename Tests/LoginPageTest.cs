using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;
using PlaywrightDemo.Pages;


namespace PlaywrightDemo.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

public class LoginPageTest : PageTest
{
    private LoginPage _loginPage;

    [SetUp]
    public async Task Init()
    {
        _loginPage = new LoginPage(Page);
        await _loginPage.NavigateAsync(TestsConstants.BaseUrl);
    }


    [Test]
    public async Task Login_ToSite_WithValidCredential()
    {
        await _loginPage.LoginAsync(TestsConstants.ValidUsername, TestsConstants.ValidPassword);

        //Expect the URL contain "dashboard" 
        await Expect(Page).ToHaveURLAsync(new Regex("dashboard"));
    }

    [Test]
    public async Task Logout_FromDashBoard()
    {
        await _loginPage.LoginAsync(TestsConstants.ValidUsername, TestsConstants.ValidPassword);
        await _loginPage.LogoutAsync();
        //Expect the URL contain "login", means returns to login page
        await Expect(Page).ToHaveURLAsync(new Regex("login"));
    }

    [Test]
    public async Task Login_WihtBlankCredentials()
    {
        await _loginPage.LoginAsync("", "");

        var message = await _loginPage.GetRequiredFieldErrorAssync();
        //Pops a "Required" message
        Assert.That(message, Does.Contain("Required"));
    }

    [TestCase("ad", "123")]
    [TestCase("Admin", "wrong")]
    [TestCase("Jane", "admin123")]
    public async Task Login_WithInvalidCredentials_ShouldFail(string username, string password)
    {
        await _loginPage.LoginAsync(username, password);

        var message = await _loginPage.GetInvalidCredentialsErrorAssync();

        Assert.That(message, Does.Contain("Invalid credentials"));
    }
}