using System;

namespace IFCViewer_Xamarin_iOS
{
	public interface IGameView
	{
		void Reshape (GameView view);

		void Render (GameView view);
	}
}

