using System;
using System.Diagnostics;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Controls
{
    /// <summary>
    /// Circulear progress bar view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircularProgressBar : ContentView
    {
        /// <summary>
        /// 
        /// </summary>
        private const int PADDING = 2;

        /// <summary>
        /// Time elapsed from previous view draw.
        /// </summary>
        private readonly Stopwatch _time = new Stopwatch();

        /// <summary>
        /// Interval between view draws.
        /// </summary>
        private readonly TimeSpan _drawInterval = TimeSpan.FromMilliseconds(30);

        /// <summary>
        /// View draw frame.
        /// </summary>
        private SKRect frame;

        /// <summary>
        /// Stroke paint configuration.
        /// </summary>
        private SKPaint paint;

        /// <summary>
        /// Background paint configuration.
        /// </summary>
        private SKPaint paintBackground;

        /// <summary>
        /// 
        /// </summary>
        private float _easing = 0;

        /// <summary>
        /// 
        /// </summary>
        private float _rotate = 0;

        /// <summary>
        /// 
        /// </summary>
        private float _currentProgress;

        /// <summary>
        /// 
        /// </summary>
        private float _value;

        /// <summary>
        /// Bindable property of <see cref="Progress"/>.
        /// </summary>
        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(
            nameof(Progress),
            typeof(double),
            typeof(CircularProgressBar),
            0d,
            propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                ((CircularProgressBar)bindable)._easing = 0;
                ((CircularProgressBar)bindable).canvas.InvalidateSurface();
            });

        /// <summary>
        /// Bindable property of <see cref="Color"/>.
        /// </summary>
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color),
            typeof(Color),
            typeof(CircularProgressBar),
            Color.Accent,
            propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                ((CircularProgressBar)bindable).UpdatePaint();
                ((CircularProgressBar)bindable).canvas.InvalidateSurface();
            });


        /// <summary>
        /// Bindable property of <see cref="Stroke"/>.
        /// </summary>
        public static readonly BindableProperty StrokeProperty = BindableProperty.Create(
            nameof(Stroke),
            typeof(double),
            typeof(CircularProgressBar),
            9d,
            propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                ((CircularProgressBar)bindable).UpdatePaint();
                ((CircularProgressBar)bindable).canvas.InvalidateSurface();
            });

        /// <summary>
        /// Bindable property of <see cref="Spin"/>.
        /// </summary>
        public static readonly BindableProperty SpinProperty = BindableProperty.Create(
            nameof(Spin),
            typeof(bool),
            typeof(CircularProgressBar),
            false,
            propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                ((CircularProgressBar)bindable).canvas.InvalidateSurface();
            });

        /// <summary>
        /// Bindable property of <see cref="Easing"/>.
        /// </summary>
        public static readonly BindableProperty EasingProperty = BindableProperty.Create(
            nameof(Easing),
            typeof(bool),
            typeof(CircularProgressBar),
            false,
            propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                ((CircularProgressBar)bindable).canvas.InvalidateSurface();
            });

        /// <summary>
        /// Progress bar value (in percentages).
        /// </summary>
        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set
            {
                _currentProgress = _value;
                SetValue(ProgressProperty, Math.Max(0, Math.Min(100, value)));
            }
        }

        /// <summary>
        /// Progress bar stroke color.
        /// </summary>
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        /// <summary>
        /// Progress bar stroke thickness.
        /// </summary>
        public double Stroke
        {
            get => (double)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        /// <summary>
        /// Should spin progress bar.
        /// </summary>
        public bool Spin
        {
            get => (bool)GetValue(SpinProperty);
            set => SetValue(SpinProperty, value);
        }

        /// <summary>
        /// Should change progress bar <see cref="Value"/> with easing.
        /// </summary>
        public bool Easing
        {
            get => (bool)GetValue(EasingProperty);
            set => SetValue(EasingProperty, value);
        }

        /// <summary>
        /// Create new instance of <see cref="CircularProgressBar"/>.
        /// </summary>
        public CircularProgressBar()
        {
            UpdatePaint();
            InitializeComponent();
        }

        /// <inheritdoc/>
        protected override void InvalidateLayout()
        {
            base.InvalidateLayout();
            canvas.InvalidateSurface();
        }

        /// <summary>
        /// Update <see cref="paint"/> and <see cref="paintBackground"/>.
        /// </summary>
        private void UpdatePaint()
        {
            paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.ToSKColor(),
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round,
                StrokeWidth = (float)ConvertToPixels(Stroke),
                IsAntialias = true,
            };

            paintBackground = paint.Clone();
            paintBackground.StrokeWidth *= 0.9f;

            paintBackground.Color = new SKColor(
                paintBackground.Color.Red,
                paintBackground.Color.Green,
                paintBackground.Color.Blue,
                50);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CanvasPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            _time.Stop();

            // Elapsed milliseconds from previous view draw
            float elapsed = (float)Math.Max(0.1, Math.Min(10, _time.ElapsedMilliseconds / _drawInterval.TotalMilliseconds));

            _time.Reset();
            _time.Start();

            if (Easing || Spin)
            {
                _easing += 0.05f * elapsed;
                _easing = Math.Min(1, _easing);
            }
            else
            {
                _easing = 1;
            }

            if (Spin)
            {
                _rotate += 8f * elapsed;

                if (_rotate > 360)
                    _rotate = _rotate - 360;
            }
            else
            {
                _rotate = 0;
            }

            if (Progress != _value
                || (Spin && Progress > 0 && Progress < 100)
                || (Easing && _easing > 0 && _easing < 1))
            {
                Device.StartTimer(_drawInterval, () =>
                {
                    canvas.InvalidateSurface();
                    return false;
                });
            }

            args.Surface.Canvas.Clear();

            frame.Size = new SKSize(
                args.Info.Width - paint.StrokeWidth - PADDING,
                args.Info.Height - paint.StrokeWidth - PADDING);

            frame.Location = new SKPoint(
                paint.StrokeWidth / 2,
                paint.StrokeWidth / 2);

            float delta = ((float)Progress - _currentProgress) * (float)Xamarin.Forms.Easing.CubicInOut.Ease(_easing);

            _value = _currentProgress + delta;

            float startAngle = _rotate + 270f;
            float sweepAngle = _value / 100f * 360f;

            SKPath path = new SKPath();
            path.AddArc(frame, startAngle, sweepAngle);

            SKPath pathBackground = new SKPath();
            pathBackground.AddArc(frame, 0, 360);

            args.Surface.Canvas.DrawPath(path, paint);
            args.Surface.Canvas.DrawPath(pathBackground, paintBackground);
        }


        /// <summary>
        /// Converts Xamarin units into pixels.
        /// https://stackoverflow.com/a/63615455/6499748
        /// </summary>
        /// <param name="value">Xamarin units.</param>
        /// <returns>Value in pixel</returns>
        private static double ConvertToPixels(double value)
            => (DeviceDisplay.MainDisplayInfo.Density / 2d) * value;
    }
}