using SFML.Graphics;
using SFML.System;
using Source.Tools;

namespace Source.Objects
{
	public class Puck : CircleObject
    {
		private const float Speed = 250f;

		public Vector2f Velocity { get; private set; }

		private FloatRect _windowBounds;

		public Puck(float radius, Vector2f initialPosition, FloatRect windowBounds) : base(radius, initialPosition)
		{
			_windowBounds = windowBounds;

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

			Vector2f normalizedDirection = direction.Normalize();

			float newSpeed = Speed * targetSpeed;
			Vector2f newVelocity = normalizedDirection * newSpeed;

			ChangeVelocity(newVelocity);
		}

		public void Validate()
		{
			if (!_windowBounds.Contains(Position))
			{
				Reset();
			}
		}

		private void GenerateVelocity()
		{
			float angle = Random.Shared.NextSingle() * MathF.PI * 2;
			Velocity = new(MathF.Cos(angle) * Speed, MathF.Sin(angle) * Speed);
		}
	}
}