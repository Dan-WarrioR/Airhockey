using SFML.Graphics;
using SFML.System;

namespace Source.Objects
{
	public class ShapeObject : GameObject
	{
		protected virtual Color FillColor => Color.White;

		public FloatRect ObjectRect => Shape.GetGlobalBounds();

		public override Vector2f Position => Shape.Position;

		protected Shape Shape { get; }

		public ShapeObject(Shape shape, Vector2f initialPosition) : base(initialPosition)
		{
			Shape = shape;

			Shape.Position = initialPosition;
		}

		protected override void ChangePosition(Vector2f position)
		{
			Shape.Position = position;
		}

		public virtual bool IsIntersects(ShapeObject shapeObject)
		{
			return ObjectRect.Intersects(shapeObject.ObjectRect);
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			Shape.Draw(target, states);
		}
	}
}
