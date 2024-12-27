using SFML.Graphics;
using SFML.System;

namespace Source.Characters
{
    public class Puck : GameObject
    {
		private static readonly Color PuckColor = Color.White;

		private const float PuckRadius = 15f;
		private const float Speed = 1f;

		private Vector2f InitialPosition { get; }

		private Vector2f _velocity;

		public Puck(Vector2f initialPosition) : base(new CircleShape(PuckRadius))
		{
			InitialPosition = initialPosition;

			Shape.FillColor = Color.White;
			Shape.Origin = new(PuckRadius, PuckRadius);
			Shape.Position = initialPosition;

			GenerateVelocity();
		}

		public override void Update()
		{
			Shape.Position += _velocity;
		}

		public void Reset()
		{
			Shape.Position = InitialPosition;
			GenerateVelocity();
		}

		public void ChangeVelocity(Vector2f newVelocity)
		{
			_velocity = newVelocity;
		}

		private void GenerateVelocity()
		{
			_velocity = new(0.5f, 0);
			return;

			float angle = Random.Shared.NextSingle() * MathF.PI * 2;
			_velocity = new Vector2f(MathF.Cos(angle) * Speed, MathF.Sin(angle) * Speed);
		}
	}
}