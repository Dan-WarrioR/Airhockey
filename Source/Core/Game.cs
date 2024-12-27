using SFML.Graphics;
using SFML.Window;
using Source.Characters;
using Source.Map;

namespace Source.Core
{
    public class Game
    {
        private static readonly (int Width, int Height) WindowSize = (1000, 500);

        private RenderWindow _window;
        
        private Player _player1;
        private Player _player2;
        
        private Puck _puck;

        private Field _field;
        
        public Game()
        {
            InitializeWindow();
            
            _field = new(WindowSize.Width, WindowSize.Height, 3f, WindowSize.Height / 3f);
            _player1 = new(InputType.WASD, new(100, WindowSize.Height / 2f), _window);
            _player2 = new(InputType.Arrows, new(WindowSize.Width - 100, WindowSize.Height / 2f), _window);
            _puck = new();
        }
        
        private void InitializeWindow()
        {
            var videoMode = new VideoMode((uint)WindowSize.Width, (uint)WindowSize.Height);
            _window = new(videoMode, "Air Hockey");
            _window.Closed += (_, __) => _window.Close();
        }
        
        public void StartGame()
        {
            Draw();

            while (!IsEndGame())
            {
                CalculateInput();
                ProcessTurn();
                Draw();
            }

            ProcessGameEnd();
        }

        private void CalculateInput()
        {
            _window.DispatchEvents();
        }

        private void ProcessTurn()
        {
            
        }

        private void ProcessGameEnd()
        {
            
        }

        


        private bool IsEndGame()
        {
            return !_window.IsOpen;
        }
        
        
        
        
        private void Draw()
        {
            _window.Clear(Color.Black);
            
            _field.Draw(_window);
            _player1.Draw(_window);
            _player2.Draw(_window);
            
            _window.Display(); 
        }
    }
}