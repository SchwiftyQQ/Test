using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Intro text to each level")]
    [SerializeField] Text startingMessage;

    [Header("scoreText on Canvas/ scoreTextCopy on WinCanvas")]
    [SerializeField] Text scoreText, scoreTextCopy;

    [Header("Text for Melons missed by the player")]
    [SerializeField] TextMeshProUGUI missedMelonCounterText;

    [Header("Score counter to be disabled cuz WIN CANVAS has a copy")]
    public GameObject melonCounterToBeDisabledOnLvLClear;

    [Header("Level Cleared overlay screen")]
    public GameObject winCanvas;
    MelonPoolManager mp;

    // Start is called before the first frame update
    void Start()
    {
        mp = MelonPoolManager.Instance;

        StartCoroutine(IntroTextForEachLevel());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        //score counted during the game
        scoreText.text = mp.SlicedMelonCounter.ToString();

        //updating this in background to display it on WinCanvas after level clear
        scoreTextCopy.text = mp.SlicedMelonCounter.ToString();
    }

    public void LevelClearedScreen()
    {
        Debug.Log("You Won!");
        winCanvas.SetActive(true);
        missedMelonCounterText.text = mp.MissedMelonCounter.ToString();
    }

    public IEnumerator IntroTextForEachLevel()
    {
        //Displays 2 messages each taking half of "startTime" variable

        startingMessage.text = "Reach " + MelonPoolManager.ammountOfPointsToWin.ToString() + " points to clear the level";
        yield return new WaitForSeconds(mp.startTime / 2);
        startingMessage.text = "1 Melon = 1 point - be fast and get 2 points";
        yield return new WaitForSeconds(mp.startTime / 2);
        startingMessage.text = "";
    }

}
