namespace Source.Abilities
{
	public abstract class BaseAbility
	{
		public abstract void TryApply();

		public abstract void Reset();

		public abstract void Update(float deltaTime);
	}
}
