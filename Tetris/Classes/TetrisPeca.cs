using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace Tetris
{
	public class TetrisPeca
	{
		/// <summary>
		/// No jogo Tetris, existirá várias peças de diversos tamanhos,
		/// então, criei um arranjo multidimensional de int.
		/// Neste arranjo, o valor de cada elemento do arranjo terá 
		/// o valor 1 ou o valor 0.
		/// A peça é localizada em uma posição especifica do tabuleiro,
		/// toda vez que a peça se move, é chamado um método que atualiza
		/// a célula do tabuleiro, cada célula da peça corresponde a uma
		/// única posição no tabuleiro, se a célula da peça tem o valor
		/// 1, então a célula é marcada que está ocupada.
		/// Assim, quando a função de renderizada é chamada, a célula 
		/// que está indicando que está ocupada, é renderizada na tela
		/// com a cor específica que foi definida.
		/// </summary>
		private static List<int[,]> lista_pecas;

		/// <summary>
		/// Cada célula do tabuleiro está localizada em uma posição específica
		/// do tabuleiro, então deve-se armazenar qual posição atual está no
		/// tabuleiro.
		/// Esta variável corresponde a posição x esquerda da peça no tabuleiro.
		/// </summary>
		int xEsquerdaTabuleiro;

		/// <summary>
		/// Cada célula do tabuleiro está localizada em uma posição específica
		/// do tabuleiro, então deve-se armazenar qual posição atual está no
		/// tabuleiro.
		/// Esta variável corresponde a posição x direita da peça no tabuleiro.
		/// </summary>
		int xDireitaTabuleiro;

		/// <summary>
		/// Cada célula do tabuleiro está localizada em uma posição específica
		/// do tabuleiro, então deve-se armazenar qual posição atual está no
		/// tabuleiro.
		/// Esta variável corresponde a posição y superior da peça no tabuleiro.
		/// </summary>
		int ySuperiorTabuleiro;

		/// <summary>
		/// Cada célula do tabuleiro está localizada em uma posição específica
		/// do tabuleiro, então deve-se armazenar qual posição atual está no
		/// tabuleiro.
		/// Esta variável corresponde a posição y inferior da peça no tabuleiro.
		/// </summary>
		int yInferiorTabuleiro;

		/// <summary>
		/// Esta variável é utlizada em loops for, pra saber o último índice da coluna
		/// da peça que está sendo analisada no momento.
		/// </summary>
		int pecaUltimaColuna;

		/// <summary>
		/// Esta variável é utlizada em loops for, pra saber o último índice da linha
		/// da peça que está sendo analisada no momento.
		/// </summary>
		int pecaUltimaLinha;

		/// <summary>
		/// Quantidade de colunas na peça.
		/// </summary>
		int pecaLargura;

		/// <summary>
		/// Quantidade de linhas na peça.
		/// </summary>
		int pecaAltura;

		/// <summary>
		/// Quantidade de linhas na peça.
		/// </summary>
		int pecaTotalDeLinhas;

		/// <summary>
		/// Quantidade de colunas na peça.
		/// </summary>
		int pecaTotalDeColunas;


		/// <summary>
		/// A peça é um conjunto de células, aqui, é definida a 
		/// cor de todas as células que fazem parte da mesma peça.
		/// </summary>
		Color corPeca;

		/// <summary>
		/// A peça é um conjunto de células, entretanto não precisamos
		/// armazenar a própria célula na classe peça, pois, a lógica
		/// do nosso jogo Tetris, é utilizar um arranjo 
		/// </summary>
		int[,] peca;

		Color tabuleiroCorFundo;

		/// <summary>
		/// Indica o último índice da coluna do
		/// tabuleiro onde esta peça está localizada.
		/// </summary>
		int tabuleiroUltimaColuna;

		/// <summary>
		/// Indica o último índice da linha do tabuleiro
		/// onde esta peça está localizada.
		/// </summary>
		int tabuleiroUltimaLinha;


		int tabuleiroTotalDeLinhas;
		int tabuleiroTotalDeColunas;


		/// <summary>
		/// Tabuleiro onde esta peça armazenada.
		/// </summary>
		Tabuleiro tabuleiro;

		/// <summary>
		/// Um tabuleiro é composto de um arranjo de TetrisCelulas.
		/// Cada célula armazena informações referentes a posição da mesma dentro do tabuleiro.
		/// </summary>
		TetrisCelula[,] celulaTabuleiro;

		static TetrisPeca()
		{

			lista_pecas = new List<int[,]>
			{
				new int[,]
				{
					{1, 1, 0},
					{0, 1, 1}
				},
				new int[,]
				{
					{0, 1, 1},
					{1, 1, 0},
				},
				new int[,]
				{
					{1, 1, 1},
					{0, 1, 0},
				},
				new int[,]
				{
					{0, 1},
					{0, 1},
					{1, 1},
				},
				new int[,]
				{
					{1, 0},
					{1, 0},
					{1, 1},
				},
				new int[,]
				{
					{1},
					{1},
					{1}
				},
				new int[,]
				{
					{1, 1},
					{1, 1},
				}
			};

		}

		/// <summary>
		/// Instancia uma nova peça pra ser utilizada no tabuleiro, aqui, estamos passando
		/// a instância das classes Tabuleiro e TetrisCelulas, pois precisamos passar
		/// informações da peça para o tabuleiro.
		/// </summary>
		/// <param name="tabuleiro">Tabuleiro.</param>
		/// <param name="celulaTabuleiro">Celula tabuleiro.</param>
		public TetrisPeca(Tabuleiro tabuleiro, ref TetrisCelula[,] celulaTabuleiro)
		{
			this.tabuleiro = tabuleiro;
			this.celulaTabuleiro = celulaTabuleiro;
			this.tabuleiroCorFundo = tabuleiro.TABULEIRO_COR_FUNDO;

			this.tabuleiroUltimaLinha = celulaTabuleiro.GetUpperBound(0);
			this.tabuleiroUltimaColuna = celulaTabuleiro.GetUpperBound(1);
		}

		public void SortearPeca()
		{
			Random geradorAleatorio = new Random();
			int indicePeca = geradorAleatorio.Next(0, lista_pecas.Count);

			pecaUltimaLinha = lista_pecas[indicePeca].GetUpperBound(0);
			pecaUltimaColuna = lista_pecas[indicePeca].GetUpperBound(1);

			pecaAltura = lista_pecas[indicePeca].GetLength(0);
			pecaLargura = lista_pecas[indicePeca].GetLength(1);

			pecaTotalDeLinhas = pecaAltura;
			pecaTotalDeColunas = pecaLargura;

			// Cria um novo arranjo baseado nas dimensões do arranjo selecionado.
			peca = new int[pecaTotalDeLinhas, pecaTotalDeColunas];

			// Preenche o arranjo.
			for (var linha = 0; linha <= pecaUltimaLinha; linha++)
			{
				for (var coluna = 0; coluna <= pecaUltimaColuna; coluna++)
				{
					peca[linha, coluna] = lista_pecas[indicePeca][linha, coluna];
				}
			}

			//AjustarCantos();
			GerarPosicaoDaPecaNoTabuleiro();

			DefinirNovaCorDaPeca();

			VerificarLinhasCompletas();


			// Verificas se há colisão, se sim, quer dizer que o jogo acabou.
			if (VerificarColisao())
			{
				tabuleiro.tetrisEstado = TetrisEstado.TETRIS_NOVO_JOGO;
			}
			else
			{
				GravarPecaNoTabuleiro();
			}
		}

		public void DefinirNovaCorDaPeca()
		{
			corPeca = TetrisCelulaCor.NovaCor();

			var linhaTabuleiro = ySuperiorTabuleiro;
			var colunaTabuleiro = xEsquerdaTabuleiro;

			for (var linha = 0; linha <= pecaUltimaLinha; linha++)
			{
				colunaTabuleiro = xEsquerdaTabuleiro;
				for (var coluna = 0; coluna <= pecaUltimaColuna; coluna++)
				{
					if (peca[linha, coluna] == 1)
					{
						celulaTabuleiro[linhaTabuleiro, colunaTabuleiro].celulaCor = corPeca;
					}
					colunaTabuleiro++;
				}
				linhaTabuleiro++;
			}
		}

		/// <summary>
		/// Reseta as variáveis 
		/// xEsquerdaTabuleiro, xDireitaTabuleiro, ySuperiorTabuleiro, yInferiorTabuleiro
		/// ao ser criado uma nova peça no tabuleiro. 
		/// </summary>
		public void GerarPosicaoDaPecaNoTabuleiro()
		{
			// Centraliza a peça horizontalmente.
			xEsquerdaTabuleiro = (tabuleiro.CELULAS_POR_COLUNA - pecaLargura) / 2;

			// Aqui, devemos decrementar 1 para localizar o lado direito da peça no tabuleiro,
			// pois, se assim naõ o fizermos, estaremos apontando para um índice após
			// o índice do lado direito, veja, o exemplo, abaixo:
			// conforme o exemplo acima, se as células são 0, 1, 2 e 3,
			// então, se começarmos em 0, e a largura é 4, se somarmos, o 
			// índice apontará para o índice 4, ao invés de 3, por isso, devemos
			// decrementar o valor 1.
			xDireitaTabuleiro = xEsquerdaTabuleiro + pecaLargura - 1;

			// Começa a peça no topo
			ySuperiorTabuleiro = 0;
			yInferiorTabuleiro = ySuperiorTabuleiro + pecaAltura - 1;
		}

		private void VerificarLinhasCompletas()
		{
			var linha = tabuleiroUltimaLinha;
			var ultimaLinhaCompleta = -1;
			var ultimaLinhaNaoCompleta = -1;

			bool haLinhasVazias = false;

			while (linha > 0)
			{
				while (linha > 0)
				{
					bool linhaCompleta = true;
					bool linhaVazia = false;

					for (var coluna = 0; coluna <= tabuleiroUltimaColuna; coluna++)
					{
						if (celulaTabuleiro[linha, coluna].celulaEstaOcupada == false)
						{
							linhaCompleta = false;
						}
					}

					// Se a linha tá completa devemos apagar.
					if (linhaCompleta == true)
					{
						for (var coluna = 0; coluna <= tabuleiroUltimaColuna; coluna++)
						{
							celulaTabuleiro[linha, coluna].celulaEstaOcupada = false;
						}

						// Se a variável tem o valor -1, quer dizer, que ainda não foi encontrado
						// nenhuma linha completa.
						// Ao acharmos a primeira linha completa não pode deixar esta variável
						// ser definida novamente, se encontrarmos uma nova linha completa
						// pois, se houver várias completas completas sucessivas, ao ser copiado
						// um linha não vazia pra uma linha completa, somente a única linha 
						// completa sucessiva terá o valores copiados e a outras linhas estarão vazias.
						if (ultimaLinhaCompleta == -1)
						{
							ultimaLinhaCompleta = linha;
						}
					}
					else
					{
						// Se a linha não está completa devemos verificar, se há ultima linha completa
						if (linha < ultimaLinhaCompleta && ultimaLinhaCompleta != -1 )  
						{
							for (var coluna = 0; coluna <= tabuleiroUltimaColuna; coluna++)
							{
								celulaTabuleiro[ultimaLinhaCompleta, coluna].AlterarCelulaCor(celulaTabuleiro[linha, coluna].celulaCor);
								celulaTabuleiro[ultimaLinhaCompleta, coluna].celulaEstaOcupada = celulaTabuleiro[linha, coluna].celulaEstaOcupada;

								celulaTabuleiro[linha, coluna].celulaEstaOcupada = false;
								celulaTabuleiro[linha, coluna].AlterarCelulaCor(tabuleiro.TABULEIRO_COR_FUNDO);
								celulaTabuleiro[linha, coluna].AlterarCelulaCorFundo(tabuleiro.TABULEIRO_COR_FUNDO);
							}
							ultimaLinhaCompleta--;
						}
					}
					linha--;
				}

			}
		}

		public void MoverPraEsquerda()
		{
			if (xEsquerdaTabuleiro == 0)
				return;


			// Sempre iremos mover uma célula por vez.
			// Então, devemos apagar a peça atual pra que
			// ao analisar a nova posição parte da peça 
			// na nova posição pode colidir com a peça da posição anterior.
			ApagarPecaNoTabuleiro();

			xEsquerdaTabuleiro--;
			xDireitaTabuleiro--;

			if (VerificarColisao())
			{
				xEsquerdaTabuleiro++;
				xDireitaTabuleiro++;
				GravarPecaNoTabuleiro();
			}
			else
			{
				// Não houve colisão, simplesmente, definir peça na nova posição
				// não precisamos apagar a posição anterior pois isto foi feito
				// ao iniciar o jogo.
				GravarPecaNoTabuleiro();
			}

		}

		public void MoverPraDireita()
		{
			// Evitar que a peça se movça pra fora dos limites da tela.
			if (xDireitaTabuleiro >= celulaTabuleiro.GetUpperBound(1))
			{
				return;
			}

			// Sempre iremos mover uma célula por vez.
			// Então, devemos apagar a peça atual pra que
			// ao analisar a nova posição parte da peça 
			// na nova posição pode colidir com a peça da posição anterior.
			ApagarPecaNoTabuleiro();

			xEsquerdaTabuleiro++;
			xDireitaTabuleiro++;

			if (VerificarColisao())
			{
				xEsquerdaTabuleiro--;
				xDireitaTabuleiro--;
				GravarPecaNoTabuleiro();
			}
			else
			{
				// Não houve colisão, simplesmente, definir peça na nova posição
				// não precisamos apagar a posição anterior pois isto foi feito
				// ao iniciar o jogo.
				GravarPecaNoTabuleiro();
			}
		}

		/// <summary>
		/// Move a peça pra baixo, verifica por colisão, se houver colisão,
		/// retorna a peça pra posição anterior e sortea uma nova peça.
		/// </summary>
		public void MoverPraBaixo()
		{
			// Devemos apagar a peça do tabuleiro, pois ela pode se sobrescrever com a nova posição.
			ApagarPecaNoTabuleiro();

			yInferiorTabuleiro++;
			ySuperiorTabuleiro++;

			if (yInferiorTabuleiro > celulaTabuleiro.GetUpperBound(0))
			{
				yInferiorTabuleiro--;
				ySuperiorTabuleiro--;
				GravarPecaNoTabuleiro();
				SortearPeca();
				return;
			}


			// Se houve colisão, retornar a peça pra posição anterior.
			// Sortear nova peça
			if (VerificarColisao())
			{
				yInferiorTabuleiro--;
				ySuperiorTabuleiro--;
				GravarPecaNoTabuleiro();
				SortearPeca();
			}
			else
			{
				// Não houve colisão na nova posição, então, devemos retornar pra posição
				// anterior e apagar a peça desta posição.
				// yInferiorTabuleiro--;
				// ySuperiorTabuleiro--;

				// ApagarPecaNoTabuleiro();
				GravarPecaNoTabuleiro();

			}
		}


		public void GirarPeca()
		{
			// Pra girar a peça, se ela não tiver a mesma quantidade de linhas e colunas
			// inverter a quantidade de linhas por colunas.
			int[,] pecaTemp = new int[pecaTotalDeColunas, pecaTotalDeLinhas];

			int ultimaLinha = pecaTemp.GetUpperBound(0);
			int ultimaColuna = pecaTemp.GetUpperBound(1);


			for (var linha = 0; linha <= pecaUltimaLinha; linha++)
			{
				for (var coluna = 0; coluna <= pecaUltimaColuna; coluna++)
				{
					// A primeira linha vai pra última coluna
					// A segunda linha vai pra penúltima coluna
					// e assim por diante...
					// Desta forma:
					// Origem: Linha->0, coluna->0	Destino: Linha->0, coluna->3
					// Origem: Linha->0, coluna->1	Destino: Linha->1, coluna->3
					//
					// 	Antes:
					// [0, 1, 2 ]
					// [3, 4, 5 ]
					// [6, 7, 8 ]
					//
					// Depois:
					// [6, 3, 0 ]
					// [7, 4, 1 ]
					// [8, 5, 2 ]
					pecaTemp[coluna, pecaUltimaLinha - linha] = peca[linha, coluna];;

				}
			}

			// Se a largura e altura da peça original são do mesmo tamanho,
			// a peça continuará na mesma posição, entretanto, se a altura e a largura 
			// são diferentes, então as coordenadas serão alteradas, entretanto,
			// devemos fazer com que a peça seja centralizada o máxim possível no centro.

			// Vamos precisar também da coordenada da peça relativo ao tabuleiro.
			int xEsquerdaTabuleiroTemp = xEsquerdaTabuleiro;
			int xDireitaTabuleiroTemp = xDireitaTabuleiro;
			int ySuperiorTabuleiroTemp = ySuperiorTabuleiro;
			int yInferiorTabuleiroTemp = yInferiorTabuleiro;

			int ladoMaior = pecaLargura;
			int ladoMenor = pecaAltura;

			if (pecaLargura > pecaAltura)
			{
				ladoMaior = pecaLargura;
				ladoMenor = pecaAltura;
			}
			else if(pecaLargura < pecaAltura)
			{
				ladoMaior = pecaAltura;
				ladoMenor = pecaLargura;
			}

			// Pra centralizar, devemos pegar a maior parte menos a menor parte, o resultado
			// desta subtração teremos o ponto onde o índice deve começar.
			int indiceInicial = (int)Math.Floor((double)((ladoMaior - ladoMenor) / 2));

			// Como o índice é baseado em zero, devemos reduzir em 1.
			indiceInicial = (indiceInicial > 0) ? indiceInicial-- : 0;

			// Conforme testes que realize o algoritmo é este.
			// Se a largura é maior que a altura, 
			// o eixo x desloca pra direita e o eixo y desloca pra cima.
			// Se a largura é menor que a altura,
			// o eixo x desloca pra esquerda e o eixo y desloca pra baixo,
			// Ou seja, se girarmos a peça ela sempre girará conforme o centro.
			if (pecaLargura > pecaAltura)
			{
				xEsquerdaTabuleiroTemp += indiceInicial;
				ySuperiorTabuleiroTemp -= indiceInicial;	
			}
			else if (pecaLargura < pecaAltura)
			{
				xEsquerdaTabuleiroTemp -= indiceInicial;
				ySuperiorTabuleiroTemp += indiceInicial;
			}

			// Devemos calcular a nova posição do lado direito e inferior da peça pois, provavelmente a forma
			// alterou.
			// Neste caso, a 'pecaLargura' armazena a largura da peça original, este valor será a nova altura
			// da peça girada e a 'pecaAltura' armazena a altura da peça original, este valor será a nova 
			// largura da peça girada.
			// De posse destes dados, podemos determinar com precisão o lado direito e inferior da peça.
			xDireitaTabuleiroTemp = xEsquerdaTabuleiroTemp + pecaAltura - 1;
			yInferiorTabuleiroTemp = ySuperiorTabuleiroTemp + pecaLargura - 1;

			// Agora, vamos apagar a posição atual.
			ApagarPecaNoTabuleiro();

			// Garantir que a peça está dentro do tabuleiro.
			while (xEsquerdaTabuleiroTemp < 0)
			{
				xEsquerdaTabuleiroTemp++;
				xDireitaTabuleiroTemp++;
			}
			while (xDireitaTabuleiroTemp > celulaTabuleiro.GetUpperBound(1))
			{
				xEsquerdaTabuleiroTemp--;
				xDireitaTabuleiroTemp--;
			}
			while (yInferiorTabuleiroTemp > celulaTabuleiro.GetUpperBound(0))
			{
				yInferiorTabuleiroTemp--;
				ySuperiorTabuleiroTemp--;
			}
			while (ySuperiorTabuleiroTemp > celulaTabuleiro.GetUpperBound(0))
			{
				yInferiorTabuleiroTemp--;
				ySuperiorTabuleiroTemp--;
			}

			// Agora, iremos verificar por colisão com a nova peça.
			var colunaTabuleiro = xEsquerdaTabuleiroTemp;
			var linhaTabuleiro = ySuperiorTabuleiroTemp;
			bool bColisao = false;

			var ultimaLinhaPecaTemp = pecaTemp.GetUpperBound(0);
			var ultimaColunaPecaTemp = pecaTemp.GetUpperBound(1);

			for (var linha = 0; linha <= ultimaLinhaPecaTemp; linha++)
			{
				colunaTabuleiro = xEsquerdaTabuleiroTemp;
				for (var coluna = 0; coluna <= ultimaColunaPecaTemp; coluna++)
				{
					if (pecaTemp[linha, coluna] == 1 && celulaTabuleiro[linhaTabuleiro, colunaTabuleiro].celulaEstaOcupada == true)
					{
						bColisao = true;
						break;
					}
					colunaTabuleiro++;
				}
				if (bColisao)
				{
					break;
				}
				linhaTabuleiro++;
			}

			// Se houve colisão, regravar posição.
			if (bColisao)
			{
				GravarPecaNoTabuleiro();
			}
			else
			{
				peca = null;
				peca = pecaTemp;

				pecaUltimaLinha = peca.GetUpperBound(0);
				pecaUltimaColuna = peca.GetUpperBound(1);
				pecaTotalDeLinhas = peca.GetLength(0);
				pecaTotalDeColunas = peca.GetLength(1);
				pecaLargura = pecaTotalDeColunas;
				pecaAltura = pecaTotalDeLinhas;

				xEsquerdaTabuleiro = xEsquerdaTabuleiroTemp;
				xDireitaTabuleiro = xDireitaTabuleiroTemp;
				ySuperiorTabuleiro = ySuperiorTabuleiroTemp;
				yInferiorTabuleiro = yInferiorTabuleiroTemp;

				GravarPecaNoTabuleiro();
			}
		}

		/// <summary>
		/// Esta informação grava a peça no tabuleiro em uma nova posição.
		/// </summary>
		private void GravarPecaNoTabuleiro()
		{
			var colunaTabuleiro = xEsquerdaTabuleiro;
			var linhaTabuleiro = ySuperiorTabuleiro;

			for (var uLinha = 0; uLinha <= pecaUltimaLinha; uLinha++)
			{
				colunaTabuleiro = xEsquerdaTabuleiro;
				for (var uColuna = 0; uColuna <= pecaUltimaColuna; uColuna++)
				{
					// Ao sortear uma nova peça, ela estará inicialmente fora do tabuleiro por isso,
					// linhaTabuleiro, pode estar com um número negativo, devemos evitar renderizar tal célula.
					if (peca[uLinha, uColuna] == 1 && linhaTabuleiro >= 0)
					{
						celulaTabuleiro[linhaTabuleiro, colunaTabuleiro].celulaEstaOcupada = true;
						celulaTabuleiro[linhaTabuleiro, colunaTabuleiro].AlterarCelulaCor(corPeca);
					}
					colunaTabuleiro++;
				}
				linhaTabuleiro++;
			}			
		}


		/// <summary>
		/// Apaga a peça na posição atual.
		/// Esta função somente será chamada se realmente a posição deve ser
		/// apagada, pois, por exemplo, a peça está na posição A e irá pra posição
		/// B, a posição A, será apagada somente se for possível deslocar a peça
		/// pra posição B.
		/// </summary>
		private void ApagarPecaNoTabuleiro()
		{
			var colunaTabuleiro = xEsquerdaTabuleiro;
			var linhaTabuleiro = ySuperiorTabuleiro;


			for (var uLinha = 0; uLinha <= pecaUltimaLinha; uLinha++)
			{
				colunaTabuleiro = xEsquerdaTabuleiro;
				for (var uColuna = 0; uColuna <= pecaUltimaColuna; uColuna++)
				{
					// Só apaga se a uLinha é não-vazio.
					if (linhaTabuleiro >= 0)
					{
						if (peca[uLinha, uColuna] == 1)
						{
							celulaTabuleiro[linhaTabuleiro, colunaTabuleiro].celulaEstaOcupada = false;
							celulaTabuleiro[linhaTabuleiro, colunaTabuleiro].AlterarCelulaCorFundo(Color.White);
						}
					}
					colunaTabuleiro++;
				}
				linhaTabuleiro++;
			}
		}

		/// <summary>
		/// Retorna true, se houve colisão
		/// </summary>
		/// <returns><c>true</c>, if colisao was verificared, <c>false</c> otherwise.</returns>
		private bool VerificarColisao()
		{
			// Vamos analisar da esquerda pra direita, então precisamos
			// de uma variável que percorrer a coluna da esquerda pra direita.
			var colunaTabuleiro = xEsquerdaTabuleiro;

			// Iremos começar da linha inferior em direção a linha superior.
			var linhaTabuleiro = ySuperiorTabuleiro;

			// Vamos verificar por colisão
			for (var linha = 0; linha <= pecaUltimaLinha; linha++)
			{
				colunaTabuleiro = xEsquerdaTabuleiro;
				for (var coluna = 0; coluna <= pecaUltimaColuna; coluna++)
				{
					if (linhaTabuleiro >= 0)
					{
						if (peca[linha, coluna] == 1 && celulaTabuleiro[linhaTabuleiro, colunaTabuleiro].celulaEstaOcupada == true)
						{
							return true;
						}
					}
					colunaTabuleiro++;
				}
				// Estamos analisando de baixo pra cima.
				linhaTabuleiro++;
			}

			return false;
		}

	}
}
