using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Source.Core;

namespace Source.Characters
{
    public enum InputType
    {
        Arrows,
        WASD,
    }
    
    public class Player
    {
        private const float PuckRadius = 20f;

        private Vector2u _windowSize;
        
        public int Score { get; set; } = 0;

        public CircleShape Paddle { get; private set; }

        private InputType _inputType;

        private Vector2f _delta;
        
        public Player(InputType inputType, Vector2f startPosition, RenderWindow window)
        {
            _inputType = inputType;
            _windowSize = window.Size;
            
            Paddle = new(PuckRadius)
            {
                Position = startPosition,
                FillColor = inputType == InputType.WASD ? Color.Blue : Color.Red
            };

            window.KeyPressed += OnMoveKeyPressed;
        }

        public void HandleInput()
        {
            Paddle.Position += _delta * 5f; // Швидкість
            ClampPosition();
        }
        
        public void Draw(RenderWindow window)
        {
            window.Draw(Paddle);
        }
        
        private void ClampPosition()
        {
            float radius = Paddle.Radius;
            float x = Math.Clamp(Paddle.Position.X, radius, _windowSize.X - radius);
            float y = Math.Clamp(Paddle.Position.Y, radius, _windowSize.Y - radius);
            Paddle.Position = new Vector2f(x, y);
        }
        
        private void OnMoveKeyPressed(object? sender, KeyEventArgs e)
        {
            _delta = _inputType switch
            {
                InputType.Arrows => e.Code switch
                {
                    Keyboard.Key.Up => new Vector2f(0, -1),
                    Keyboard.Key.Down => new Vector2f(0, 1),
                    Keyboard.Key.Left => new Vector2f(-1, 0),
                    Keyboard.Key.Right => new Vector2f(1, 0),
                    _ => new Vector2f(0, 0),
                },
                
                InputType.WASD => e.Code switch
                {
                    Keyboard.Key.W => new Vector2f(0, -1),
                    Keyboard.Key.S => new Vector2f(0, 1),
                    Keyboard.Key.A => new Vector2f(-1, 0),
                    Keyboard.Key.D => new Vector2f(1, 0),
                    _ => new Vector2f(0, 0),
                },
            };

            HandleInput();
        } 
    }
}