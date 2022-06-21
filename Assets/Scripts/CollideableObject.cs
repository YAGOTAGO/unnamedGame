using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideableObject : MonoBehaviour
{
    private Collider2D Collider;
    [SerializeField]
    private ContactFilter2D Filter;
    private List<Collider2D> ColliderObjects= new List<Collider2D>(1);

    private void Start()
    {
        Collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Collider.OverlapCollider(Filter, ColliderObjects);

        foreach(var t in ColliderObjects)
        {
            Debug.Log("collided with" + t.name);
        }
    }
}
