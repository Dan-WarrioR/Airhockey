using SFML.Graphics;
using SFML.System;
using Source.Objects;
using Source.Map;

namespace Source.Core
{
    public class Game
    {
        private const float PlayerRadius = 30f;
        private const float PuckRadius = 15f;

        private RenderWindow _window;
        
        private Player _player1;
        private Player _player2;
        
        private Puck _puck;

        private Field _field;

		private TextObject _scoreText;

        private Clock _clock;

        private GameObjectManager _gameObjectManager;

		public Game(RenderWindow window, GameObjectManager gameObjectManager)
        {
			_window = window;
            _gameObjectManager = gameObjectManager;

			InitializeObjects();
		}

        private void InitializeObjects()
        {
			float borderWidth = 3f;

            var windowSize = (Vector2f)_window.Size;

			float halfHeight = windowSize.Y / 2f;

			_field = new(windowSize.X, windowSize.Y, borderWidth, windowSize.Y / 3f);
			_player1 = new(InputType.WASD, PlayerRadius, new(100, halfHeight), windowSize);
			_player2 = new(InputType.Arrows, PlayerRadius, new(windowSize.X - 100, halfHeight), windowSize);
			_puck = new(PuckRadius, new(windowSize.X / 2, halfHeight));
            _scoreText = new(new(10, 10), $"{_player1.Score} | {_player2.Score}");
            _clock = new();

            _gameObjectManager.Add(_field);
            _gameObjectManager.Add(_player1);
            _gameObjectManager.Add(_player2);
            _gameObjectManager.Add(_puck);
            _gameObjectManager.Add(_scoreText);
		}

		public void StartGame()
        {
            Draw();

            while (!IsEndGame())
            {
                CalculateInput();
                ProcessLogic();
                Draw();
            }
        }

        private void CalculateInput()
        {
            _window.DispatchEvents();
            
            _player1.HandleInput();
            _player2.HandleInput();
		}

        private void ProcessLogic()
        {
            var deltaTime = _clock.Restart().AsSeconds();

            _gameObjectManager.UpdateAll(deltaTime);

            if (_field.IsInGoal(_puck, out GoalSide goalSide))
            {
                UpdateScore(goalSide);
			}

            CheckPuckWallCollisions();

            CheckPuckWithPlayerCollisions();
		}

        private void CheckPuckWallCollisions()
        {
			if (_puck.Position.X - _puck.Radius <= 0 || _puck.Position.X + _puck.Radius >= _field.Width)
			{
				_puck.ChangeVelocity(new(-_puck.Velocity.X, _puck.Velocity.Y));
			}

			if (_puck.Position.Y - _puck.Radius <= 0 || _puck.Position.Y + _puck.Radius >= _field.Height)
			{
				_puck.ChangeVelocity(new(_puck.Velocity.X, -_puck.Velocity.Y));
			}
		}

        private void CheckPuckWithPlayerCollisions()
        {
            if (_puck.CollideWith(_player1))
            {
				ChangePuckVelocity(_player1);
			}
			else if (_puck.CollideWith(_player2))
			{
				ChangePuckVelocity(_player2);
			}
		}

        private void ChangePuckVelocity(Player player)
        {
			Vector2f direction = _puck.Position - player.Position;

			float magnitude = MathF.Sqrt(direction.X * direction.X + direction.Y * direction.Y);

			if (magnitude == 0)
            {
                return;
            }

			direction /= magnitude;

			float speed = MathF.Sqrt(_puck.Velocity.X * _puck.Velocity.X + _puck.Velocity.Y * _puck.Velocity.Y);

			Vector2f newVelocity = direction * speed;

			_puck.ChangeVelocity(newVelocity);
		}

        private void UpdateScore(GoalSide goalSide)
        {
            Player player = goalSide == GoalSide.Right ? _player1 : _player2;
            player.Score++;

            _scoreText.DisplayedString = $"{_player1.Score} | {_player2.Score}";

			_puck.Reset();
        }
        


        private bool IsEndGame()
        {
            return !_window.IsOpen;
        }  
        
        
        
        private void Draw()
        {		
			_gameObjectManager.DrawAll();			
		}
    }
}