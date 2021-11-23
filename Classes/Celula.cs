using System;

using Microsoft.Xna.Framework;

namespace Tetris
{

	public class Celula
	{
		private float _xEsquerda;
		private float _xMeio;
		private float _xDireita;

		private float _ySuperior;
		private float _yMeio;
		private float _yInferior;

		/// <summary>
		/// Largura da célula.
		/// </summary>
		private int largura;

		/// <summary>
		/// Altura da célula.
		/// </summary>
		private int altura;

		/// <summary>
		/// Metade da largura da célula
		/// </summary>
		private int larguraMetade;

		/// <summary>
		/// Metade da altura da célula.
		/// </summary>
		private int alturaMetade;


		virtual protected float Esquerda
		{
			get
			{
				return _xEsquerda;
			}
			set
			{
				// Se o usuário da classe, definir o valor da esquerda, posteriormente,
				// iremos analisar se a célula moveu pra esquerda ou pra direita.
				var deslocamento = value - _xEsquerda;
				switch (Math.Sign(deslocamento))
				{
					case -1:
						MoverPraEsquerda(Math.Abs(deslocamento));
						break;
					case 1:
						MoverPraDireita(Math.Abs(deslocamento));
						break;
				}
			}
		}

		protected float Direita
		{
			get
			{
				return _xDireita;
			}
			set
			{
				var deslocamento = value - _xDireita;

				switch (Math.Sign(deslocamento))
				{
					case -1:
						MoverPraEsquerda(Math.Abs(deslocamento));
						break;
					case 1:
						MoverPraDireita(Math.Abs(deslocamento));
						break;
				}				
			}
		}

		protected float Superior
		{
			get
			{
				return _ySuperior;
			}
			set
			{
				var deslocamento = value - _ySuperior;

				switch (Math.Sign(deslocamento))
				{
					case -1:
						MoverPraCima(Math.Abs(deslocamento));
						break;

					case 1:
						MoverPraBaixo(Math.Abs(deslocamento));
						break;
				}
			}
		}

		protected float Inferior
		{
			get
			{
				return _yInferior;
			}
			set
			{
				var deslocamento = value - _yInferior;

				switch (Math.Sign(deslocamento))
				{
					case -1:
						MoverPraCima(Math.Abs(deslocamento));
						break;

					case 1:
						MoverPraBaixo(Math.Abs(deslocamento));
						break;
				}
			}
		}




		Vector2 cantoSuperiorEsquerdo;
		Vector2 cantoSuperiorDireito;
		Vector2 cantoInferiorEsquerdo;
		Vector2 cantoInferiorDireito;
		Vector2 pontoCentral;

		Vector2 pontoOrigem;
		Vector2 pontoDestino;

		/// <summary>
		/// Indica o ponto fixo que corresponde ao centro da célula
		/// onde a célula tem sua posição definida ao ser criada.
		/// </summary>
		public readonly Vector2 PontoFixo;

		public Vector2 PontoOrigem { get { return pontoOrigem; } }
		public Vector2 PontoDestino { get { return pontoDestino; } }


		protected int Largura
		{
			get
			{
				return largura;
			}
		}

		protected int Altura
		{
			get { return altura; }
		}

		public Celula(int x, int y, int largura, int altura)
		{

			if (largura <= 0)
			{
				throw new ArgumentException("Largura não pode ser menor que zero");
			}
			if (altura <= 0)
			{
				throw new ArgumentException("Altura não pode ser menor que zero");
			}
			if (x < 0)
			{
				throw new ArgumentException("Argumento 'x' não pode ser menor que zero");
			}
			if (y < 0)
			{
				throw new ArgumentException("Argumento 'y' não pode ser menor que zero");
			}

			this.largura = largura;
			this.altura = altura;
			larguraMetade = largura / 2;
			alturaMetade = altura / 2;

			_xEsquerda = x;
			_xMeio = x + larguraMetade;
			_xDireita = x + largura;

			_ySuperior = y;
			_yMeio = y + alturaMetade;
			_yInferior = y + altura;

			pontoOrigem = new Vector2(_xMeio, _yMeio);
			pontoDestino = new Vector2(_xMeio, _xMeio);
			pontoCentral = new Vector2(_xMeio, _yMeio);

			cantoSuperiorEsquerdo = new Vector2(_xEsquerda, _ySuperior);
			cantoSuperiorDireito = new Vector2(_xDireita, _ySuperior);
			cantoInferiorEsquerdo = new Vector2(_xEsquerda, _yInferior);
			cantoInferiorDireito = new Vector2(_xDireita, _yInferior);

			RecalcularCelula();

			this.PontoFixo = pontoOrigem;
		}

		/// <summary>
		/// Altera as dimensões da célula.
		/// </summary>
		/// <param name="largura">Largura da célula, largura deve ser maior que zero.</param>
		/// <param name="altura">Altura da célula, altura deve ser maior que zero.</param>
		public void AlterarTamanho(int largura, int altura)
		{
			if (largura <= 0)
			{
				throw new ArgumentException("Largura não pode ser menor que zero");
			}
			if (altura <= 0)
			{
				throw new ArgumentException("Altura não pode ser menor que zero");
			}

			this.largura = largura;
			this.altura = altura;

			RecalcularCelula();
		}

		/// <summary>
		/// Altera a largura da célula.
		/// </summary>
		/// <param name="largura">Largura da célula, largura deve ser maior que zero.</param>
		public void AlterarLargura(int largura)
		{
			if (largura <= 0)
			{
				throw new ArgumentException("Largura não pode ser menor que zero");
			}

			this.largura = largura;
			RecalcularCelula();
		}

		/// <summary>
		/// Altera a altura da célula.
		/// </summary>
		/// <param name="altura">Altura da célula, altura deve ser maior que zero.</param>
		public void AlterarAltura(int altura)
		{
			if (altura <= 0)
			{
				throw new ArgumentException("Altura não pode ser menor que zero");
			}

			this.altura = altura;
			RecalcularCelula();
		}

		/// <summary>
		/// Move a célula para a esquerda.
		/// </summary>
		/// <param name="deslocamento">Deslocamento.</param>
		public void MoverPraEsquerda(float deslocamento)
		{
			_xEsquerda -= deslocamento;

			// Devemos recalcular os valores de x.
			_xMeio = _xEsquerda + larguraMetade;
			_xDireita = _xEsquerda + largura;

			RecalcularCelula();
		}

		/// <summary>
		/// Move a célula para a direita.
		/// </summary>
		/// <param name="deslocamento">Deslocamento.</param>
		public void MoverPraDireita(float deslocamento)
		{
			_xEsquerda += deslocamento;

			// Devemos recalcular os valores de x.
			_xMeio = _xEsquerda + larguraMetade;
			_xDireita = _xEsquerda + largura;

			RecalcularCelula();
		}

		/// <summary>
		/// Move a célula para cima.
		/// </summary>
		/// <param name="deslocamento">Deslocamento.</param>
		public void MoverPraCima(float deslocamento)
		{
			this._ySuperior -= deslocamento;

			// Devemos recalcular os valores de y.
			_yMeio = _ySuperior + alturaMetade;
			_yInferior = _ySuperior + altura;

			RecalcularCelula();
		}

		/// <summary>
		/// Move a célula para baixo.
		/// </summary>
		/// <param name="deslocamento">Deslocamento.</param>
		public void MoverPraBaixo(float deslocamento)
		{
			this._ySuperior += deslocamento;

			// Devemos recalcular os valores de y.
			_yMeio = _ySuperior + alturaMetade;
			_yInferior = _ySuperior + altura;

			RecalcularCelula();
		}

		/// <summary>
		/// Move o ponto central da célula.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void MoverCentro(float x, float y)
		{
			_xMeio = x;
			_yMeio = y;

			_xEsquerda = _xMeio - larguraMetade;
			_xDireita = _xMeio + larguraMetade;

			_ySuperior = _yMeio - alturaMetade;
			_yInferior = _yMeio + alturaMetade;

			RecalcularCelula();
		}

		/// <summary>
		/// Move o ponto central da célula.
		/// </summary>
		/// <param name="xyCentro">Xy centro.</param>
		public void MoverCentro(Vector2 xyCentro)
		{
			MoverCentro(xyCentro.X, xyCentro.Y);
		}

		/// <summary>
		/// Se a posição da célula ou dimensões são alteradas, devemos recalculá-la.
		/// </summary>
		private void RecalcularCelula()
		{
			larguraMetade = largura / 2;
			alturaMetade = altura / 2;

			// _xEsquerda = x;
			_xMeio = _xEsquerda + larguraMetade;
			_xDireita = _xEsquerda + largura;

			// _ySuperior = y;
			_yMeio = _ySuperior + alturaMetade;
			_yInferior = _ySuperior + altura;

			pontoOrigem = new Vector2(_xMeio, _yMeio);
			pontoDestino = new Vector2(_xMeio, _xMeio);
			pontoCentral = new Vector2(_xMeio, _yMeio);

			cantoSuperiorEsquerdo = new Vector2(_xEsquerda, _ySuperior);
			cantoSuperiorDireito = new Vector2(_xDireita, _ySuperior);
			cantoInferiorEsquerdo = new Vector2(_xEsquerda, _yInferior);
			cantoInferiorDireito = new Vector2(_xDireita, _yInferior);
		}
	}
}
