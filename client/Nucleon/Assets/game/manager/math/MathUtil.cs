using UnityEngine;
using System.Collections;

namespace game.manager.math
{
	public class MathUtil
	{

		public static int getRandomNoOne(int min, int max , int noOne)
		{
			int target = noOne;


			while (target == noOne) {
				target = Mathf.FloorToInt(min + ( max - min + 1) * Random.value);
			}
			return target;
		}

	}
	
}