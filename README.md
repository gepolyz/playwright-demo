# Playwright UI Test Suite

This repository contains UI tests using **Microsoft Playwright**, **NUnit**, and **C#** targeting the OrangeHRM demo site.

## Technologies Used
- .NET 8
- Playwright for .NET
- NUnit
- GitHub Actions (CI/CD)

## Prerequisites
- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- Git
- PowerShell (for Playwright install script)

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/playwright-ui-tests.git
cd playwright-ui-tests
dotnet restore
pwsh bin/Debug/net8.0/playwright.ps1 install --with-deps
dotnet test --no-restore --verbosity normal
```

## CI with GitHub Actions
A GitHub Actions pipeline is included in .github/workflows/playwright.yml, which:

- Builds the project

- Installs Playwright dependencies

- Runs the tests

- Uploads .trx results and optional traces

## Notes
- Playwright auto-waits for elements, making your tests more reliable.

- Tracing and screenshots can be enabled in the test lifecycle.