namespace Source.Abilities
{
	public class DashAbility : BaseAbility
	{
		private const float _dashMultiplier = 2.5f;
		private const float _dashDuration = 0.2f;
		private const float _dashCooldown = 1.0f;

		public float SpeedMultiplier => IsDashing ? _dashMultiplier : 1f;

		public bool IsDashing { get; private set; }

		private float _currentDashTime = 0f;
		private float _dashCooldownTime = 0f;

		public override void TryApply()
		{
			if (!CanStartDash())
			{
				return;
			}

			ApplyDash();
		}

		public override void Reset()
		{
			IsDashing = false;
			_dashCooldownTime = _dashCooldown;
			_currentDashTime = 0f;
		}

		public override void Update(float deltaTime)
		{
			if (IsDashing)
			{
				_currentDashTime += deltaTime;

				if (_currentDashTime >= _dashDuration)
				{
					Reset();
				}

				return;
			}

			_dashCooldownTime -= deltaTime;
		}

		private void ApplyDash()
		{
			IsDashing = true;
			_currentDashTime = 0f;
		}

		private bool CanStartDash()
		{
			if (IsDashing || _dashCooldownTime > 0)
			{
				return false;
			}

			return true;
		}
	}
}