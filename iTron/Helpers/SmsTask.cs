using System;
using MonoTouch.MessageUI;
using MonoTouch;

namespace iTron
{
	public class SmsTask
	{
		//private readonly TouchModalHost _modalHost;
		private MFMessageComposeViewController _sms;

			public SmsTask()
			{
			//	_modalHost = Mvx.Resolve<IMvxTouchModalHost>();
			}

			public void SendSMS(string body, string phoneNumber)
			{
			if (!MFMessageComposeViewController.CanSendText)
					return;

				_sms = new MFMessageComposeViewController {Body = body, Recipients = new[] {phoneNumber}};
				_sms.Finished += HandleSmsFinished;

			//_modalHost.PresentModalViewController(_sms, true);
			}

			private void HandleSmsFinished(object sender, MFMessageComposeResultEventArgs e)
			{
			//	var uiViewController = sender as UIViewController;
			//	if (uiViewController == null)
			//		throw new ArgumentException("sender");
			//
			//	uiViewController.DismissViewController(true, () => {});
			//	_modalHost.NativeModalViewControllerDisappearedOnItsOwn();
			}
	}

	public class CustomMessageComposeDelegate : MFMessageComposeViewControllerDelegate
	{
		public override void Finished (MFMessageComposeViewController controller,
			MessageComposeResult result)
		{
			// TODO: Implement the method to handle
			// Cancelled, Failed or Sent result.
		}
	}
}

