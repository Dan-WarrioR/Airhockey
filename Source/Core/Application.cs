using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Source.Core
{
	public class Application
	{
		private static readonly Vector2f WindowSize = new(1000, 500);

		private const string WindowAppName = "Air Hockey";

		public void Start()
		{
			RenderWindow window = CreateWindow();

			EntityHandler gameObjectManager = new(window);

			Game game = new(window, gameObjectManager);

			game.StartGame();
		}

		private RenderWindow CreateWindow()
		{
			var videoMode = new VideoMode((uint)WindowSize.X, (uint)WindowSize.Y);
			var window = new RenderWindow(videoMode, WindowAppName);

			window.Closed += (_, _) => window.Close();

			return window;
		}
	}
}
