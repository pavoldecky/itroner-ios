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
	[Register ("MainView")]
	partial class MainView
	{
		[Outlet]
		MonoTouch.UIKit.UIButton settingsBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UINavigationBar uiNavigationBar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIPageControl uiPageControl { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView uiScrollView { get; set; }

		[Action ("settingsButton:")]
		partial void settingsButton (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (uiNavigationBar != null) {
				uiNavigationBar.Dispose ();
				uiNavigationBar = null;
			}

			if (uiPageControl != null) {
				uiPageControl.Dispose ();
				uiPageControl = null;
			}

			if (uiScrollView != null) {
				uiScrollView.Dispose ();
				uiScrollView = null;
			}

			if (settingsBtn != null) {
				settingsBtn.Dispose ();
				settingsBtn = null;
			}
		}
	}
}
