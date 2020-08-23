using System.Collections.Generic;
using UnityEngine;

public class MelonPoolManager : MonoSingleton<MelonPoolManager>
{
    Blade blade;

    public GameObject melonPrefab;
    public GameObject melonSlicedPrefab; // Counldn't pool Sliced Melons cuz of some weird RigidBody2D complication...
    public GameObject slicingFXPrefab;

    [Header("Ammount of melons to pool together")]
    public int pooledAmmount;
    public List<GameObject> melonPool;

    [Header("Spawn Points (empty game objects)" +
    "Their GreenAxis is used for Melon direction")]
    [SerializeField] Transform[] spawnPoints;

    #region Container/Parent GameOjbects
    [Header("Empty Game Object for Melon parent")]
    public GameObject melonContainer;
    [Header("Empty Game Object for Sliced Melon parent")]
    public GameObject slicedMelonContainer;
    #endregion


    [Header("Time when melons start shooting")]
    public int startTime;
    [Header("Shooting frequency")]
    public float repeatRate;

    #region StaticVariables
    // Speed of mellons shooting
    public static float melonSpeed = 14f;

    // ammount of points needed to get to next level, added 10 on every "ReloadLevel" method in LevelLoader.cs
    public static int ammountOfPointsToWin = 10;
    #endregion

    #region Properties
    //used as main challenge/score
    public int SlicedMelonCounter { get; set; }
    public int MissedMelonCounter { get; set; }

    //time at which last melon was cut, used for bonus point mechanic
    public float LastFruitCutTime { get; set; }
    public int BonusPoint { get; set; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        blade = FindObjectOfType<Blade>();

        melonPool = new List<GameObject>();
        for (int i = 0; i < pooledAmmount; i++)
        {
            GameObject obj = Instantiate(melonPrefab, melonContainer.transform);
            obj.SetActive(false);
            melonPool.Add(obj);

        }

        InvokeRepeating("ShootMelons", startTime, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForBonusPointCondition();
        CheckForMaxPointsReached();
    }

    private void CheckForMaxPointsReached()
    {
        if (SlicedMelonCounter >= ammountOfPointsToWin && ammountOfPointsToWin <= 0)
        {
            Debug.LogError("AmmountOfPointsToWin not set, it's currently " + ammountOfPointsToWin);
        }
        else if (SlicedMelonCounter >= ammountOfPointsToWin)
        {
            UIManager.Instance.LevelClearedScreen();
            UIManager.Instance.melonCounterToBeDisabledOnLvLClear.SetActive(false);
            blade.GetComponent<CircleCollider2D>().enabled = false;

        }
    }

    private void CheckForBonusPointCondition()
    {
        //0.15 second window where one melon gives 2 points instead of 1
        if (Time.time > LastFruitCutTime + 0.15f)
        {
            BonusPoint = 0;
        }
        else
            BonusPoint = 1;
    }

    void ShootMelons()
    {
        for (int i = 0; i < melonPool.Count; i++)
        {
            if (!melonPool[i].activeInHierarchy)
            {
                int random = Random.Range(0, spawnPoints.Length);
                melonPool[i].transform.position = spawnPoints[random].position;
                melonPool[i].transform.rotation = spawnPoints[random].rotation;
                melonPool[i].SetActive(true);
                break;
            }
        }
    }

}
