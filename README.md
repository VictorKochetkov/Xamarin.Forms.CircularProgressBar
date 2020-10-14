Circular progress bar for Xamarin.Forms (Android and iOS)

<img src="https://github.com/VictorKochetkov/Xamarin.Forms.CircularProgressBar/blob/master/github/showcase.gif?raw=true" alt="showcase">

# Quick start
Static progress bar without easing

`<controls:CircularProgressBar WidthRequest="40" HeightRequest="40"/>`

Static progress bar with easing

`<controls:CircularProgressBar WidthRequest="40" HeightRequest="40" Easing="True"/>`

Spinning progress bar

`<controls:CircularProgressBar WidthRequest="40" HeightRequest="40" Spin="True"/> `

# Properties you would like to use:
`double Progress` - value of progress (in percents)

`bool Spin` - should bar infinitly spinning

`bool Easing` - should progress be changed with ease or instantly (ignored if `Spin` set to `true`)

`double Stroke` - stroke of progress
