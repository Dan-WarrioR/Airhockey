using SFML.Graphics;
using SFML.System;

namespace Source.Objects
{
	public class GameObject : Drawable
	{
		public FloatRect ObjectRect => Shape.GetGlobalBounds();

		public Vector2f Position => Shape.Position;

		public Shape Shape { get; }

		public GameObject(Shape shape)
		{
			Shape = shape;
		}

		public virtual void Update()
		{
			 
		}	

		public virtual void Move(float deltaTime)
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
