# Xamarin.Forms.CircularProgressBar

<a href="https://www.nuget.org/packages/Xamarin.Forms.CircularProgressBar/"><img alt="Nuget" src="https://img.shields.io/nuget/v/Xamarin.Forms.CircularProgressBar"></a> <a href="https://www.nuget.org/packages/Xamarin.Forms.CircularProgressBar/"><img alt="Nuget" src="https://img.shields.io/nuget/dt/Xamarin.Forms.CircularProgressBar"></a>

Easy to use circular progress bar for your Xamarin.Forms application (works on Android and iOS).

https://www.nuget.org/packages/Xamarin.Forms.CircularProgressBar/

<img src="https://github.com/VictorKochetkov/Xamarin.Forms.CircularProgressBar/blob/master/github/showcase.gif?raw=true" alt="showcase">

# Quick start
Static progress bar without easing

```xaml
<controls:CircularProgressBar WidthRequest="40" HeightRequest="40"/>
```

Static progress bar with easing

```xaml
<controls:CircularProgressBar WidthRequest="40" HeightRequest="40" Easing="True"/>
```

Spinning progress bar

```xaml
<controls:CircularProgressBar WidthRequest="40" HeightRequest="40" Spin="True"/>
```

# Features
`double Progress` - value of progress (in percents)

`bool Spin` - should bar infinitly spinning

`bool Easing` - should progress be changed with ease or instantly (ignored if `Spin` set to `true`)

`double Stroke` - stroke of progress

# Donate

If you like this project you can support it by making a donation 🤗 Thank you!

<a href="https://www.buymeacoffee.com/bananadev" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee"></a>
