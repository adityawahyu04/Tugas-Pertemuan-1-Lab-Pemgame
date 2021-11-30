using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    //VARIABEL
    public Image currentEnergy;
    public Text time;
    [SerializeField] GameObject pauseMenu;
    public static bool GameIsPaused = false;
    public Player playerInstance;

    private float energy = 200;
    private float maxEnergy = 200;
    private float kecepatan;
    private float kecepatanLari;
    private float input_x;
    private float input_z;

    //HUD darah
    private float darah;
    private float maxDarah = 100f;
    public Image currentDarah;

    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject information;
    string info;
    

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        kecepatanLari = player.GetComponent<movement_player>().speed_lari;

        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        kecepatan = player.GetComponent<movement_player>().kecepatan;
        input_x = player.GetComponent<movement_player>().x;
        input_z = player.GetComponent<movement_player>().z;
        darah = player.GetComponent<sistem_darah>().darah_player;
        info = player.GetComponent<sistem_darah>().info;

        Text pesan = information.GetComponent<Text>();
        pesan.text = info;

        EnergyDrain();
        UpdateEnergy();
        UpdateTime();
        ShowPauseMenu();
        UpdateDarah();
        gameOver();

    }

    private void EnergyDrain()
    {
        if(kecepatan == kecepatanLari)
        {
            if(input_x > 0 || input_z > 0 )
            {
                if(energy > 0)
                {
                    energy -= 10 * Time.deltaTime;
                }
                
            }
        }
        else{
            if(energy < maxEnergy)
            {
                 energy += 15 * Time.deltaTime;
            }
           
        }

        

        
    }

    private void UpdateEnergy()
    {
        float ratio = energy / maxEnergy;
        currentEnergy.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }

    private void UpdateTime()
    {
        int hours = EnviroSky.instance.GameTime.Hours;
        int minutes = EnviroSky.instance.GameTime.Minutes;
        string gameHours;
        string gameMinutes;

        if(hours >= 0 && hours < 10)
        {
            gameHours = "0" + hours.ToString();

        }
        else
        {
            gameHours = hours.ToString();
        }

        if(minutes >= 0 && minutes < 10)
        {
            gameMinutes = "0" + minutes.ToString();

        }
        else
        {
            gameMinutes = minutes.ToString();
        }

        time.text = gameHours+ " : " + gameMinutes;

        
    }

    private void ShowPauseMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(playerInstance);
    }

    private void UpdateDarah()
    {
        float ratio = darah / maxDarah;
        currentDarah.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }

    public void gameOver()
    {
        if(darah < 1)
        {
            //player mati
            GameOverMenu.SetActive(true);
            GameIsPaused = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void restart()
    {
        //sama seperti merubah scene
        SceneManager.LoadScene("SampleScene");
    }

}