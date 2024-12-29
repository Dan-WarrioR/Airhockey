using SFML.Graphics;
using SFML.System;
using Source.Tools;

namespace Source.Objects
{
    public class Puck : SphereObject
    {
		private const float Speed = 100f;

		public Vector2f Velocity { get; private set; }

		public Puck(float radius, Vector2f initialPosition) : base(radius, initialPosition)
		{
			GenerateVelocity();
		}

		public override void Update(float deltaTime)
		{
			Shape.Position += Velocity * deltaTime;
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