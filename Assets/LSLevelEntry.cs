using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSLevelEntry : MonoBehaviour
{
    public bool canLoadLevel;
    public string levelName, levelToCheck, displayName;

    public GameObject mapPointActive, mapPointInactive;

    private bool _levelUnlocked;

    private bool levelLoading;

    private void Start()
    {
        if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1 || levelToCheck == "")
        {
            mapPointActive.SetActive(true);
            mapPointInactive.SetActive(false);
            _levelUnlocked = true;
        }
        else
        {
            mapPointActive.SetActive(false);
            mapPointInactive.SetActive(true);
            _levelUnlocked = false;
        }

        if(PlayerPrefs.GetString("CurrentLevel") == levelName)
        {
            PlayerController.instance.transform.position = transform.position;
            LSResetPlayer.instance.respawnPosition = transform.position;
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump") && canLoadLevel && _levelUnlocked && !levelLoading)
        {
            StartCoroutine("LevelLoadWaiter");
            levelLoading = true;


        }

    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            canLoadLevel = true;

            LSUIManager.instance.lnamePanel.SetActive(true);
            LSUIManager.instance.lnameText.text = displayName;

            if(PlayerPrefs.HasKey(levelName + "_coins"))
            {
                LSUIManager.instance.coinsText.text = PlayerPrefs.GetInt(levelName+"_coins").ToString();
            }
            else
            {
                LSUIManager.instance.coinsText.text = "???";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canLoadLevel = false;

            LSUIManager.instance.lnamePanel.SetActive(false);
        }
    }

    public IEnumerator LevelLoadWaiter()
    {
        PlayerController.instance.stopMove = true;
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(levelName);
        PlayerPrefs.SetString("CurrentLevel", levelName);
    }

}
