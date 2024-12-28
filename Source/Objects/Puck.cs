using SFML.Graphics;
using SFML.System;

namespace Source.Objects
{
    public class Puck : GameObject
    {
		private static readonly Color PuckColor = Color.White;

		private const float Speed = 0.5f;

		public float Radius { get; }

		public Vector2f Velocity { get; private set; }

		private Vector2f InitialPosition { get; }

		public Puck(float radius, Vector2f initialPosition) : base(new CircleShape(radius))
		{
			Radius = radius;
			InitialPosition = initialPosition;

			Shape.FillColor = PuckColor;
			Shape.Origin = new(Radius, Radius);
			Shape.Position = initialPosition;

			GenerateVelocity();
		}

		public void Move()
		{
			Shape.Position += Velocity;
		}

		public void Reset()
		{
			Shape.Position = InitialPosition;
			GenerateVelocity();
		}

		public void ChangeVelocity(Vector2f newVelocity)
		{
			Velocity = newVelocity;
		}

		private void GenerateVelocity()
		{
			float angle = Random.Shared.NextSingle() * MathF.PI * 2;
			Velocity = new Vector2f(MathF.Cos(angle) * Speed, MathF.Sin(angle) * Speed);
		}
	}
}