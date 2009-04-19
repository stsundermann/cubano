// 
// Slider.cs
//  
// Author:
//       Aaron Bockover <abockover@novell.com>
// 
// Copyright 2009 Aaron Bockover
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using Cairo;

using Hyena.Gui;
using Hyena.Gui.Theming;

namespace Hyena.Gui.Canvas
{    
    public class Slider : CanvasItem
    {
        public Slider ()
        {
            Margin = new Thickness (3);
            MarginStyle = new ShadowMarginStyle {
                ShadowSize = 3,
                ShadowOpacity = 0.25
            };
        }
        
        private void SetPendingValueFromX (double x)
        {
            IsValueUpdatePending = true;
            PendingValue = x / Width;
        }
        
        /*protected override void OnButtonPress (double x, double y, uint button)
        {
            SetPendingValueFromX (x);
            base.OnButtonPress (x, y, button);
        }

        protected override void OnButtonRelease ()
        {
            Value = PendingValue;
            IsValueUpdatePending = false;
            base.OnButtonRelease ();
        }
        
        protected override void OnPointerMotion (double x, double y)
        {
            if (ButtonPressed >= 0) {
                SetPendingValueFromX (x);
            }
        
            base.OnPointerMotion (x, y);
        }*/
        
        protected override Rect InvalidationRect {
            get { return new Rect (
                -Margin.Left - ThrobberSize / 2, 
                -Margin.Top, 
                Allocation.Width + ThrobberSize, 
                Allocation.Height); 
            }
        }
        
        protected override void ClippedRender (Cairo.Context cr)
        {   
            double throbber_r = ThrobberSize / 2.0;
            double throbber_x = RenderSize.Width * (IsValueUpdatePending ? PendingValue : Value);
            double throbber_y = (Allocation.Height - ThrobberSize) / 2.0 - Margin.Top + throbber_r;
            double bar_w = RenderSize.Width * Value;
            
            Color color = Theme.Colors.GetWidgetColor (GtkColorClass.Dark, Gtk.StateType.Active);
            
            throbber_x = Math.Round (throbber_x);
            
            Color fill_color = CairoExtensions.ColorShade (color, 0.4);
            Color light_fill_color = CairoExtensions.ColorShade (color, 0.3);
            fill_color.A = 1.0;
            light_fill_color.A = 1.0;
            
            LinearGradient fill = new LinearGradient (0, 0, 0, RenderSize.Height);
            fill.AddColorStop (0, light_fill_color);
            fill.AddColorStop (0.5, fill_color);
            fill.AddColorStop (1, light_fill_color);
            
            cr.Rectangle (0, 0, bar_w, RenderSize.Height);
            cr.Pattern = fill;
            cr.Fill ();
            
            cr.Color = fill_color;
            cr.Arc (throbber_x, throbber_y, throbber_r, 0, Math.PI * 2);
            cr.Fill ();
        }
        
        public override Size Measure (Size available)
        {
            Height = BarSize;
            return DesiredSize = new Size (base.Measure (available).Width, 
                Height + Margin.Top + Margin.Bottom);
        }
        
        private double bar_size = 3;
        public virtual double BarSize {
            get { return bar_size; }
            set { bar_size = value; }
        }
        
        private double throbber_size = 7;
        public virtual double ThrobberSize {
            get { return throbber_size; }
            set { throbber_size = value; }
        }
        
        private double value;
        public virtual double Value {
            get { return this.value; }
            set {
                if (value < 0.0 || value > 1.0) {
                    throw new ArgumentOutOfRangeException ("Value", "Must be between 0.0 and 1.0 inclusive");
                } else if (this.value == value) {
                    return;
                }
                
                this.value = value;
                InvalidateRender ();
            }
        }
        
        private bool is_value_update_pending;
        public virtual bool IsValueUpdatePending {
            get { return is_value_update_pending; }
            set { is_value_update_pending = value; }
        }
        
        private double pending_value;
        public virtual double PendingValue {
            get { return pending_value; }
            set {
                if (value < 0.0 || value > 1.0) {
                    throw new ArgumentOutOfRangeException ("Value", "Must be between 0.0 and 1.0 inclusive");
                } else if (pending_value == value) {
                    return;
                }
                
                pending_value = value;
                InvalidateRender ();
            }
        }
    }
}
