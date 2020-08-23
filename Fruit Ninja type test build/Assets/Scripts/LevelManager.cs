using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    MelonPoolManager mp;

    [Header("numbers representing levels for difficulty increase. default 100, 200, 300...")]
    public List<int> milestones; //milestones to check if we should change certain variables


    private void Start()
    {
        mp = MelonPoolManager.Instance;
    }

    public void LoadNextLevelButton()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        MelonPoolManager.ammountOfPointsToWin += 10; //every "LoadNextLevel" points needed to win go up 10

        CheckForMilestones();

        // reload the scene with new variables/conditions
        SceneManager.LoadScene(currentSceneName);
    }

    private void CheckForMilestones()
    {
        foreach (var milestone in milestones)
        {
            //to check on 100, 200, 300.. points and increase melon spawn rate by 0.1f
            //and melon flying speed by 0.3f;
            if (milestone == MelonPoolManager.ammountOfPointsToWin)
            {
                mp.repeatRate -= 0.1f;
                MelonPoolManager.melonSpeed += 0.3f;
            }
            //and when spawn rate reaches 0.3f, make sure we don't get any lower
            else if (mp.repeatRate <= 0.3f)
            {
                mp.repeatRate = 0.3f;
            }
            //and when melon speed reaches 15.2f make sure we don't go any higher
            else if (MelonPoolManager.melonSpeed > 15.2f)
            {
                MelonPoolManager.melonSpeed = 15.2f;
            }
        }
    }

    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
