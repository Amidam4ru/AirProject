using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CloudVisibility : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Cloud cloud))
        {
            SpriteRenderer sprite = collision.transform.GetComponent<SpriteRenderer>();
            sprite.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Cloud cloud))
        {
            SpriteRenderer sprite = collision.transform.GetComponent<SpriteRenderer>();

            sprite.enabled = false;
        }
    }
}
