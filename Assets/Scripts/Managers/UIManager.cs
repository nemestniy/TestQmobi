using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public event Action GreetingScreenWasShown;

    public Text Score;
    public GameObject Greeting;
    public float TimeToShowGreeting;
    public GameObject Lives;

    public GameObject LoseText;
    public GameObject RestartButton;

    private List<RectTransform> livesItems;

    public void Initialize()
    {
        Greeting.SetActive(true);
        Score.gameObject.SetActive(true);
        Lives.SetActive(true);

        LoseText.SetActive(false);
        RestartButton.SetActive(false);
        InitializeLivesItems();
    }

    //Get one item from scene like sample and generate new item regarding of lives count from GameManager
    private void InitializeLivesItems()
    {
        var item = Lives.transform.GetChild(0).GetComponent<RectTransform>();
        var lives = GameManager.Instance.Lives;

        livesItems = new List<RectTransform>();
        livesItems.Add(item);

        for(int i = 0; i < lives - 1; i++)
        {
            var newItem = Instantiate(item.gameObject, Lives.transform).GetComponent<RectTransform>();
            newItem.position = item.position + Vector3.right * item.sizeDelta.x * (i+1);
            livesItems.Add(newItem);
        }
    }

    public void RemoveLivesItems()
    {
        if (livesItems.Count >= 1)
        {
            Destroy(livesItems[livesItems.Count - 1].gameObject);
            livesItems.Remove(livesItems[livesItems.Count - 1]);
        }
    }

    public void ShowLoseScreen()
    {
        LoseText.SetActive(true);
        RestartButton.SetActive(true);
    }

    public void OnClockRestartButton()
    {
        GameManager.Restart();
    }

    private void Update()
    {
        TimeToShowGreeting -= Time.deltaTime;
        if(TimeToShowGreeting <= 0 && Greeting.activeSelf)
        {
            GreetingScreenWasShown();
            Greeting.SetActive(false);
        }

        Score.text = "Score: " + GameManager.Score;
    }
}
