Gravatar Mobile API is a cross platfrom framework for integration of Gravatar in to your mobile application.

# Features

* Cross platform  
  * Works on iOS, Android, Windows Phone, Mac and Windows
* Support for Gravatar images and profiles
* Small footprint
* Simple API
* PCL core library
* Helper libraries for iOS, Android and Windows phone with implementations of "GravatarView"
* Extensions for converting to native image types

## Usage

A `Gravatar` object can be created by passing an email address to the constructor.


	using GravatarMobile.Core;
	...
	
	var aVatar = new Gravatar("someone@somewhere.com");
	
Once created you can call the methods to load the image and the profile in to the `Gravatar` using async calls

	using GravatarMobile.Core;
	...
	
	var aVatar = new Gravatar("someone@somewhere.com");
	
	//load the default size image of 80px
	await aVatar.LoadImageAsync();
	
	//load avatar of a specific size
	await aVatar.LoadImageAsync(512);
	
	//load the profile
	await aVatar.LoadProfileAsync();
	

On iOS, Android and windows phone we provide a `GravatarView` that can display a Avatar for you.  You can simply pass a gravatar to it to load the Image, by passing it to the `Avatar` property


	using GravatarMobile.Core;
	...
	
	var aVatar = new Gravatar("someone@somewhere.com");
	
	//refernece the instance of GravatarView
	gravView.Avatar = aVatar;
	