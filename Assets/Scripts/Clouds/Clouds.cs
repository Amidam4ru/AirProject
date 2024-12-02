using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bigClouds;

    private void Awake()
    {
        foreach (GameObject clouds in _bigClouds)
        {
            OffCloudes(clouds.transform);
        }
    }

    private void OffCloudes(Transform transform)
    {
        foreach (Transform child in transform)
        {
            SpriteRenderer sprite = child.GetComponent<SpriteRenderer>();
            sprite.enabled = false;
        }
    }
}
