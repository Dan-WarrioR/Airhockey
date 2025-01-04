using SFML.Graphics;
using SFML.System;
using Source.Tools;

namespace Source.Objects
{
	public class SphereObject : GameObject
	{
		protected virtual Color FillColor => Color.White;

		public FloatRect ObjectRect => Shape.GetGlobalBounds();

		public override Vector2f Position => Shape.Position;

		public float Radius { get; }

		protected Shape Shape { get; }

		public SphereObject(float radius, Vector2f initialPosition) : base(initialPosition)
		{
			Radius = radius;

			Shape = new CircleShape(radius)
			{
				Position = initialPosition,
				Origin = new(Radius, Radius),
				FillColor = FillColor,
			};
		}

		protected override void ChangePosition(Vector2f position)
		{
			Shape.Position = position;
		}

		public virtual bool IsIntersects(SphereObject sphere)
		{
			float distanceToObject = Position.Distance(sphere.Position);

			return distanceToObject <= Radius + sphere.Radius;
		}

		public virtual bool IsIntersects(FloatRect objectRect)
		{
			return ObjectRect.Intersects(objectRect);
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			Shape.Draw(target, states);
		}
	}
}
