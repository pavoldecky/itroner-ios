// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace iTron
{
	[Register ("iTronViewController")]
	partial class iTronViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIPageControl uiPageControl { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView uiScrollView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (uiScrollView != null) {
				uiScrollView.Dispose ();
				uiScrollView = null;
			}

			if (uiPageControl != null) {
				uiPageControl.Dispose ();
				uiPageControl = null;
			}
		}
	}
}
