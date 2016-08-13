using UnityEngine;
using System.Collections;

public class OnlyActiveInEditor : MonoBehaviour 
{
	protected void Awake()
	{
		Destroy(this.gameObject);
	}
}