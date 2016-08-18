using UnityEngine;

public class OnlyActiveInEditor : MonoBehaviour 
{
	protected void Awake()
	{
        gameObject.SetActive(false);
	}
}