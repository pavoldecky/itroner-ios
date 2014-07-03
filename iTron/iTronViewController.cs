using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace iTron
{
	public partial class iTronViewController : UIViewController
	{
		public iTronViewController () : base ("iTronViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Dummy Content
			int i;

			for(i = 1 ; i<5 ; i++)
			{
				UILabel label = new UILabel ();
				RectangleF rectangle = new RectangleF ();
				rectangle.X = (this.uiScrollView.Frame.Width * i) + 50;
				rectangle.Y = 0;
				rectangle.Size = this.uiScrollView.Frame.Size;
				label.Frame = rectangle;
				label.Text = i.ToString ();

				this.uiScrollView.AddSubview (label);
			}

			// set pages and content size
			this.uiPageControl.Pages = i;
			uiScrollView.ContentSize = new SizeF (uiScrollView.Frame.Width * i, uiScrollView.Frame.Height);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

