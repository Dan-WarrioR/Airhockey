using SFML.System;
using System.Numerics;

namespace Source.Objects
{
	public class Puck : SphereObject
    {
		private const float Speed = 250f;

		public Vector2f Velocity { get; private set; }

		public Puck(float radius, Vector2f initialPosition) : base(radius, initialPosition)
		{
			GenerateVelocity();
		}

		public override void Update(float deltaTime)
		{
			ChangePosition(Velocity * deltaTime + Position);
		}

		public void Reset()
		{
			ChangePosition(InitialPosition);
			GenerateVelocity();
		}

		public void ChangeVelocity(Vector2f newVelocity)
		{
			Velocity = newVelocity;
		}

		public void ChangeVelocityFromPosition(Vector2f targetPosition, float targetSpeed)
		{
			Vector2f direction = Position - targetPosition;

			float magnitude = MathF.Sqrt(direction.X * direction.X + direction.Y * direction.Y);

			if (magnitude == 0)
			{
				return;
			}

			direction /= magnitude;

			float newSpeed = Speed * targetSpeed;
			Vector2f newVelocity = direction * newSpeed;

			ChangeVelocity(newVelocity);
		}

		private void GenerateVelocity()
		{
			float angle = Random.Shared.NextSingle() * MathF.PI * 2;
			Velocity = new Vector2f(MathF.Cos(angle) * Speed, MathF.Sin(angle) * Speed);
		}
	}
}