using UnityEngine;

public class MelonDestroy : MonoBehaviour
{
    Blade blade;
    MelonPoolManager mp;


    private void Start()
    {
        blade = FindObjectOfType<Blade>();
        mp = MelonPoolManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            MissedMelonCountingMechanic();

            PointSystemRestrictions();

            Disable();
        }

        else if (collision.tag == "Blade")
        {
            mp.LastFruitCutTime = Time.time;

            InstantiateSlicedMelon(collision);

            InstantiateSplatFX(collision);

            mp.SlicedMelonCounter++;
            BonusPointMechanic();

            Disable();
        }
    }

    #region SlicedMelon and FXs Instantiations
    private void InstantiateSlicedMelon(Collider2D collision)
    {
        GameObject melonSliced = Instantiate(mp.melonSlicedPrefab, transform.position, Quaternion.identity, mp.slicedMelonContainer.transform);
        melonSliced.transform.rotation = Rotation(collision, melonSliced);
        Destroy(melonSliced, 2f);
    }

    //rotation so that sliced melon faces the point at which it was cut
    private Quaternion Rotation(Collider2D other, GameObject melonSliced)
    {
        Vector2 direction = blade.melonSlicingDirection;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion rot = melonSliced.transform.rotation = Quaternion.Lerp(other.transform.rotation, rotation, 60f/*check back later*/ * Time.deltaTime);
        return rot;
    }

    private void InstantiateSplatFX(Collider2D collision)
    {
        GameObject splatEFX = Instantiate(mp.slicingFXPrefab, collision.transform.position, Quaternion.identity);
        Destroy(splatEFX, 1f);
    }

    #endregion

    private void PointSystemRestrictions()
    {
        //to stop score from going into negative
        //and stop it from deducting after level was cleared
        if (mp.SlicedMelonCounter > 0 && mp.SlicedMelonCounter < MelonPoolManager.ammountOfPointsToWin)
        {
            mp.SlicedMelonCounter--;
        }

        //once level is cleared, score equals the maximum reachable points
        if (mp.SlicedMelonCounter > MelonPoolManager.ammountOfPointsToWin)
        {
            mp.SlicedMelonCounter = MelonPoolManager.ammountOfPointsToWin;
        }
    }

    private void MissedMelonCountingMechanic()
    {
        //count missed melons before the level is cleared and canvas is active in hierarchy
        if (!UIManager.Instance.winCanvas.activeInHierarchy)
        {
            mp.MissedMelonCounter++;
        }
    }

    private void BonusPointMechanic()
    {
        if (mp.BonusPoint > 0)
        {
            mp.SlicedMelonCounter += mp.BonusPoint;
        }
    }


    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

}
