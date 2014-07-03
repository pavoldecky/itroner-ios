using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.ObjCRuntime;

namespace iTron
{
	public partial class MainView : UIViewController
	{
		public MainView () : base ("MainView", null)
		{
			settings = new Settings ();
			smsTask = new SmsTask ();
		}

		private Settings settings;
		private SmsTask smsTask;

		private UIView firstView;
		private UIView secondView;
		private UIView thirdView;

		private float frameWidth = 0;
		private float frameHeight = 0;

		private SettingsController settingsScreen;

		private RectangleF viewFrame;

		private UIAlertView passwordAlert;

		public string PhoneNumber {
			get {
				return settings.GetValueOrDefault<string> (Constant.PhoneNumber, "+");
			}
		}

		public string Password {
			get {
				return settings.GetValueOrDefault<string> (Constant.Password, "1234");
			}
		}

		public bool UsePassword {
			get {
				return settings.GetValueOrDefault<bool> (Constant.UsePassword, false);
			}
		}

		public string MaxSpeed {
			get {
				return settings.GetValueOrDefault<string> (Constant.MaxSpeed, "120");
			}
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

			//return ([[UIDevice currentDevice] userInterfaceIdiom] == UIUserInterfaceIdiomPhone && [UIScreen mainScreen].bounds.size.height == 568.0);

		public override void ViewDidLoad ()
		{
			Title = "iTron";
			AutomaticallyAdjustsScrollViewInsets = true;
			NavigationController.NavigationBar.Translucent = false;
			NavigationController.NavigationBar.TintColor = UIColor.Black;

			// ios7 layout
			if (RespondsToSelector (new Selector ("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.All;
			NavigationController.Title = "iTron";

			UIBarButtonItem baritem = new UIBarButtonItem (UIBarButtonSystemItem.Edit, delegate {
				if(this.settingsScreen == null) { this.settingsScreen = new SettingsController(); }
				//---- push our hello world screen onto the navigation controller and pass a true so it navigates
				this.NavigationController.PushViewController(this.settingsScreen, true);
			});

			NavigationItem.RightBarButtonItems = new UIBarButtonItem[]{ baritem };



			if (Session.FirstLaunch && UsePassword) {
				passwordAlert = new UIAlertView ("Prihlasenie", "Zadajte heslo", null, "Ok");
				passwordAlert.AlertViewStyle = UIAlertViewStyle.SecureTextInput;
				passwordAlert.Show ();
				passwordAlert.Clicked += HandleClicked;
			}

			base.ViewDidLoad ();
			//    // ios7 layout
						//if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
			//	EdgesForExtendedLayout = UIRectEdge.None;
			this.uiScrollView.DecelerationEnded += this.scrollView_DecelerationEnded;
			this.uiPageControl.ValueChanged += this.pageControl_ValueChanged;

			this.uiScrollView.Scrolled += delegate
			{
				Console.WriteLine("Scrolled!");
			};


			this.uiScrollView.PagingEnabled = true;

			RectangleF scrollPageFrame = this.uiScrollView.Frame;
			this.uiScrollView.ContentSize = new SizeF(scrollPageFrame.Width * 3, 300);

			var pageControlFrame = uiPageControl.Frame;

			firstView = new UIView(scrollPageFrame);
			firstView.BackgroundColor = UIColor.Red;
			scrollPageFrame.X += this.uiScrollView.Frame.Width;

			viewFrame = firstView.Frame;
			frameHeight = viewFrame.Height;
			frameWidth = viewFrame.Width;

			float padding = 10;

			float buttonWidth = (frameWidth - (3 * padding));
			buttonWidth = buttonWidth / 2;
			float buttonHeight = (frameHeight - (3 * padding));
			buttonHeight = buttonHeight / 2;

			var button1Rect = new RectangleF (padding, padding, buttonWidth, buttonHeight);
			var button2Rect = new RectangleF (button1Rect.Right + padding, padding, buttonWidth, buttonHeight);
			var button3Rect = new RectangleF (padding, button1Rect.Bottom + padding, buttonWidth, buttonHeight);
			var button4Rect = new RectangleF (button1Rect.Right + padding, button1Rect.Bottom + padding, buttonWidth, buttonHeight);

			UIButton button1 = new UIButton (button1Rect);
			UIButton button2 = new UIButton (button2Rect);
			UIButton button3 = new UIButton (button3Rect);
			UIButton button4 = new UIButton (button4Rect);

			UIButton button5 = new UIButton (button1Rect);
			UIButton button6 = new UIButton (button2Rect);
			UIButton button7 = new UIButton (button3Rect);
			UIButton button8 = new UIButton (button4Rect);

			UIButton button9 = new UIButton (button1Rect);
			UIButton button10 = new UIButton (button2Rect);
			UIButton button11 = new UIButton (button3Rect);
			UIButton button12 = new UIButton (button4Rect);
		
			button1.BackgroundColor = UIColor.White;
			button2.BackgroundColor = UIColor.White;
			button3.BackgroundColor = UIColor.White;
			button4.BackgroundColor = UIColor.White;
			button5.BackgroundColor = UIColor.White;
			button6.BackgroundColor = UIColor.White;
			button7.BackgroundColor = UIColor.White;
			button8.BackgroundColor = UIColor.White;
			button9.BackgroundColor = UIColor.White;
			button10.BackgroundColor = UIColor.White;
			button11.BackgroundColor = UIColor.White;
			button12.BackgroundColor = UIColor.White;

			button1.Tag = 1;
			button2.Tag = 2;
			button3.Tag = 3;
			button4.Tag = 4;
			button5.Tag = 5;
			button6.Tag = 6;
			button7.Tag = 7;
			button8.Tag = 8;
			button9.Tag = 9;
			button10.Tag = 10;
			button11.Tag = 11;
			button12.Tag = 12;

			button1.TouchUpInside += HandleTouchUpInside;
			button2.TouchUpInside += HandleTouchUpInside;
			button3.TouchUpInside += HandleTouchUpInside;
			button4.TouchUpInside += HandleTouchUpInside;
			button5.TouchUpInside += HandleTouchUpInside;
			button6.TouchUpInside += HandleTouchUpInside;
			button7.TouchUpInside += HandleTouchUpInside;
			button8.TouchUpInside += HandleTouchUpInside;
			button9.TouchUpInside += HandleTouchUpInside;
			button10.TouchUpInside += HandleTouchUpInside;
			button11.TouchUpInside += HandleTouchUpInside;
			button12.TouchUpInside += HandleTouchUpInside;

			firstView.Add (button1);
			firstView.Add (button2);
			firstView.Add (button3);
			firstView.Add (button4);

			secondView = new UIView(scrollPageFrame);
			secondView.BackgroundColor = UIColor.Blue;
			scrollPageFrame.X += this.uiScrollView.Frame.Width;

			secondView.Add (button5);
			secondView.Add (button6);
			secondView.Add (button7);
			secondView.Add (button8);

			thirdView = new UIView(scrollPageFrame);
			thirdView.BackgroundColor = UIColor.Green;
			scrollPageFrame.X += this.uiScrollView.Frame.Width;

			thirdView.Add (button9);
			thirdView.Add (button10);
			thirdView.Add (button11);
			thirdView.Add (button12);

			uiScrollView.Add(firstView);
			uiScrollView.Add(secondView);
			uiScrollView.Add(thirdView);
		}

		void SendMessage (string command)
		{
			smsTask.SendSMS (command, PhoneNumber);
			//var uiAlert = new UIAlertView ("Text commandu", command, null, "Ok");
			//uiAlert.Show ();
		}

		void HandleTouchUpInside (object sender, EventArgs e)
		{
			string command = null;
			var clickedButon = sender as UIButton;
			switch (clickedButon.Tag) {
			case 1:
				{
					command = string.Format ("{0}", Password);
					SendMessage (command);
				}
				break;
			case 2:
				{
					command = string.Format("{0} ON USAGE MONITOR", Password);
					SendMessage (command);
				}
				break;
			case 3:
				{
					command = string.Format("{0} OFF USAGE MONITOR", Password);
					SendMessage (command);
				}
				break;
			case 4:
				{
					command = string.Format("{0} HELP", Password);
					SendMessage (command);
				}
				break;
			case 5:
				{
					command = string.Format("{0} SPEED LIMIT={1}", Password, MaxSpeed);
					SendMessage (command);
				}
				break;
			case 6:
				{
					command = string.Format("{0} SPEED LIMIT=0", Password);
					SendMessage (command);
				}
				break;
			case 7:
				{
					command = string.Format("{0} IMMOBILIZE", Password);
					SendMessage (command);
				}
				break;
			case 8:
				{
					command = string.Format("{0} FORCE IMMOBILIZE", Password);
					SendMessage (command);
				}
				break;
			case 9:
				{
					command = string.Format("{0} CANCEL IMMOBILIZE", Password);
					SendMessage (command);
				}
				break;
			case 10:
				{
					command = string.Format("{0} MAINTENANCE MODE", Password);
					SendMessage (command);
				}
				break;
			case 11:
				{
					command = string.Format("{0} TRANSPORT MODE", Password);
					SendMessage (command);
				}
				break;
			case 12:
				{
					command = string.Format("{0} NORMAL MODE", Password);
					SendMessage (command);
				}
				break;
			}
		}

		void HandleClicked (object sender, UIButtonEventArgs e)
		{
			var inputPassword = passwordAlert.GetTextField (0);
			if (inputPassword.Text == Password) {
				Session.FirstLaunch = false;
				return;
			} else {
				passwordAlert = new UIAlertView ("Prihlasenie", "Zadajte heslo", null, "Ok");
				passwordAlert.AlertViewStyle = UIAlertViewStyle.SecureTextInput;
				passwordAlert.Show ();
				passwordAlert.Clicked += HandleClicked;
			}	
		}

		private void pageControl_ValueChanged(object sender, EventArgs e)
		{
			PointF contentOffset = this.uiScrollView.ContentOffset;
			switch (this.uiPageControl.CurrentPage)
			{
			case 0:
				contentOffset.X = this.firstView.Frame.X;
				this.uiScrollView.SetContentOffset(contentOffset, true);
				break;
			case 1:
				contentOffset.X = this.secondView.Frame.X;
				this.uiScrollView.SetContentOffset(contentOffset, true);
				break;
			case 2:
				contentOffset.X = this.thirdView.Frame.X;
				this.uiScrollView.SetContentOffset(contentOffset, true);
				break;
			default:
				// do nothing	
				break;
			}
		}

		private void scrollView_DecelerationEnded(object sender, EventArgs e)
		{
			float x1 = this.firstView.Frame.X;
			float x2 = this.secondView.Frame.X;
			float x = this.uiScrollView.ContentOffset.X;
			if (x == x1){
				this.uiPageControl.CurrentPage = 0;
			} else if (x == x2){
				this.uiPageControl.CurrentPage = 1;
			} else{
				this.uiPageControl.CurrentPage = 2;
			}
		}

		private void ScrollEvent(object sender, EventArgs e)
		{
			this.uiPageControl.CurrentPage = (int)System.Math.Floor(uiScrollView.ContentOffset.X / this.uiScrollView.Frame.Size.Width);
		}
	}
}


