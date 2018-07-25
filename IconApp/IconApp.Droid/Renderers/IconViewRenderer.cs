using System;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using System.ComponentModel;
using IconApp;
using Xamarin.Forms;
using IconApp.Droid.Renderers;
using Android.Graphics;
using Android.Graphics.Drawables;

[assembly: ExportRendererAttribute(typeof(IconView), typeof(IconViewRenderer))]

namespace IconApp.Droid.Renderers
{
    public class IconViewRenderer : ViewRenderer<IconView, ImageView>
    {
        private bool _isDisposed;

        public IconViewRenderer()
        {
            AutoPackage = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<IconView> e)
        {
            if (e.OldElement != null) 
            {
                // Clear old element event
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new ImageView(Context));
                }
                
                UpdateBitmap(e.NewElement);
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == IconView.SourceProperty.PropertyName)
            {
                UpdateBitmap((IconView)sender);
            }
            else if (e.PropertyName == IconView.ForegroundProperty.PropertyName)
            {
                UpdateBitmap((IconView)sender);
            }
        }

        private void UpdateBitmap(IconView element = null)
        {
            if (_isDisposed || string.IsNullOrWhiteSpace(element.Source)) return;
            
            var d = Resources.GetDrawable(element.Source).Mutate();
            d.SetColorFilter(new LightingColorFilter(element.Foreground.ToAndroid(), element.Foreground.ToAndroid()));
            d.Alpha = element.Foreground.ToAndroid().A;
            Control.SetImageDrawable(d);
            ((IVisualElementController)element).NativeSizeChanged();
        }
    }
}

