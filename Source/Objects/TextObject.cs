using SFML.Graphics;
using SFML.System;

namespace Source.Objects
{
	public class TextObject : GameObject
	{
		private static readonly Color TextColor = Color.White;

		private const string FontPath = @"C:\Windows\Fonts\Arial.ttf";
		private const uint CharacterSize = 24;

		public override Vector2f Position => _text.Position;

		private Text _text;

		public TextObject(Vector2f initialPosition) : base(initialPosition)
		{
			var font = new Font(FontPath);

			_text = new("0 | 0", font, CharacterSize)
			{
				FillColor = TextColor,
				Position = initialPosition,
			};
		}

		protected override void ChangePosition(Vector2f position)
		{
			_text.Position = position;
		}

		public void ChangeText(string text)
		{
			_text.DisplayedString = text;
		}

		public void OnScoreChanged(int leftPlayerScore, int rightPlayerScore)
		{
			ChangeText($"{rightPlayerScore} | {leftPlayerScore}");
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			_text.Draw(target, states);
		}
	}
}
