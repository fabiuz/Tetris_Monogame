using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tetris
{
	public static class TetrisCelulaCor
	{
		private static Random geradorAleatorio = new Random();

		static List<Color> lista_cores = new List<Color>
		{

			Color.Black,
			Color.Blue,
			Color.BlueViolet,
			Color.Chocolate,
			Color.Cyan,
			Color.DarkBlue,
			Color.Yellow,
			Color.YellowGreen,

		};

		static public Color NovaCor()
		{
			var indice = geradorAleatorio.Next(0, lista_cores.Count);
			return lista_cores[indice];
		}
	}
}
