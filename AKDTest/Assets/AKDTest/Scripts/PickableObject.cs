using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PickableObject : MonoBehaviour
{
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer(Constants.PickUpLayerName);
    }
}

