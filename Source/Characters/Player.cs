using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Source.Characters
{
	public struct MovementKey
	{
		public Keyboard.Key Key { get; }
		public float DeltaX { get; }
		public float DeltaY { get; }

		public MovementKey(Keyboard.Key key, float deltaX, float deltaY)
		{
			Key = key;
			DeltaX = deltaX;
			DeltaY = deltaY;
		}
	}

	public enum InputType
	{
		Arrows,
		WASD,
	}

	public class Player : GameObject
	{
		private const float _speed = 0.5f;
		private const float PuckRadius = 20f;

		public int Score { get; set; } = 0;

		private InputType _inputType;

		private Vector2f _mapSize;

		private List<MovementKey> _keyMap = new(4);

		private bool _isLeftSide = false;

		public Player(InputType inputType, Vector2f startPosition, Vector2f mapSize) : base(new CircleShape(PuckRadius))
		{
			_inputType = inputType;
			_mapSize = mapSize;
			_isLeftSide = startPosition.X < mapSize.X / 2;

			Shape.Position = startPosition;
			Shape.FillColor = inputType == InputType.WASD ? Color.Blue : Color.Red;
			Shape.Origin = new(PuckRadius, PuckRadius);
			
			_keyMap = _inputType switch
			{
				InputType.WASD => new()
				{
					new(Keyboard.Key.W, 0, -1),
					new(Keyboard.Key.S, 0, 1),
					new(Keyboard.Key.A, -1, 0),
					new(Keyboard.Key.D, 1, 0),
				},
				InputType.Arrows => new()
				{
					new(Keyboard.Key.Up, 0, -1),
					new(Keyboard.Key.Down, 0, 1),
					new(Keyboard.Key.Left, -1, 0),
					new(Keyboard.Key.Right, 1, 0),
				},
			};
		}

		public override void Update()
		{
			var delta = GetDelta();

			Shape.Position += delta * _speed;

			ClampPosition();
		}

		private void ClampPosition()
		{
			float centerX = _mapSize.X / 2;
			float minX = _isLeftSide ? PuckRadius : centerX + PuckRadius;
			float maxX = _isLeftSide ? centerX - PuckRadius : _mapSize.X - PuckRadius;

			float x = Math.Clamp(Shape.Position.X, minX, maxX);
			float y = Math.Clamp(Shape.Position.Y, PuckRadius, _mapSize.Y - PuckRadius);
			Shape.Position = new(x, y);
		}

		private Vector2f GetDelta()
		{
			float deltaX = 0;
			float deltaY = 0;

			foreach (var key in _keyMap)
			{
				if (Keyboard.IsKeyPressed(key.Key))
				{
					deltaX += key.DeltaX;
					deltaY += key.DeltaY;
				}
			}

			return new(deltaX, deltaY);
		}
	}
}