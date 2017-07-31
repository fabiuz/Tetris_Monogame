
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
	public class Tabuleiro
	{
		public readonly int CELULAS_POR_COLUNA = 10;
		public readonly int CELULAS_POR_LINHA = 20;
		private const int CELULA_LARGURA = 25;
		private const int CELULA_ALTURA = 25;
		private const int ESPACO_HORIZONTAL_ENTRE_CELULAS = 1;
		private const int ESPACO_VERTICAL_ENTRE_CELULAS = 1;

		private const int xCantoSuperiorEsquerdo = 5;
		private const int yCantoSuperiorEsquerdo = 5;

		public readonly Color TABULEIRO_COR_FUNDO = Color.WhiteSmoke;

		public TetrisEstado tetrisEstado = TetrisEstado.TETRIS_NOVO_JOGO;

		private JogoFaseEstado jogoFase = JogoFaseEstado.FASE_0;

		private TetrisCelula[,] celulasTabuleiro;
		private TetrisPeca tetrisPeca;

		/// <summary>
		/// Estrutura que armazena os intervalos de tempo.
		/// </summary>
		private TetrisPecaTempo pecaTempo; 

		public static GraphicsDevice graphicsDevice;
		public static ContentManager contentManager;
		public static SpriteFont spriteFont;


		public Tabuleiro()
		{
			CriarTabuleiro();
		}

		public void Update(GameTime gameTime)
		{
			switch (tetrisEstado)
			{
				case TetrisEstado.TETRIS_NOVO_JOGO:
					VerificarTeclaNovoJogo();
					break;

				case TetrisEstado.TETRIS_ANTES_DE_COMECAR_NOVO_JOGO:
					NovoJogo();
					break;

				case TetrisEstado.TETRIS_JOGANDO:
					VerificarTeclas(gameTime);
					break;
			}			
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			switch (tetrisEstado)
			{
				case TetrisEstado.TETRIS_JOGANDO:
					DesenharTabuleiro(gameTime, spriteBatch);
					break;
			}
		}

		/// <summary>
		/// Inicia um novo jogo.
		/// Reseta tabuleiro e sortea uma nova peça.
		/// </summary>
		public void NovoJogo()
		{
			ResetarTabuleiro();
			tetrisPeca = new TetrisPeca(this, ref celulasTabuleiro);
			tetrisPeca.SortearPeca();

			// Alterar estado pra jogando.
			tetrisEstado = TetrisEstado.TETRIS_JOGANDO;
			jogoFase = JogoFaseEstado.FASE_0;

			DefinirTempo();
		}

		/// <summary>
		/// Reseta o tabuleiro para iniciar um novo jogo.
		/// </summary>
		public void ResetarTabuleiro()
		{
			// Sempre existirá uma instância da variável celulasTabuleiro.
			// Ao instanciarmos a classe 'Tabuleiro'.
			for (var uLinha = 0; uLinha < CELULAS_POR_LINHA; uLinha++)
			{
				for (var uColuna = 0; uColuna < CELULAS_POR_COLUNA; uColuna++)
				{
					celulasTabuleiro[uLinha, uColuna].celulaCor = Color.White;
					celulasTabuleiro[uLinha, uColuna].celulaEstaOcupada = false;
				}
			}
		}

		public void VerificarTeclaNovoJogo()
		{
			if (Keyboard.GetState().IsKeyDown(Keys.F2))
			{
				tetrisEstado = TetrisEstado.TETRIS_ANTES_DE_COMECAR_NOVO_JOGO;
			}			
		}

		/// <summary>
		/// Define o intervalo de tempo que uma peça
		/// deve ter após o qual ele pode se mover.
		/// </summary>
		public void DefinirTempo()
		{
			
			switch (jogoFase)
			{
				case JogoFaseEstado.FASE_0:
					pecaTempo.intervaloDeslocarHorizontal = 100;
					pecaTempo.intervaloDeslocarVertical = 500;
					pecaTempo.intervaloGirar = 250;
					pecaTempo.intervaloDeslocarVerticalManual = 50;
					pecaTempo.intervaloPodeDescer = 500;
					pecaTempo.podeDescer = true;
					break;
			}
		}


		/// <summary>
		/// Esta função define o tempo que cada movimento deve ser realizado.
		/// Por exemplo, deslocar pra esquerda ou direita, ou deslocar pra baixo.
		/// Este método foi criado, pois a função update é chamada milésimos de segundos.
		/// Ou seja, antes de completar 1 segundo, a função pode ser chamada várias vezes.
		/// Se não controlasse, os deslocamento conforme o tempo, a tecla moveria da esquerda
		/// ou pra direita muito rapidamente.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void VerificarTempo(GameTime gameTime)
		{
			// GameTime tempoAtual = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

			var tempoDecorrido = gameTime.TotalGameTime.TotalMilliseconds;

			if (tempoDecorrido - pecaTempo.tempoDecorridoHorizontal > pecaTempo.intervaloDeslocarHorizontal)
			{
				pecaTempo.podeDeslocarHorizontal = true;
				pecaTempo.tempoDecorridoHorizontal = tempoDecorrido;
			}

			if (tempoDecorrido - pecaTempo.tempoDecorridoVertical > pecaTempo.intervaloDeslocarVertical)
			{
				pecaTempo.podeDeslocarVertical = true;
				pecaTempo.tempoDecorridoVertical = tempoDecorrido;
			}

			if (tempoDecorrido - pecaTempo.tempoDecorridoGirar > pecaTempo.intervaloGirar)
			{
				pecaTempo.podeGirarPeca = true;
				pecaTempo.tempoDecorridoGirar = tempoDecorrido;
			}
			if (tempoDecorrido - pecaTempo.tempoDecorridoPodeDescer > pecaTempo.intervaloPodeDescer)
			{
				pecaTempo.tempoDecorridoPodeDescer = gameTime.TotalGameTime.TotalMilliseconds;
			}

		}

		public void VerificarTeclas(GameTime gameTime)
		{
			VerificarTempo(gameTime);

			if (Keyboard.GetState().IsKeyDown(Keys.Left))
			{
				// Só move a tecla a cada 1 segundo.
				if (pecaTempo.podeDeslocarHorizontal == true)
				{
					tetrisPeca.MoverPraEsquerda();
					pecaTempo.podeDeslocarHorizontal = false;

					pecaTempo.tempoDecorridoHorizontal = gameTime.TotalGameTime.TotalMilliseconds;
				}
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
			{
				if (pecaTempo.podeDeslocarHorizontal == true)
				{
					tetrisPeca.MoverPraDireita();
					pecaTempo.podeDeslocarHorizontal = false;

					pecaTempo.tempoDecorridoHorizontal = gameTime.TotalGameTime.TotalMilliseconds;
				}
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Down))
			{
				if (gameTime.TotalGameTime.TotalMilliseconds - pecaTempo.tempoDecorridoVerticalManual > pecaTempo.intervaloDeslocarVerticalManual)
				{
					if (pecaTempo.podeDescer == false)
					{
						return;
					}
					tetrisPeca.MoverPraBaixo();
					pecaTempo.tempoDecorridoVerticalManual = gameTime.TotalGameTime.TotalMilliseconds;
				}
			
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Up))
			{
				if (pecaTempo.podeGirarPeca)
				{
					tetrisPeca.GirarPeca();
					pecaTempo.tempoDecorridoGirar = gameTime.TotalGameTime.TotalMilliseconds;
					pecaTempo.podeGirarPeca = false;
				}
			}



			if (Keyboard.GetState().IsKeyDown(Keys.Space))
			{
				if (gameTime.TotalGameTime.TotalMilliseconds - pecaTempo.tempoDecorridoPodeDescer > pecaTempo.intervaloPodeDescer)
				{
					pecaTempo.podeDescer = !pecaTempo.podeDescer;
					pecaTempo.tempoDecorridoPodeDescer = gameTime.TotalGameTime.TotalMilliseconds;
				}
			}



			GameTime tempo = gameTime;


			System.Diagnostics.Debug.Print("TotalMilisegundos: " + tempo.TotalGameTime.TotalMilliseconds.ToString());

			// Desloca a peça pra baixo automaticamente.
			if (pecaTempo.podeDeslocarVertical == true && pecaTempo.podeDescer)
			{
				tetrisPeca.MoverPraBaixo();
				pecaTempo.podeDeslocarVertical = false;
			}

			// pecaTempo.podeGirarPeca = false;
			// pecaTempo.podeDeslocarVertical = false;
			// pecaTempo.podeDeslocarHorizontal = false;
		}


		public void CriarTabuleiro()
		{
			celulasTabuleiro = new TetrisCelula[CELULAS_POR_LINHA, CELULAS_POR_COLUNA];

			int x, y;

			x = xCantoSuperiorEsquerdo;
			y = yCantoSuperiorEsquerdo;
			for (var uLinha = 0; uLinha < CELULAS_POR_LINHA; uLinha++)
			{
				x = xCantoSuperiorEsquerdo;
				for (var uColuna = 0; uColuna < CELULAS_POR_COLUNA; uColuna++)
				{
					celulasTabuleiro[uLinha, uColuna] = new TetrisCelula(x, y, CELULA_LARGURA, CELULA_ALTURA, graphicsDevice, spriteFont);
					x = x + CELULA_LARGURA + ESPACO_HORIZONTAL_ENTRE_CELULAS;
				}
				y = y + CELULA_ALTURA + ESPACO_VERTICAL_ENTRE_CELULAS;
			}
		}

		public void DesenharTabuleiro(GameTime gameTime, SpriteBatch spriteBatch)
		{
			for (var uLinha = 0; uLinha < CELULAS_POR_LINHA; uLinha++)
			{
				for (var uColuna = 0; uColuna < CELULAS_POR_COLUNA; uColuna++)
				{
					celulasTabuleiro[uLinha, uColuna].Draw(gameTime, spriteBatch, uLinha, uColuna);
				}
			}
		}


	}
}
