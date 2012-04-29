using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMercury;
using ProjectMercury.Emitters;
using ProjectMercury.Renderers;

namespace MercuryExample
{
	public class Game1 : Game
	{
		private readonly GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private SpriteBatchRenderer _spriteBatchRenderer;
		private ParticleEffect _particleEffect;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			TargetElapsedTime = TimeSpan.FromTicks(333333);
			InactiveSleepTime = TimeSpan.FromSeconds(1);

			_spriteBatchRenderer = new SpriteBatchRenderer();//{ GraphicsDeviceService = _graphics };
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
			
		}

		protected override void LoadContent()
		{
			_particleEffect = Content.Load<ParticleEffect>("Demo1");
			
			foreach (var emitter in _particleEffect.Emitters)
			{
				emitter.ParticleTexture = Content.Load<Texture2D>("Star");
				emitter.Initialise();
			}
			_spriteBatchRenderer.LoadContent(Content);
		}

		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			// TODO: Add your update logic here
			var position = new Vector3(150, 150, 0);
			_particleEffect.Trigger(ref position);
			_particleEffect.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			var matrix = Matrix.Identity;
			var cameraPosition = Vector3.Zero;
			_spriteBatchRenderer.Transformation = Matrix.Identity;
			_spriteBatchRenderer.RenderEffect(_particleEffect, ref matrix, ref matrix, ref matrix, ref cameraPosition);
			base.Draw(gameTime);
		}
	}
}
