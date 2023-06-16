using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image enemyHealthBar;
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private Image shieldBar;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;

    [SerializeField] private CinemachineVirtualCamera InitialCamera;
    [SerializeField] private CinemachineBrain mainCamera;
    [SerializeField] public CinemachineBlenderSettings newBlend;

    [SerializeField] private GameObject EndGameCanvas;

    private EnemyInteractions enemyInteractions;
    private PlayerInteractions playerInteractions;

    private bool isGameOver = false;

    private float timer = 3;
    // Start is called before the first frame update
    void Start()
    {
        enemyInteractions = enemy.GetComponent<EnemyInteractions>();
        playerInteractions = player.GetComponent<PlayerInteractions>();
        InitialCamera.Priority = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            mainCamera.m_CustomBlends = null;
        }
        enemyHealthBar.fillAmount = enemyInteractions.GetCurrentHealth()/enemyInteractions.GetHealth();
        playerHealthBar.fillAmount = playerInteractions.GetCurrentHealth()/ playerInteractions.GetHealth();

        if (!isGameOver)
        {
            CheckPlayerHealth();
            CheckEnemyHealth();
            CheckShield();
        }
        if (isGameOver)
        {
            Debug.Log("gameover");
            player.GetComponent<StarterAssetsInputs>().cursorLocked = false;
            player.GetComponent<StarterAssetsInputs>().cursorInputForLook = false;
        }
    }

    private void CheckPlayerHealth()
    {
        if (playerInteractions.GetCurrentHealth() <= 0)
        {
            Time.timeScale = 0;
            EndGameCanvas.SetActive(true);
            //Lost screen
            Transform message = EndGameCanvas.transform.Find("Message");
            message.GetComponent<TextMeshProUGUI>().SetText("You Died!");
            isGameOver = true;
        }
        
    }
    private void CheckEnemyHealth()
    {
        if (enemyInteractions.GetCurrentHealth() <= 0)
        {
            Time.timeScale = 0;
            EndGameCanvas.SetActive(true);
            //WonScreen
            Transform message = EndGameCanvas.transform.Find("Message");
            message.GetComponent<TextMeshProUGUI>().SetText("You Won!");
            isGameOver = true;
        }
    }
    private void CheckShield()
    {
        shieldBar.fillAmount = player.GetComponent<PlayerSpecialities>().GetShieldTimer();
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
