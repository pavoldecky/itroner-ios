using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using System.Threading;
using System.Threading.Tasks;
using MonoTouch.MessageUI;
using MonoTouch.CoreGraphics;

namespace iTron
{
	public partial class MainController : UIViewController
	{
		public MainController () : base ("MainController", null)
		{
			settings = new Settings ();
		}

		private Settings settings;

		private UIScrollView uiScrollView;
		private UIPageControl uiPageControl; 

		private UIView firstView;
		private UIView secondView;
		private UIView thirdView;

		private float frameWidth = 0;
		private float frameHeight = 0;

		private SettingsController settingsScreen;

		private RectangleF viewFrame;

		private MFMessageComposeViewController messageController;

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

		public string CorrectImageLink (string imageLink)
		{
			if (IsIphone5) {
				var array = imageLink.Split ('.');
				return string.Join(".", string.Format ("{0}_5", array[0]), array[1]);
			}
			return imageLink;
		}

		private bool IsIphone5 {
			get;
			set;
		}

		public void ShowButton(UIButton button, string off, string on, SizeF size)
		{


//			var uiImageOff = UIImage.FromFile (CorrectImageLink("button_off.png"));
//			var uiImageOn = UIImage.FromFile (CorrectImageLink("button_on.png"));
			//var uiImageOn = uiImageOnOld.Scale (size);

			var uiImageOff = UIImage.FromFile (CorrectImageLink(off));
			var uiImageOn = UIImage.FromFile (CorrectImageLink(on));

			button.SetImage (uiImageOff, UIControlState.Normal);
			button.SetImage (uiImageOn, UIControlState.Selected|UIControlState.Highlighted);
			button.SetImage (uiImageOn, UIControlState.Selected);
			button.SetImage (uiImageOn, UIControlState.Highlighted);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			//Title = "iTroner";


			UIBarButtonItem baritem = new UIBarButtonItem (UIBarButtonSystemItem.Compose, delegate {
				if(this.settingsScreen == null) { this.settingsScreen = new SettingsController(); }
				this.NavigationController.PushViewController(this.settingsScreen, true);
			});

			NavigationItem.RightBarButtonItems = new UIBarButtonItem[]{ baritem };

			if (Session.FirstLaunch && UsePassword) {
				passwordAlert = new UIAlertView ("Prihlásenie", "Zadajte heslo", null, "Ok");
				passwordAlert.AlertViewStyle = UIAlertViewStyle.SecureTextInput;
				passwordAlert.Show ();
				passwordAlert.Clicked += HandleClicked;
			}



			SizeF nieco = View.Bounds.Size;

			SizeF viewSize = UIScreen.MainScreen.Bounds.Size;

			IsIphone5 = viewSize.Height == 568;

			//NavigationController.NavigationBar.Translucent = false;
			NavigationController.NavigationBar.TintColor = UIColor.Black;

			NavigationController.NavigationBar.SetBackgroundImage (new UIImage (), UIBarMetrics.Default); 
			NavigationController.NavigationBar.ShadowImage = new UIImage();
			NavigationController.NavigationBar.Translucent = true;
			NavigationItem.TitleView = new UIImageView (UIImage.FromFile(@"logo_platne.png")); 


			// ios7 layout
			if (RespondsToSelector (new Selector ("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.All;


			var uiScrollFrame = new RectangleF (0, 0, viewSize.Width, viewSize.Height -20);
			uiScrollView = new UIScrollView (uiScrollFrame);
			//uiScrollView.BackgroundColor = UIColor.Red;
			Add (uiScrollView);
			var uiPageControlFrame = new RectangleF (0, uiScrollView.Frame.Bottom, viewSize.Width, 20);
			uiPageControl = new UIPageControl (uiPageControlFrame);
			uiPageControl.Pages = 3;

			Add (uiPageControl);

			this.uiScrollView.DecelerationEnded += this.scrollView_DecelerationEnded;
			this.uiPageControl.ValueChanged += this.pageControl_ValueChanged;

			this.uiScrollView.PagingEnabled = true;

			RectangleF scrollPageFrame = this.uiScrollView.Frame;
			this.uiScrollView.ContentSize = new SizeF(scrollPageFrame.Width * 3, 300);

			var pageControlFrame = uiPageControl.Frame;

			firstView = new UIView(scrollPageFrame);
			//firstView.BackgroundColor = UIColor.Red;
			scrollPageFrame.X += this.uiScrollView.Frame.Width;

			viewFrame = firstView.Frame;
			frameHeight = viewFrame.Height - 64;
			frameWidth = viewFrame.Width;

			//View.BackgroundColor = UIColor.Clear;
			this.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile (@"pozadie.png"));

			float padding = 1;
			
			float buttonWidth = (frameWidth - (4 * padding));
			buttonWidth = buttonWidth / 2;
			float buttonHeight = (frameHeight - (4 * padding));
			buttonHeight = buttonHeight / 2;

			
			var button1Rect = new RectangleF (1, 1, buttonWidth, buttonHeight);
			var button2Rect = new RectangleF (button1Rect.Right + 2, 1, buttonWidth, buttonHeight);
			var button3Rect = new RectangleF (1, button1Rect.Bottom + 2, buttonWidth, buttonHeight);
			var button4Rect = new RectangleF (button1Rect.Right + 2, button1Rect.Bottom + 2, buttonWidth, buttonHeight);

//			float padding = 10;
//
//			float buttonWidth = (frameWidth - (3 * padding));
//			buttonWidth = buttonWidth / 2;
//			float buttonHeight = (frameHeight - (3 * padding));
//			buttonHeight = buttonHeight / 2;
//
//			var button1Rect = new RectangleF (padding, padding, buttonWidth, buttonHeight);
//			var button2Rect = new RectangleF (button1Rect.Right + padding, padding, buttonWidth, buttonHeight);
//			var button3Rect = new RectangleF (padding, button1Rect.Bottom + padding, buttonWidth, buttonHeight);
//			var button4Rect = new RectangleF (button1Rect.Right + padding, button1Rect.Bottom + padding, buttonWidth, buttonHeight);

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

			ShowButton (button1, "button1_off.png", "button1_on.png", button1Rect.Size);
			ShowButton (button2, "button2_off.png", "button2_on.png", button1Rect.Size);
			ShowButton (button3, "button3_off.png", "button3_on.png", button1Rect.Size);
			ShowButton (button4, "button4_off.png", "button4_on.png", button1Rect.Size);
			ShowButton (button5, "button5_off.png", "button5_on.png", button1Rect.Size);
			ShowButton (button6, "button6_off.png", "button6_on.png", button1Rect.Size);
			ShowButton (button7, "button7_off.png", "button7_on.png", button1Rect.Size);
			ShowButton (button8, "button8_off.png", "button8_on.png", button1Rect.Size);
			ShowButton (button9, "button9_off.png", "button9_on.png", button1Rect.Size);
			ShowButton (button10, "button10_off.png", "button10_on.png", button1Rect.Size);
			ShowButton (button11, "button11_off.png", "button11_on.png", button1Rect.Size);
			ShowButton (button12, "button12_off.png", "button12_on.png", button1Rect.Size);
		
//			ShowButton (button1, "button1_off.png", "button1_on.png", button1Rect.Size);
//			ShowButton (button2, "button2_off.png", "button2_on.png", button1Rect.Size);
//			ShowButton (button3, "button3_off.png", "button3_on.png", button1Rect.Size);
//			ShowButton (button4, "button4_off.png", "button4_on.png", button1Rect.Size);
//			ShowButton (button5, "button5_off.png", "button5_on.png", button1Rect.Size);
//			ShowButton (button6, "button6_off.png", "button6_on.png", button1Rect.Size);
//			ShowButton (button7, "button7_off.png", "button7_on.png", button1Rect.Size);
//			ShowButton (button8, "button8_off.png", "button8_on.png", button1Rect.Size);
//			ShowButton (button9, "button9_off.png", "button9_on.png", button1Rect.Size);
//			ShowButton (button10, "button10_off.png", "button10_on.png", button1Rect.Size);
//			ShowButton (button11, "button11_off.png", "button11_on.png", button1Rect.Size);
//			ShowButton (button12, "button12_off.png", "button12_on.png", button1Rect.Size);


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

			//button1.TouchUpInside += HandleTouchUpInside;
			//button2.TouchUpInside += HandleTouchUpInside;
			//button3.TouchUpInside += HandleTouchUpInside;
			//button4.TouchUpInside += HandleTouchUpInside;
			//button5.TouchUpInside += HandleTouchUpInside;
			//button6.TouchUpInside += HandleTouchUpInside;
			//button7.TouchUpInside += HandleTouchUpInside;
			//button8.TouchUpInside += HandleTouchUpInside;
			//button9.TouchUpInside += HandleTouchUpInside;
			//button10.TouchUpInside += HandleTouchUpInside;
			//button11.TouchUpInside += HandleTouchUpInside;
			//button12.TouchUpInside += HandleTouchUpInside;

			UILongPressGestureRecognizer recognizer = new UILongPressGestureRecognizer (longPress);
			button1.AddGestureRecognizer (recognizer);


			button1.TouchDown += HandleTouchDown;
			button2.TouchDown += HandleTouchDown;
			button3.TouchDown += HandleTouchDown;
			button4.TouchDown += HandleTouchDown;
			button5.TouchDown += HandleTouchDown;
			button6.TouchDown += HandleTouchDown;
			button7.TouchDown += HandleTouchDown;
			button8.TouchDown += HandleTouchDown;
			button9.TouchDown += HandleTouchDown;
			button10.TouchDown += HandleTouchDown;
			button11.TouchDown += HandleTouchDown;
			button12.TouchDown += HandleTouchDown;


			firstView.Add (button1);
			firstView.Add (button2);
			firstView.Add (button3);
			firstView.Add (button4);

			secondView = new UIView(scrollPageFrame);
			//secondView.BackgroundColor = UIColor.Blue;
			scrollPageFrame.X += this.uiScrollView.Frame.Width;

			secondView.Add (button5);
			secondView.Add (button6);
			secondView.Add (button7);
			secondView.Add (button8);

			thirdView = new UIView(scrollPageFrame);
			//thirdView.BackgroundColor = UIColor.Green;
			scrollPageFrame.X += this.uiScrollView.Frame.Width;

			thirdView.Add (button9);
			thirdView.Add (button10);
			thirdView.Add (button11);
			thirdView.Add (button12);

			uiScrollView.Add(firstView);
			uiScrollView.Add(secondView);
			uiScrollView.Add(thirdView);

		}

		void longPress (UILongPressGestureRecognizer gestureRecognizer)
		{
			if (gestureRecognizer.State == UIGestureRecognizerState.Ended) {
				var a = 10;
			}
		}

		async void HandleTouchDown (object sender, EventArgs e)
		{
			var clickedButon = sender as UIButton;
			clickedButon.Selected = !clickedButon.Selected;
			//clickedButon.Highlighted = !clickedButon.Highlighted;
			await Task.Delay(100);
			string command = null;
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
					SendMessageWithConfirmation (command);
				}
				break;
			case 8:
				{
					command = string.Format("{0} FORCE IMMOBILIZE", Password);
					SendMessageWithConfirmation (command);
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

			clickedButon.Selected = !clickedButon.Selected;
			//clickedButon.Highlighted = !clickedButon.Highlighted;
			//clickedButon.SetImage (UIImage.FromFile(@"off.jpeg"), UIControlState.Normal);
		}

		private void ShowUiAlert(string title, string text)
		{
			var uiAlert = new UIAlertView (title, text, null, "Ok");
			uiAlert.Show ();
		}


		void SendMessage (string command)
		{
			//ShowUiAlert("Text commnadu",command);
			if (MFMessageComposeViewController.CanSendText){
				this.messageController = new 
					MFMessageComposeViewController();
				this.messageController.Recipients = new string[] { PhoneNumber };
				this.messageController.Body = command;
				//this.messageController.MessageComposeDelegate = new MessageComposerDelegate();
				this.messageController.Finished += HandleFinished;

				//this.PresentModalViewController(this.messageController, true);
				this.PresentViewController(this.messageController, false, null);
			} else{
				ShowUiAlert("Chyba","Nie je možné odoslať SMS!");
			}

		}

		void HandleFinished (object sender, MFMessageComposeResultEventArgs e)
		{
			DismissViewController (true, null);
			//Console.WriteLine (e.Result);
		}
		// Displays a UIAlertView and returns the index of the button pressed.
		public static Task<string> ShowAlert (string title, string message, params string [] buttons)
		{
			var tcs = new TaskCompletionSource<string> ();
			var alert = new UIAlertView {
				Title = title,
				Message = message
			};
			foreach (var button in buttons)
				alert.AddButton (button);
			alert.Clicked += (s, e) => {
				var sender = s as UIAlertView;
				if (sender == null)
					tcs.TrySetResult(null);
				tcs.TrySetResult (sender.GetTextField(0).Text);
			};
			alert.AlertViewStyle = UIAlertViewStyle.SecureTextInput;
			alert.Show ();
			return tcs.Task;
		}

		async void SendMessageWithConfirmation (string command)
		{
			var providedPassword = await ShowAlert("Varovanie", "Chcete vykonať akciu", "Ok", "Zruš");
			if (providedPassword == Password) {
				SendMessage (command);
			}
		}

		void HandleTouchUpInside (object sender, EventArgs e)
		{
			string command = null;
			var clickedButon = sender as UIButton;
			//clickedButon.Selected = !clickedButon.Selected;
			//clickedButon.Highlighted = !clickedButon.Highlighted;
			//clickedButon.SetImage (UIImage.FromFile(@"on.jpeg"), UIControlState.Normal);	
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
					SendMessageWithConfirmation (command);
				}
				break;
			case 8:
				{
					command = string.Format("{0} FORCE IMMOBILIZE", Password);
					SendMessageWithConfirmation (command);
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
				passwordAlert = new UIAlertView ("Prihlásenie", "Zadajte heslo", null, "Ok");
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

		private class MessageComposerDelegate : 
		MFMessageComposeViewControllerDelegate{
			public override void Finished (MFMessageComposeViewController 
				controller, MessageComposeResult result){
				switch (result){
				case MessageComposeResult.Sent:
					Console.WriteLine("Message sent!");
					break;
				case MessageComposeResult.Cancelled:
					Console.WriteLine("Message cancelled!");
					break;
				default:
					Console.WriteLine("Message sending failed!");
					break;
				}
				//controller.DismissModalViewControllerAnimated(true);
				controller.DismissViewController (true, null);
			}
		}
	}

}

