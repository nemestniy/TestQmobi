using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public static int Score { get; private set; }

    public Player PlayerPrefab;
    public int Lives;

    [Space()]

    public UIManager UIManager;
    public EnemyManager EnemyManager;
    public SafeBordersManager SafeBorders;
    public ObjectsHolder ObjectsHolder;

    private static string MainSceneName = "MainScene";

    //Initialization of UI and wait for hiding of greeting screen
    private void Start()
    {
        Score = 0;
        UIManager.Initialize();

        UIManager.GreetingScreenWasShown += Uimanager_GreetingScreenWasShown;
    }

    //Start game after greeting
    private void Uimanager_GreetingScreenWasShown()
    {
        StartGame();
        UIManager.GreetingScreenWasShown -= Uimanager_GreetingScreenWasShown;
    }

    //Initialization of player, enemyManager and ObjectsHolder with lin kon player
    private void StartGame()
    {
        InitializePlayer();

        ObjectsHolder.Initialize();
        EnemyManager.Initialize();
        SafeBorders.Initialize();
    }

    private void StopGame()
    {
        EnemyManager.StopEnemiesGeneration();
        UIManager.ShowLoseScreen();
    }

    private Player InitializePlayer()
    {
        var player = Instantiate(PlayerPrefab, Vector2.zero, Quaternion.identity);
        player.Initialize();
        return player;
    }

    public static void IncreaseScore(int points)
    {
        Score += points;
    }

    public static void Restart()
    {
        SceneManager.LoadScene(MainSceneName);
    }

    //After every losing before new player initialization remove all enemies
    public void KillPlayer()
    {
        Lives--;
        Destroy(ObjectsHolder.Player.gameObject);
        UIManager.RemoveLivesItems();

        if(Lives >= 1)
        {
            var asteroids = FindObjectsOfType<AsteroidController>();
            foreach (var item in asteroids)
                Destroy(item.gameObject);

            var UFOs = FindObjectsOfType<UFOController>();
            foreach (var item in UFOs)
                Destroy(item.gameObject);

            var player = InitializePlayer();
            ObjectsHolder.ChangePlayer(player);
        }
        else
        {
            StopGame();
        }
    }
}
