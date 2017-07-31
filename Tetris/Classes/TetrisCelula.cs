using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{


	public class TetrisCelula: Celula
	{
		// private readonly Color CELULA_COR_DE_FUNDO_OCUPADA = Color.White;
		 // private readonly Color CELULA_COR = Color.Yellow;

		public bool celulaEstaOcupada { get; set; } = false;
		public Color celulaCor { get; set; } = Color.White;
		public Color celulaCorFundo { get; set; } = Color.White;

		Texture2D texturaCelulaFundo;
		Texture2D texturaCelula;
		SpriteFont spriteFont;

		GraphicsDevice graphicsDevice { get; set; } = null;
		Rectangle celulaRetangulo;

		public new float Direita
		{
			get
			{
				return base.Direita;
			}
		}

		public new float Superior
		{
			get 
			{ 
				return base.Superior; 
			}
		}


		public TetrisCelula(int x, int y, int largura, int altura, GraphicsDevice graphicsDevice, SpriteFont spriteFont ) : base(x, y, largura, altura)
		{
			celulaCor = TetrisCelulaCor.NovaCor();
			celulaCorFundo = Color.White;

			texturaCelulaFundo = new Texture2D(graphicsDevice, 1, 1);
			texturaCelulaFundo.SetData<Color>(new Color[] { celulaCorFundo });

			texturaCelula = new Texture2D(graphicsDevice, 1, 1);
			texturaCelula.SetData<Color>(new Color[] { celulaCor});

			celulaRetangulo = new Rectangle(x, y, largura, altura);

			this.spriteFont = spriteFont;
		}

		public void AlterarCelulaCor(Color celulaCor)
		{
			texturaCelula.SetData<Color>(new Color[] { celulaCor } );
			this.celulaCor = celulaCor;
		}

		public void AlterarCelulaCorFundo(Color celulaCorFundo)
		{
			texturaCelulaFundo.SetData<Color>(new Color[] { celulaCorFundo });
			this.celulaCorFundo = celulaCorFundo;
		}


		public void Draw(GameTime gameTime, SpriteBatch spriteBatch, int linha, int coluna)
		{
			if (celulaEstaOcupada)
			{
				spriteBatch.Draw(texturaCelula, celulaRetangulo, celulaCor);

				/*
				if (coluna == 0)
				{
					spriteBatch.DrawString(spriteFont, linha.ToString(), new Vector2(celulaRetangulo.Left, celulaRetangulo.Top), Color.Red);
				}
				else
				{
					spriteBatch.DrawString(spriteFont, "1", new Vector2(celulaRetangulo.Left, celulaRetangulo.Top), Color.Red);
				}
				*/
			}
			else
			{
				spriteBatch.Draw(texturaCelulaFundo, celulaRetangulo, Color.White);

				/*
				if (coluna == 0)
				{
					spriteBatch.DrawString(spriteFont, linha.ToString(), new Vector2(celulaRetangulo.Left, celulaRetangulo.Top), Color.Red);
				}
				else
				{
					spriteBatch.DrawString(spriteFont, "0", new Vector2(celulaRetangulo.Left, celulaRetangulo.Top), Color.Red);
				}
				*/
			}
		}
	}
}
