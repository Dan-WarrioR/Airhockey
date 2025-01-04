using SFML.Graphics;
using SFML.System;
using Source.Objects;
using Color = SFML.Graphics.Color;

namespace Source.Map
{
	public enum BorderType
	{
		Up,
		Down, 
		Left, 
		Right,
		LeftGate,
		RightGate,
	}

	public enum GoalSide
	{
		Left,
		Right,
	}

    public class Field : GameObject
    {
		private static readonly Color BorderColor = Color.White;
		private static readonly Color GateColor = Color.Black;

        public float Width { get; }
        public float Height { get; }
        
		private Dictionary<BorderType, Shape> _borders;

		public Field(float width, float height, float borderWidth, float gateHeight) : base(new(0, 0))
        {
            Width = width;
            Height = height;

			_borders = new();

			float widthDelta = Width - borderWidth;
			float heightDelta = Height - gateHeight;

			PlaceBorders(widthDelta, borderWidth, gateHeight);

			PlacePlayerGates(widthDelta, heightDelta, borderWidth, gateHeight);
		}

		protected override void ChangePosition(Vector2f position)
		{
			
		}

		public bool IsInGoal(Puck puck, out GoalSide goalSide)
		{
			if (IsIntersects(BorderType.LeftGate, puck))
			{
				goalSide = GoalSide.Left;

				return true;
			}

			if (IsIntersects(BorderType.RightGate, puck))
			{
				goalSide = GoalSide.Right;

				return true;
			}

			goalSide = default;

			return false;
		}

		public bool IsIntersects(BorderType borderType, SphereObject targetObject)
		{
			if (!_borders.TryGetValue(borderType, out Shape? shape))
			{
				return false;
			}

			return IsIntersects(shape, targetObject);
		}
		
		public override void Draw(RenderTarget target, RenderStates states)
		{
			foreach (var item in _borders.Values)
			{
				item.Draw(target, states);
			}
		}

		private bool IsIntersects(Shape shape, SphereObject otherObject)
		{
			var shapeBounds = shape.GetGlobalBounds();
			
			return shapeBounds.Intersects(otherObject.ObjectRect);
		}

		private void PlaceBorders(float widthDelta, float borderWidth, float gateHeight)
        {	
			Vector2f horizontalLineSize = new(Width, borderWidth);
			Vector2f vecrticalLineSize = new(borderWidth, Height);

			var upBorder = new RectangleShape(horizontalLineSize);
			var downBorder = new RectangleShape(horizontalLineSize)
			{
				Position = new(0, Height - borderWidth),
			};

			var leftBorder = new RectangleShape(vecrticalLineSize);
			var rightBorder = new RectangleShape(vecrticalLineSize)
			{
				Position = new(widthDelta, 0),
			};

			_borders.Add(BorderType.Up, upBorder);
			_borders.Add(BorderType.Down, downBorder);
			_borders.Add(BorderType.Left, leftBorder);
			_borders.Add(BorderType.Right, rightBorder);

			foreach (var shape in _borders.Values)
			{
				shape.FillColor = BorderColor;
			}
		}

		private void PlacePlayerGates(float widthDelta, float heightDelta, float borderWidth, float gateHeight)
		{
			var gateSize = new Vector2f(borderWidth, heightDelta / 2);

			var leftGate = new RectangleShape(gateSize)
			{
				Position = new(0, heightDelta / 2),
				FillColor = GateColor,
			};

			var rightGate = new RectangleShape(gateSize)
			{
				Position = new(widthDelta, heightDelta / 2),
				FillColor = GateColor,
			};

			_borders.Add(BorderType.LeftGate, leftGate);
			_borders.Add(BorderType.RightGate, rightGate);
		}		
	}
}