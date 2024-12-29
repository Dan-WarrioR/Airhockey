using SFML.Graphics;
using SFML.System;

namespace Source.Objects
{
	public class GameObject : Drawable
	{
		protected virtual Color FillColor => Color.White;

		public FloatRect ObjectRect => Shape.GetGlobalBounds();

		public Vector2f Position => Shape.Position;

		protected Vector2f InitialPosition { get; }

		protected Shape Shape { get; }

		public GameObject(Shape shape, Vector2f initialPosition)
		{
			Shape = shape;
			InitialPosition = initialPosition;

			Shape.Position = initialPosition;
			Shape.FillColor = FillColor;
		}

		public void ChangeShapeColor(Color color)
		{
			Shape.FillColor = color;
		}

		public virtual void Update(float deltaTime)
		{
			 
		}	

		public virtual bool CollideWith(GameObject gameObject)
		{
			return ObjectRect.Intersects(gameObject.ObjectRect);
		}

		public void Draw(RenderTarget target, RenderStates states)
		{
			Shape.Draw(target, states);
		}
	}
}
