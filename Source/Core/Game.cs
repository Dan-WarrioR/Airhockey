using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Source.Characters;
using Source.Map;

namespace Source.Core
{
    public class Game
    {
        private static readonly Vector2f WindowSize = new(1000, 500);

        private RenderWindow _window;
        
        private Player _player1;
        private Player _player2;
        
        private Puck _puck;

        private Field _field;
        
        public Game()
        {
            InitializeWindow();
            
            _field = new((int)WindowSize.X, (int)WindowSize.Y, 3f, WindowSize.Y / 3f);
            _player1 = new(InputType.WASD, new(100, WindowSize.Y / 2f), WindowSize);
            _player2 = new(InputType.Arrows, new(WindowSize.X - 100, WindowSize.Y / 2f), WindowSize);
            _puck = new(new Vector2f(WindowSize.X / 2, WindowSize.Y / 2));
        }
        
        private void InitializeWindow()
        {
            var videoMode = new VideoMode((uint)WindowSize.X, (uint)WindowSize.Y);
            _window = new(videoMode, "Air Hockey");
            _window.Closed += (_, _) => _window.Close();
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

            ProcessGameEnd();
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

            CheckCollisions();
		}

        private void CheckCollisions()
        {

        }

        private void UpdateScore(GoalSide goalSide)
        {
            Player player = goalSide == GoalSide.Right ? _player1 : _player2;
            player.Score++;

			_puck.Reset();
        }

        private void ProcessGameEnd()
        {
            Console.WriteLine($"Player 1 - {_player1.Score}" +
                $"\nPlayer 2 - {_player2.Score}");
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

			_window.Display(); 
        }
    }
}