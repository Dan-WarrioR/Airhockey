using SFML.Graphics;
using SFML.System;

namespace Source.Objects
{
	public abstract class GameObject : Drawable
	{
		public virtual Vector2f Position { get; }

		protected Vector2f InitialPosition { get; }

		public GameObject(Vector2f initialPosition)
		{
			InitialPosition = initialPosition;

			Position = initialPosition;
		}

		protected abstract void ChangePosition(Vector2f position);

		public virtual void Update(float deltaTime)
		{

		}

		public abstract void Draw(RenderTarget target, RenderStates states);
	}
}
