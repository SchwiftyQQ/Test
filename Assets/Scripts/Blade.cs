using UnityEngine;

public class Blade : MonoBehaviour
{
    MelonPoolManager mp;

    [SerializeField] GameObject trail;
    GameObject currentTrail;

    Rigidbody2D rb;
    CircleCollider2D col;

    Vector2 previousPos;

    public float minVelocity;
    bool isCutting;

    public Vector2 melonSlicingDirection;

    // Start is called before the first frame update
    void Start()
    {
        mp = MelonPoolManager.Instance;

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdatePosition();
        }

    }

    private void FixedUpdate()
    {
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //this 2 lines are for melon slicing direction in MelonDestroy.cs
        Vector2 direction = (newPos - previousPos);
        melonSlicingDirection = direction;
    }


    void UpdatePosition()
    {
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPos;
        float velocity = (newPos - previousPos).magnitude * Time.deltaTime;

        if (velocity > minVelocity && mp.SlicedMelonCounter < MelonPoolManager.ammountOfPointsToWin)
        {
            col.enabled = true;
        }
        else if (velocity < minVelocity)
        {
            col.enabled = false;
        }

        previousPos = newPos;
    }

    void StartCutting()
    {
        isCutting = true;
        rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = rb.position;
        currentTrail = Instantiate(trail, transform);
    }

    void StopCutting()
    {
        isCutting = false;
        Destroy(currentTrail);
    }
}
