using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace iTron
{
	public partial class SettingsController : DialogViewController
	{


		Settings settings = new Settings ();

		public string PhoneNumber {
			get {
				return settings.GetValueOrDefault<string> (Constant.PhoneNumber, "+");
			}
			set {
				settings.AddOrUpdateValue (Constant.PhoneNumber, value);
				settings.Save ();
			}
		}

		public string Password {
			get {
				return settings.GetValueOrDefault<string> (Constant.Password, "1234");
			}
			set {
				settings.AddOrUpdateValue (Constant.Password, value);
				settings.Save ();
			}
		}

		public bool UsePassword {
			get {
				return settings.GetValueOrDefault<bool> (Constant.UsePassword, false);
			}
			set {
				settings.AddOrUpdateValue (Constant.UsePassword, value);
				settings.Save ();
			}
		}

		public string MaxSpeed {
			get {
				return settings.GetValueOrDefault<string> (Constant.MaxSpeed, "120");
			}
			set {
				settings.AddOrUpdateValue (Constant.MaxSpeed, value);
				settings.Save ();
			}
		}

		EntryElement phoneNumber;
		EntryElement password;
		BooleanElement usePassword;
		EntryElement maxSpeed;


		public SettingsController () : base (UITableViewStyle.Grouped, null, true)
		{
			//NavigationBar.TintColor = UIColor.Black;

			phoneNumber = new EntryElement ("Tel. číslo","Tel. cislo",PhoneNumber);
			password = new EntryElement ("Heslo","Heslo", Password, true);
			usePassword = new BooleanElement ("Použiť heslo", UsePassword);
			maxSpeed = new EntryElement ("Max. rýchlosť","Max. rýchlosť",MaxSpeed);
			password.EntryEnded += (sender, e) => {
				var entry = sender as EntryElement;
				if (entry != null && entry.Value != null && entry.Value.Length != 4)
				{ 
					new UIAlertView ("Výstraha", "Heslo musí mať 4 znaky!", null, "Ok").Show ();

				}
			};

			phoneNumber.KeyboardType = UIKeyboardType.NumberPad;
			maxSpeed.KeyboardType = UIKeyboardType.NumberPad;

			Root = new RootElement ("Nastavenie") {
				new Section ("Základne nastavenie") {
					phoneNumber,
					password,
					usePassword,
					maxSpeed
				},
				new Section ("Informácie") {
					new StringElement ("Ako získať iTroner", () => {
						UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.itroner.sk/"));
					}),
				},
			};
		}

		public override void ViewWillDisappear (bool animated)
		{
			PhoneNumber = phoneNumber.Value;
			Password = password.Value;
			UsePassword = usePassword.Value;
			MaxSpeed = maxSpeed.Value;
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}
	}
}
