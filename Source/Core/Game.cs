﻿using SFML.Graphics;
using SFML.System;
using Source.Objects;
using Source.Map;

namespace Source.Core
{
    public class Game
    {
        private const float PlayerRadius = 30f;
        private const float PuckRadius = 15f;
        private const float BorderWidth = 3f;

        private RenderWindow _window;
        private FloatRect _windowBounds;

		private Player _rightPlayer;
        private Player _leftPlayer;
        
        private Puck _puck;

        private Field _field;

		private TextObject _scoreText;

        private Clock _clock;

        private EntityHandler _entityHandler;

        public event Action<int, int> OnScoreChanged;

		public Game(RenderWindow window, EntityHandler entityHandler)
        {
			_window = window;
            _entityHandler = entityHandler;

			InitializeObjects();
		}

        private void InitializeObjects()
        {
            var windowSize = (Vector2f)_window.Size;

			_windowBounds = new(0, 0, windowSize.X ,windowSize.Y);

			float halfHeight = windowSize.Y / 2f;

			_field = new(windowSize.X, windowSize.Y, BorderWidth, windowSize.Y / 3f);
			_rightPlayer = new(InputType.WASD, PlayerRadius, new(100, halfHeight), windowSize);
			_leftPlayer = new(InputType.Arrows, PlayerRadius, new(windowSize.X - 100, halfHeight), windowSize);
			_puck = new(PuckRadius, new(windowSize.X / 2, halfHeight), _windowBounds);
            _scoreText = new(new(10, 10));
            _clock = new();

            OnScoreChanged += _scoreText.OnScoreChanged;

			_entityHandler.Add(_field);
            _entityHandler.Add(_rightPlayer);
            _entityHandler.Add(_leftPlayer);
            _entityHandler.Add(_puck);
            _entityHandler.Add(_scoreText);
		}

		public void StartGame()
        {
            Draw();

            while (!IsGameActive())
            {
                CalculateInput();
                ProcessLogic();
                Draw();
            }
        }

        private void CalculateInput()
        {
            _window.DispatchEvents();
            
            _rightPlayer.HandleInput();
            _leftPlayer.HandleInput();
		}

        private void ProcessLogic()
        {
            var deltaTime = _clock.Restart().AsSeconds();

            _entityHandler.UpdateAll(deltaTime);

            if (_field.IsInGoal(_puck, out GoalSide goalSide))
            {
                UpdateScore(goalSide);
			}

            CheckPuckWallCollisions();

            CheckPuckWithPlayerCollisions();

            _puck.Validate();
		}

        private void CheckPuckWallCollisions()
        {
			if (_field.IsIntersects(BorderType.Left, _puck) || _field.IsIntersects(BorderType.Right, _puck))
			{
				_puck.ChangeVelocity(new(-_puck.Velocity.X, _puck.Velocity.Y));
			}

			if (_field.IsIntersects(BorderType.Up, _puck) || _field.IsIntersects(BorderType.Down, _puck))
			{
				_puck.ChangeVelocity(new(_puck.Velocity.X, -_puck.Velocity.Y));
			}
		}

        private void CheckPuckWithPlayerCollisions()
        {
            if (_puck.IsIntersects(_rightPlayer))
            {
                _puck.ChangeVelocityFromPosition(_rightPlayer.Position, _rightPlayer.SpeedMultiplier);
			}
			else if (_puck.IsIntersects(_leftPlayer))
			{
				_puck.ChangeVelocityFromPosition(_leftPlayer.Position, _leftPlayer.SpeedMultiplier);
			}
		}

        private void UpdateScore(GoalSide goalSide)
        {
            Player player = goalSide == GoalSide.Right ? _rightPlayer : _leftPlayer;
			player.ChangeScore(player.Score + 1);

			_puck.Reset();

            OnScoreChanged?.Invoke(_leftPlayer.Score, _rightPlayer.Score);
        }
        


        private bool IsGameActive()
        {
            return !_window.IsOpen;
        }  
        
        
        
        private void Draw()
        {		
			_entityHandler.DrawAll();			
		}
    }
}