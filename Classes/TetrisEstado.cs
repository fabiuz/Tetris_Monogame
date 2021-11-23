using System;
namespace Tetris
{
	public enum TetrisEstado
	{
		/// <summary>
		/// The tetris novo jogo.
		/// </summary>
		TETRIS_NOVO_JOGO,

		/// <summary>
		/// Vai pra este estado pra configurar as peças e resetar o tabuleiro.
		/// </summary>
		TETRIS_ANTES_DE_COMECAR_NOVO_JOGO,

		/// <summary>
		/// Indica que o jogo está em execução.
		/// </summary>
		TETRIS_JOGANDO,
		TETRIS_PAUSE,
		TETRIS_GAME_OVER
	}
}
