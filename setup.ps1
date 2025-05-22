Write-Host "Restoring .NET dependencies..."
dotnet restore

Write-Host "Building the project..."
dotnet build

Write-Host "Installing Playwright browsers..."
bin/Debug/net8.0/playwright.ps1 install --with-deps

Write-Host "Playwright project is ready to run."
Write-Host "To run tests, use:"
Write-Host "dotnet test --no-restore --verbosity normal"
Write-Host "Tests will begin: "
dotnet test --no-restore --verbosity normal