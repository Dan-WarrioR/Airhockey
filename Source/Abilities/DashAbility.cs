namespace Source.Abilities
{
	public class DashAbility : BaseAbility
	{
		private const float _dashMultiplier = 2.5f;
		private const float _dashDuration = 0.2f;
		private const float _dashCooldown = 1f;

		public float SpeedMultiplier => IsActive ? _dashMultiplier : 1f;

		protected override float Duration => _dashDuration;

		protected override float CooldownDuration => _dashCooldown;
	}
}