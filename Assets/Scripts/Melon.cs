using UnityEngine;

public class Melon : MonoBehaviour
{
    Rigidbody2D rb;
    MelonPoolManager mp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mp = MelonPoolManager.Instance;
    }


    private void OnEnable()
    {
        rb.AddForce(transform.up * MelonPoolManager.melonSpeed, ForceMode2D.Impulse);
    }

}
