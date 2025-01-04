using SFML.Graphics;
using Source.Objects;

namespace Source.Core
{
	public class EntityHandler
	{
		private readonly List<GameObject> _gameObjects = new();

		private readonly RenderWindow _window;

		public EntityHandler(RenderWindow window)
		{
			_window = window;
		}

		public void Add(GameObject gameObject)
		{
			_gameObjects.Add(gameObject);
		}

		public void Remove(GameObject gameObject)
		{
			_gameObjects.Remove(gameObject);
		}

		public void UpdateAll(float deltaTime)
		{
			foreach (var gameObject in _gameObjects)
			{
				gameObject.Update(deltaTime);
			}
		}

		public void DrawAll()
		{
			_window.Clear(Color.Black);

			foreach (var gameObject in _gameObjects)
			{
				_window.Draw(gameObject);
			}

			_window.Display();
		}
	}
}
