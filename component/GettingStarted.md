Gravatar Mobile API is a cross platfrom framework for integration of Gravatar in to your mobile application.

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
	

## GravatarView

On iOS, Android and windows phone we provide a `GravatarView` which implements `IGravatarView` that can display a Avatar for you.  You can simply pass a gravatar to it to load the Image, by passing it to the `Avatar` property


	using GravatarMobile.Core;
	...
	
	var aVatar = new Gravatar("someone@somewhere.com");
	
	//refernece the instance of GravatarView
	gravView.Avatar = aVatar;
	

When passed in this way the image loaded from Gravatar will be set to the size of the `GravatarView` instances dimensions.

You can set the style of the GravatarView to be either `Round` or `Square` using the `ViewStyle`property and you can also set the size of the border to be drawn around the image using the `BorderWidth` property

**iOS**

'GravatarView' on iOS will work when created programatically, from an xib and within a storyboard.

You can create one programatically using the standard `Frame` based contructor after which it can be added to you view controller

	using GravatarMobile.Core;
	using GravatarMobile.iOS;
	...
	
	public override void ViewDidLoad()
	{
		base.ViewDidLoad();
		
		var gravView = new GravatarView(RectangleF.Empty);
		
		//refernece the instance of GravatarView
		gravView.Avatar = new Gravatar("someone@somewhere.com");
		
		this.View.Add(gravView);
		
	}	

In the iOS Storyboard designer you can access both the `ViewStyle` and `BorderWidth` properties.

**Android**

In Android you can include the `GravatarView` in your xml layout

    <gravatarmobile.droid.GravatarView xmlns:gravview="http://schemas.android.com/apk/res-auto"
        android:src="@android:drawable/ic_menu_gallery"
        android:layout_width="192dp"
        android:layout_height="192dp"
        android:id="@+id/imgGravatar"
        gravview:initial_style="round" />	
        

You can also create an instance programatically and add it as a sub-view.

The `initial_style` attribute can be set to `round` or `square` to set the load style

**Windows Phone**

On windows phone you can add the `GravatarView` into your xaml definition

	xmlns:WP="clr-namespace:GravatarMobile.WP;assembly=GravatarMobile.WP" 
	...
	
	   <WP:GravatarView 
    	   x:Name="imgGravatar" 
       	   Margin="0,10,0,0" 
           Width="320" 
           Height="320"
           IntitialViewStyle="Round"/>

