using SFML.Graphics;
using SFML.System;
using Source.Objects;
using Color = SFML.Graphics.Color;

namespace Source.Map
{
	public enum GoalSide
	{
		Left,
		Right,
	}

    public class Field
    {
		private static readonly Color BorderColor = Color.White;
		private static readonly Color GateColor = Color.Blue;

        public float Width { get; }
        public float Height { get; }
        
        private List<GameObject> _objects;

        private GameObject _leftGate;
        private GameObject _rightGate;

        public Field(float width, float height, float borderWidth, float gateHeight)
        {
            Width = width;
            Height = height;

            _objects = new();

			float widthDelta = Width - borderWidth;
			float heightDelta = Height - gateHeight;

			PlaceBorders(widthDelta, borderWidth, gateHeight);

			PlacePlayerGates(widthDelta, heightDelta, borderWidth, gateHeight);
		}
        
        public void Draw(RenderWindow window)
        {
            foreach (var gameObject in _objects)
            {
                window.Draw(gameObject); 
            }
		}

		public bool IsInGoal(Puck puck, out GoalSide goalSide)
		{
			if (_leftGate.CollideWith(puck))
			{
				goalSide = GoalSide.Left;

				return true;
			}

			if (_rightGate.CollideWith(puck))
			{
				goalSide = GoalSide.Right;

				return true;
			}

			goalSide = default;

			return false;
		}

        private void PlaceBorders(float widthDelta, float borderWidth, float gateHeight)
        {	
			Vector2f horizontalLineSize = new(Width, borderWidth);
			Vector2f vecrticalLineSize = new(borderWidth, Height);

			//Horizontal borders
			_objects.Add(new(new RectangleShape(horizontalLineSize)));
			_objects.Add(new(new RectangleShape(horizontalLineSize)
			{
				Position = new(0, Height - borderWidth),
			}));

			//Vertical borders
			_objects.Add(new(new RectangleShape(vecrticalLineSize)));
			_objects.Add(new(new RectangleShape(vecrticalLineSize)
			{
				Position = new(widthDelta, 0),
			}));

			//Center line
			//_objects.Add(new(new RectangleShape(vecrticalLineSize)
			//{
			//	Position = new(Width / 2f, 0),
			//}));

			foreach (var rectangle in _objects)
			{
				rectangle.Shape.FillColor = BorderColor;
			}
		}

		private void PlacePlayerGates(float widthDelta, float heightDelta, float borderWidth, float gateHeight)
		{
			var gateSize = new Vector2f(borderWidth, heightDelta / 2);

			_leftGate = new(new RectangleShape(gateSize)
			{
				Position = new(0, heightDelta / 2),
				FillColor = GateColor,
			});
			_rightGate = new(new RectangleShape(gateSize)
			{
				Position = new(widthDelta, heightDelta / 2),
				FillColor = GateColor,
			});

			_objects.Add(_leftGate);
			_objects.Add(_rightGate);
		}
	}
}