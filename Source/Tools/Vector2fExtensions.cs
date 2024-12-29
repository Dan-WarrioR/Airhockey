using SFML.System;

namespace Source.Tools
{
	public static class Vector2fExtensions
	{
		public static float Distance(this Vector2f source, Vector2f second)
		{
			float deltaX = second.X - source.X;
			float deltaY = second.Y - source.Y;

			return MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
		}
	}
}
