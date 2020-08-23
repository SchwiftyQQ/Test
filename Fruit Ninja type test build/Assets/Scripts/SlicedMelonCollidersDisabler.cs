using UnityEngine;

public class SlicedMelonCollidersDisabler : MonoBehaviour
{
    BoxCollider2D[] bc2D;

    // Start is called before the first frame update
    void Start()
    {
        bc2D = GetComponentsInChildren<BoxCollider2D>();
        Invoke("DisableCollidersInChildren", 0.15f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void DisableCollidersInChildren()
    {
        foreach (var collider in bc2D)
        {
            collider.enabled = false;
        }
    }
}
