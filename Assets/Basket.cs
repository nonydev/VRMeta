using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
	private HashSet<Collider> interactingColliders = new HashSet<Collider>();
	public void OnTriggerEnter(Collider other)
	{
		var rb = other.GetComponentInParent<Rigidbody>();
		if (rb != null && rb.CompareTag("Ingredient"))
		{
			PizzaMaker.Instance.AddIngredient(other.name);
			interactingColliders.Add(other);
			print("ADD" + other.name);
		}
	}

	public void OnTriggerExit(Collider other)
	{
		var rb = other.GetComponentInParent<Rigidbody>();

		if (rb != null && rb.CompareTag("Ingredient"))
		{
			PizzaMaker.Instance.RemoveIngredient(other.name);
			interactingColliders.Remove(other);
			print("REMOVE" + other.name);
		}
	}


	public void Lock()
	{
		foreach (var c in interactingColliders)
		{
			var rb = c.GetComponentInParent<Rigidbody>();
			rb.isKinematic = true;
			rb.transform.SetParent(transform);
		}
	}

	public void Unlock()
	{
		foreach (var c in interactingColliders)
		{
			var rb = c.GetComponentInParent<Rigidbody>();
			rb.isKinematic = false;
			rb.transform.SetParent(null);
		}
	}
}
