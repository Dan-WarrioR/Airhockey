namespace Source.Abilities
{
	public abstract class BaseAbility
	{
		public bool IsActive { get; protected set; } = false;

		protected abstract float Duration { get; }

		protected abstract float CooldownDuration { get; }

		protected float CurrentTime { get; private set; }

		protected float CooldownTime { get; private set; }

		public virtual void Apply()
		{
			if (!CanUseAbility())
			{
				return;
			}

			IsActive = true;
			CurrentTime = 0f;
		}

		public virtual void Reset()
		{
			IsActive = false;
			CooldownTime = CooldownDuration;
			CurrentTime = 0f;
		}

		public virtual void Update(float deltaTime)
		{
			if (!IsActive)
			{
				CooldownTime -= deltaTime;

				return;
			}

			CurrentTime += deltaTime;

			if (CurrentTime >= Duration)
			{
				Reset();
			}
		}

		public virtual bool CanUseAbility()
		{
			if (IsActive || CooldownTime > 0)
			{
				return false;
			}

			return true;
		}
	}
}
