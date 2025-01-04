using SFML.System;
using SFML.Window;
using Source.Abilities;

namespace Source.Objects
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

	public class Player : SphereObject
	{
		private const float _speed = 250f;

		public int Score { get; private set; } = 0;

		public float SpeedMultiplier => _dashAbility.SpeedMultiplier;

		private Vector2f _delta;

		private bool _isDashPressed = false;

		private DashAbility _dashAbility;

		private InputType _inputType;
		private Vector2f _mapSize;

		private List<MovementKey> _keyMap = new(4);

		private bool _isLeftSide = false;

		public Player(InputType inputType, float radius, Vector2f initialPosition, Vector2f mapSize) : base(radius, initialPosition)
		{
			_inputType = inputType;

			_mapSize = mapSize;
			_isLeftSide = initialPosition.X < mapSize.X / 2;

			_dashAbility = new DashAbility();

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

		public void ChangeScore(int score)
		{
			Score = score;
		}

		public void HandleInput()
		{
			_delta = GetDelta();

			_isDashPressed = OnDashKeyPressed();	
		}

		public override void Update(float deltaTime)
		{
			TryStartDash();

			_dashAbility.Update(deltaTime);

			float currentSpeed = _speed * SpeedMultiplier;

			ChangePosition(currentSpeed * deltaTime * _delta + Position);

			ClampPosition();
		}

		private void ClampPosition()
		{
			float centerX = _mapSize.X / 2;
			float minX = _isLeftSide ? Radius : centerX + Radius;
			float maxX = _isLeftSide ? centerX - Radius : _mapSize.X - Radius;

			float x = Math.Clamp(Position.X, minX, maxX);
			float y = Math.Clamp(Position.Y, Radius, _mapSize.Y - Radius);

			ChangePosition(new(x, y));
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

		//Dash

		private void TryStartDash()
		{		
			if (!CanStartDash())
			{
				return;
			}

			_dashAbility.TryApply();
		}

		private bool CanStartDash()
		{
			if (!_isDashPressed || _delta == new Vector2f(0, 0))
			{
				return false;
			}

			return true;
		}

		private bool OnDashKeyPressed()
		{
			return _inputType switch
			{
				InputType.WASD => Keyboard.IsKeyPressed(Keyboard.Key.LShift),
				InputType.Arrows => Keyboard.IsKeyPressed(Keyboard.Key.RShift),
			};
		}
	}
}