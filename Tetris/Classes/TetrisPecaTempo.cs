using System;
namespace Tetris
{
	/// <summary>
	/// Estrutura que serve pra armazenar o tempo decorrido e o intervalo
	/// que a peça pode-se mover.
	/// 
	/// </summary>
	public struct TetrisPecaTempo
	{
		/// <summary>
		/// Indica a última vez, tempo, que deslocou horizontalmente.
		/// </summary>
		public double tempoDecorridoHorizontal;

		/// <summary>
		/// Indica a última vez, tempo que deslocou verticalmente.
		/// </summary>
		public double tempoDecorridoVertical;

		/// <summary>
		/// Indica a última vez, tempo que o usuári clicou na seta pra descer a peça mais rápido.
		/// </summary>
		public double tempoDecorridoVerticalManual;


		/// <summary>
		/// Indica a última vez, tempo que a peça foi girada.
		/// </summary>
		public double tempoDecorridoGirar;

		/// <summary>
		/// A peça pode se locomover após decorrido o intervalo de tempo
		/// desde a última vez que a peça se deslocou.
		/// </summary>
		public double intervaloDeslocarHorizontal;

		/// <summary>
		/// A peça pode se locomver após decorrido o intervalo de tempo 
		/// desde a última vez que a peça se deslocou.
		/// </summary>
		public double intervaloDeslocarVertical;


		/// <summary>
		/// Se o usuário clica na seta pra baixo, devemos descer 1 segundo por vez.
		/// </summary>
		public double intervaloDeslocarVerticalManual;


		/// <summary>
		/// A peça pode ser girada somente após a o intervalo de tempo
		/// ter sido atingido desde a última vez que a peça girou.
		/// </summary>
		public double intervaloGirar;

		public double intervaloPodeDescer;
		public double tempoDecorridoPodeDescer;

		public bool podeDeslocarHorizontal;
		public bool podeDeslocarVertical;
		public bool podeGirarPeca;
		public bool podeDeslocarVerticalManual;

		public bool podeDescer;
	}
}
