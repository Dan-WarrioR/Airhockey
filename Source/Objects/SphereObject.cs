using SFML.Graphics;
using SFML.System;
using Source.Tools;

namespace Source.Objects
{
	public class SphereObject : ShapeObject
	{
		public float Radius { get; }

		public SphereObject(float radius, Vector2f initialPosition) : base(new CircleShape(radius), initialPosition)
		{
			Radius = radius;

			Shape.Origin = new(Radius, Radius);
		}

		public virtual bool IsIntersects(SphereObject sphere)
		{
			float distanceToObject = Position.Distance(sphere.Position);

			return distanceToObject <= Radius + sphere.Radius;
		}
	}
}
