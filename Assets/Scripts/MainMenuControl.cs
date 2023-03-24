using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public static MainMenuControl instance;

    [Header("Components")]
    [SerializeField] UpgradeUI upgradeUI;
    [SerializeField] MainButtons mainButtons;
    [SerializeField] SpriteRenderer planet;
    [SerializeField] MainMenuPlayer player;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        CloseUpgradeUI();
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowUpgradeUI()
    {
        upgradeUI.gameObject.SetActive(true);
        mainButtons.gameObject.SetActive(false);
        planet.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.gameObject.SetActive(false);
        mainButtons.gameObject.SetActive(true);
        planet.gameObject.SetActive(true);
        player.gameObject.SetActive(true);

        player.transform.position = new Vector3(-0.6f, 19.9f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MainMenuPlayer player))
        {
            // start
            if (mainButtons.index == 0)
                startGame();
            // bag
            else if (mainButtons.index == 1)
                ShowUpgradeUI();
            // exit
            else if (mainButtons.index == 2)
                Application.Quit();
        }
    }

}
