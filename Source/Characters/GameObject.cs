using SFML.Graphics;

namespace Source.Characters
{
	public class GameObject : Drawable
	{
		public FloatRect ObjectRect => Shape.GetGlobalBounds();

		public Shape Shape { get; }

		public GameObject(Shape shape)
		{
			Shape = shape;
		}

		public virtual void Update()
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
