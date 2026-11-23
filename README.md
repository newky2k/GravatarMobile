# DSoft.Gravatar.Mobile

A small .NET library for [Gravatar](https://gravatar.com): load the avatar image and the public profile for an email address.

[![NuGet](https://img.shields.io/nuget/v/DSoft.Gravatar.Mobile.svg)](https://www.nuget.org/packages/DSoft.Gravatar.Mobile)

## Features

* Gravatar images at any size, as bytes, a stream or (on Windows) a WPF `BitmapImage`
* Gravatar public profiles (name, about me, location, links, accounts, photos and more)
* No dependencies
* Targets .NET Standard 2.0, .NET 10 and .NET 11, plus `net10.0-windows` and `net11.0-windows` with WPF helpers

## Status

This is the platform-neutral core, rewritten for modern .NET. The native views are being ported and will follow as separate releases:

* iOS and Android `GravatarView` controls on .NET 10 and .NET 11
* A .NET MAUI view built from the native views

The old Xamarin, UWP and Mac Catalyst code is still in the repository but is not built or packaged yet.

## Install

```
dotnet add package DSoft.Gravatar.Mobile
```

## Usage

```csharp
using GravatarMobile;

var gravatar = new Gravatar("someone@example.com");

// The constructor starts loading the 80px image and the profile in the background.
// To load explicitly, and wait for the result:
await gravatar.LoadImageAsync(256);      // size in pixels
await gravatar.LoadProfileAsync();       // Profile is null if there is no public profile

byte[] image = gravatar.Image;           // throws if the image has not been loaded
var name = gravatar.Profile?.DisplayName;
```

On Windows (`net10.0-windows` and `net11.0-windows`) the `ImageAsBitmap()` and `ImageAsBitmapAsync()` extensions give a WPF `BitmapImage`:

```csharp
imgGravatar.Source = gravatar.ImageAsBitmap();
```

`gravatar.Hash` is the lower case MD5 hash of the trimmed, lower cased email address that Gravatar uses to identify an account.

## Build

```bash
dotnet build Gravatar.Mobile.sln -c Release
dotnet test GravatarMobile.Tests/GravatarMobile.Tests.csproj -c Release
```

Build with the .NET 10 and .NET 11 SDKs. `WPFSample` is a small WPF app that loads an avatar and profile for an email address; it runs on Windows only.

## License

MIT
