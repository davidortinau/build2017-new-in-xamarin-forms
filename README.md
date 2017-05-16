# Microsoft Build 2017 - Weather Sample - Xamarin.Forms Embedding (PREVIEW)
This sample shows a shared Xamarin.Forms UI being embedded into non-Xamarin.Forms iOS, Android and UWP applications. With this method developers can use as much or as little Xamarin.Forms in their application as desired.

Xamarin.Forms services such as DependencyService and MessagingCenter work with the exception of Navigation. Binding also works when setting the context. 

**NOTE:** This is an early preview and not all features have been evaluated.

## Android
![](art/embedding-android.gif)

```
public void ShowHistory()
{
    if (_history == null)
    {
        // #1 Initialize Forms.Init(Context, Bundle)
        Forms.Init(this, null); 
        // #2 Use it with CreateFragment(Context)
        _history = new History().CreateFragment(this);
    }


    // And push that fragment onto the stack
    FragmentTransaction ft = FragmentManager.BeginTransaction();

    ft.AddToBackStack(null);
    ft.Replace(Resource.Id.fragment_frame_layout, _history, "history");
    
    ft.Commit();
}
```

## iOS
![](art/embedding-ios.gif)

Call `Forms.Init()` before creating the UIViewController.

```
public void ShowHistory()
{
    if (_history == null)
    {
        // #2 Use it
        _history = new History().CreateViewController();
    }

    // And push it onto the navigation stack
    _navigation.PushViewController(_history, true);
}
```

## UWP Desktop
![](art/embedding-uwp-desktop.gif)

In this demo we placed the History Page inside a flyOut Frame. Call `Forms.Init(e)` prior.

```
var x = new History().CreateFrameworkElement();
```