using UnityEngine;

public class Character : MonoBehaviour
{
    public static GameObject Self { get; set; }

    protected void Awake()
    {
        Self = gameObject;
    }
}