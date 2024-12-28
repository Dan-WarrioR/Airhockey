using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Source.Objects;
using Source.Map;

namespace Source.Core
{
    public class Game
    {
        private static readonly Vector2f WindowSize = new(1000, 500);
        private const float PlayerRadius = 30f;

        private RenderWindow _window;
        
        private Player _player1;
        private Player _player2;
        
        private Puck _puck;

        private Field _field;

		private Text _scoreText;

		public Game()
        {
            InitializeWindow();

            InitializeObjects();

			LoadScoreText();
		}
        
        private void InitializeWindow()
        {
            var videoMode = new VideoMode((uint)WindowSize.X, (uint)WindowSize.Y);
            _window = new(videoMode, "Air Hockey");
            _window.Closed += (_, _) => _window.Close();
        }

        private void InitializeObjects()
        {
			float borderWidth = 3f;
			float halfHeight = WindowSize.Y / 2f;

			_field = new(WindowSize.X, WindowSize.Y, borderWidth, WindowSize.Y / 3f);
			_player1 = new(InputType.WASD, PlayerRadius, new(100, halfHeight), WindowSize);
			_player2 = new(InputType.Arrows, PlayerRadius, new(WindowSize.X - 100, halfHeight), WindowSize);
			_puck = new(15f, new(WindowSize.X / 2, halfHeight));
		}

		private void LoadScoreText()
		{
			var font = new Font(@"C:\Windows\Fonts\Arial.ttf");
			_scoreText = new($"{_player1.Score} | {_player2.Score}", font, 24)
			{
				FillColor = Color.Black,
				Position = new Vector2f(10, 10),
			};
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
            
            _player1.Update();
            _player2.Update();
			_puck.Update();
		}

        private void ProcessLogic()
        {
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
            _window.Clear(Color.Black);

            _field.Draw(_window);
            
            _window.Draw(_player1);
            _window.Draw(_player2);
            _window.Draw(_puck);

            _window.Draw(_scoreText);

			_window.Display(); 
        }
    }
}