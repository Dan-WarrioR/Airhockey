using SFML.Graphics;
using SFML.System;
using Color = SFML.Graphics.Color;

namespace Source.Map
{
    public class Field
    {
        private static readonly Color BorderColor = Color.Red;

        public int Width { get; }
        public int Height { get; }
        
        private List<RectangleShape> _rectangles;

        public Field(int width, int height, float borderWidth, float goalHeight)
        {
            Width = width;
            Height = height;

            _rectangles = new();
            
            float widthDelta = Width - borderWidth;
            float heightDelta = Height - goalHeight;
            
            _rectangles.Add(new(new Vector2f(Width, borderWidth)));
            _rectangles.Add(new(new Vector2f(borderWidth, heightDelta / 2)));
            _rectangles.Add(new(new Vector2f(Width, borderWidth))
            {
                Position = new(0, Height - borderWidth),
            });
            _rectangles.Add(new(new Vector2f(borderWidth, heightDelta / 2))
            {
                Position = new (0, Height - heightDelta / 2),
            });
            _rectangles.Add(new (new Vector2f(borderWidth, heightDelta / 2))
            {
                Position = new (widthDelta, 0),
            });
            _rectangles.Add(new(new Vector2f(borderWidth, heightDelta / 2))
            {
                Position = new(widthDelta, Height - heightDelta / 2),
            });
            _rectangles.Add(new(new Vector2f(borderWidth, Height))
            {
                Position = new(Width / 2f, 0),
            });

            foreach (var rectangle in _rectangles)
            {
                rectangle.FillColor = BorderColor;
            }
        }
        
        public void Draw(RenderWindow window)
        {
            foreach (var shape in _rectangles)
            {
                window.Draw(shape); 
            }
        }
    }
}