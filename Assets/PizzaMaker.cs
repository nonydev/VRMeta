using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PizzaMaker : MonoBehaviour, IPunObservable
{
	public Text IngredientText;

	private PhotonView view;
	private Pizza[] pizzas;
	public List<string> Ingredients = new List<string>();
	private int bestIndex = -1;
	private int BestIndex
	{
		get
		{
			return bestIndex;
		}

		set
		{
			bestIndex = value;
			if (bestIndex != -1)
			{
				DisplayBestPizza();
			}
		}
	}

	public static PizzaMaker Instance;

	private void Awake()
	{
		Instance = this;
		pizzas = GetComponentsInChildren<Pizza>(true);
		view = GetComponent<PhotonView>();
	}

	public void OnTriggerEnter(Collider other)
	{
		if (view.IsMine == false)
		{
			return;
		}

		if (other.CompareTag("Ingredient"))
		{
			Ingredients.Add(other.name);
		}

		Refresh();
	}

	public void AddIngredient(string name)
	{
		Ingredients.Add(name);

		Refresh();
	}

	public void RemoveIngredient(string name)
	{
		Ingredients.Remove(name);

		Refresh();
	}

	private void Refresh()
	{
		if (view.IsMine == false)
		{
			return;
		}

		float max = -1000;
		Pizza best = null;
		bestIndex = -1;
		int index = 0;
		foreach (var pizza in pizzas)
		{
			float score = pizza.GetScore(Ingredients);
			if (score > max)
			{
				bestIndex = index;
				best = pizza;
				max = score;
			}
			index++;
		}

		DisplayBestPizza();
	}

	private void DisplayBestPizza()
	{
		foreach (var pizza in pizzas)
		{
			pizza.gameObject.SetActive(false);
		}

		pizzas[BestIndex].gameObject.SetActive(true);
		string ingList = "Ingredients\n";
		foreach (var ing in pizzas[BestIndex].Ingredients)
		{
			ingList += ing + "\n";
		}
		ingList = ingList.Substring(0, ingList.Length - 1);
		IngredientText.text = ingList;
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(BestIndex);
		}
		else
		{
			BestIndex = (int)stream.ReceiveNext();
		}
	}
}
