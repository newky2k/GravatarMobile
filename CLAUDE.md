# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

`DSoft.Gravatar.Mobile` — a .NET library for Gravatar images and profiles (`Gravatar`, `GravatarControl`, `GravatarProfile`). It is being migrated in stages:

1. **Core (done):** `GravatarMobile/GravatarMobile.csproj` builds only the platform-neutral `*.shared.cs` files, targeting `netstandard2.0;net10.0;net11.0` plus `net10.0-windows;net11.0-windows` (and the android and ios targets on non-Linux hosts) (WPF `ImageAsBitmap` extensions in `GravatarExtensions.netf.netcore.cs`).
2. **Native views (ported, needs device verification):** `GravatarView.android.cs` / `.ios.cs` and their extensions build for `net10.0-android`, `net11.0-android`, `net10.0-ios` and `net11.0-ios` (not on Linux: they need the workloads). `.mac.cs` and `.uwp.cs` are in the tree but not compiled.
3. **MAUI view (written, needs build and device verification):** `GravatarMobile.Maui/` is the separate package `DSoft.Gravatar.Mobile.Maui`: a cross-platform `ContentView` (`GravatarMobile.Maui.GravatarView`, Image inside a Border) for `net10.0`/`net11.0` android, ios, maccatalyst and windows (Windows only on Windows hosts), referencing the core project. It cannot be built on Linux (maui workloads). Released together with the core, same version.

The old Xamarin and UWP samples under `samples/` are legacy and not in the solution.

## Build and test

```bash
dotnet build Gravatar.Mobile.sln -c Release
dotnet test GravatarMobile.Tests/GravatarMobile.Tests.csproj -c Release
```

- On Linux build the core and tests only (`dotnet build GravatarMobile/GravatarMobile.csproj`, `dotnet test GravatarMobile.Tests/...`); the solution includes the MAUI project, which needs the maui workloads.
- `GravatarMobile.Tests` (xunit, `net10.0;net11.0`) covers the hashing and the argument checks. There are no network tests.
- Android minimum API is 21 for net10 and 24 for net11, iOS minimum is 15.0. The Android `initial_style` attribute is read from the layout without the generated `Resource` class.
- `EnableWindowsTargeting` is on so the Windows targets compile on Linux. `WPFSample` compiles there but only runs on Windows.
- `MD5` is a thin wrapper over `System.Security.Cryptography.MD5` (Gravatar identifies accounts by MD5); it is not used for security.
- Assemblies are strong-name signed with `GravatarMobile/DSoft.snk`; do not remove it.
- Use `dotnet build`, not `dotnet pack`: with `GeneratePackageOnBuild=true`, a plain `dotnet pack` on a clean tree fails with NU5026 because it packs before building. `dotnet pack -p:GeneratePackageOnBuild=false` also works.
- `GeneratePackageOnBuild=true`, so every build produces `DSoft.Gravatar.Mobile.<version>.nupkg` (with `README.md` and `DSIcon.png`). Release builds add SourceLink.

## Branching

- `development` is the working branch: branch from it and open pull requests into it. Never commit or open a pull request directly against `main`.
- `main` is the release branch. Every push to `main` runs `release.yml`, which publishes the package to nuget.org and creates a GitHub release, so only merge `development` into `main` when a release is intended, and only when asked to release.

## CI

GitHub Actions (`.github/workflows/`) on `windows-latest`:

- `ci.yml` builds Release and runs the tests for pull requests into `main` and `development`; it publishes nothing.
- `release.yml` runs on every push to `main` (changes only to Markdown or workflow files are skipped; run it by hand to test a workflow change). It builds Release as `2.0.<yyMM>.<run number>` plus `RELEASE_SUFFIX` (empty for a stable version), runs the tests, pushes `DSoft.Gravatar.Mobile` to nuget.org with Trusted Publishing (OIDC, `NUGET_USER` secret, `nuget` environment), then tags the commit `v<version>` and creates a GitHub release with the package attached.
