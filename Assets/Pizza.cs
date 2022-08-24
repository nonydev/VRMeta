using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
	public string[] Ingredients;

	public float GetScore(List<string> ings)
	{
		if (ings.Count == 0)
		{
			if (Ingredients.Length == 0)
			{
				return -1000;
			}

			return 0;
		}

		HashSet<string> set = new HashSet<string>(ings);
		float count = 0;
		foreach (string ing in Ingredients)
		{
			if (set.Contains(ing))
			{
				count++;
			}
			else
			{
				count--;
			}
		}

		return count;
	}
}
