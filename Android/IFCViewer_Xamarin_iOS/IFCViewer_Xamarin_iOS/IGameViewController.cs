using System;

namespace IFCViewer_Xamarin_iOS
{
	public interface IGameViewController
	{
		void RenderViewControllerUpdate (GameViewController gameViewController);

		void RenderViewController (GameViewController gameViewController, bool value);
	}

}
