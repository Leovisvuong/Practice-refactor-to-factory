using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int timeRespawnCountDown;
    private float timePlay;
    public int gameLevel;
    private bool canStartCountDown;
    public int life;
    public int star;
    public int maxUndead;
    public int maxHPUp;
    public bool gameStart;
    private int levelButtonPressed = 1;
    private float[,] achievementTable = {{-1,-1,-1},{-1,-1,-1}};
    
    public GameObject MainMenu;
    private CameraMovement cameraMovement;
    public GameObject Setting;
    public GameObject LevelSetting;
    public GameObject SoundSetting;
    public GameObject Achievement;
    public GameObject ResultOfAchievement;
    public GameObject GameStop;
    public GameObject Information;
    public GameObject DoneLevel;
    public GameObject Lose;
    public GameObject DefenseIcon;
    public Button startButton;
    public Button settingButton;
    public Button settingOKButton;
    public Button soundSettingButton;
    public Button levelSettingButton;
    public Button achievementButton;
    public Button ExitButton;
    public Button levelSettingOKButton;
    public Scrollbar levelScrollbar;
    public Button soundSettingOKButton;
    public Scrollbar soundScrollbar;
    public Button continueButton;
    public Button backToMenuButton;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI starText;
    public Button returnMenuButton;
    public Button LV3Button;
    public Button LV2Button;
    public Button LV1Button;
    public Button OKAchievementButton;
    public TextMeshProUGUI achievementTimeText;
    public TextMeshProUGUI achievementStarText;
    public TextMeshProUGUI doneTimeText;
    public TextMeshProUGUI doneStarText;
    public Button doneBackToMenuButton;
    public TextMeshProUGUI loseTimeText;
    public TextMeshProUGUI loseStarText;
    public Button loseBackToMenuButton;
    public Button EscapeButton;
    public TextMeshProUGUI DefenseCountDownText;
    private GameManage gameManage;
    private PathGenerator pathGeneratorScript;
    PlayerMovement playerMovement;
    public AudioSource backgroundSound;
    public AudioSource jump1Sound;
    public AudioSource jump2Sound;
    public AudioSource jump3Sound;
    public AudioSource getStarSound;
    public AudioSource winSound;
    public AudioSource clickSound;
    public AudioSource loseSound;
    public AudioSource getPowerUpSound;
    public AudioSource hurtSound;
    public AudioSource turnSound;
    public bool CheckMaxValue(string maxValueName){
        if(maxValueName == "HPUp"){
            if(maxHPUp < 1) return false;
            maxHPUp--;
            return true;
        }
        if(maxUndead < 1) return false;
        maxUndead--;
        return true;
    }
    public void PlusValue(string valueName){
        if(valueName == "star") star++;
        else life++;
    }
    public void MinusLife(){
        if(life == 0){
            life--;
            DoLose();
            return;
        }
        life--;
        playerMovement.Respawn(true);
    }
    private void SetVolume(){
        backgroundSound.volume = soundScrollbar.value;
        jump1Sound.volume = soundScrollbar.value;
        jump2Sound.volume = soundScrollbar.value;
        jump3Sound.volume = soundScrollbar.value;
        getStarSound.volume = soundScrollbar.value;
        winSound.volume = soundScrollbar.value;
        loseSound.volume = soundScrollbar.value;
        clickSound.volume = soundScrollbar.value;
        getPowerUpSound.volume = soundScrollbar.value;
        hurtSound.volume = soundScrollbar.value;
        turnSound.volume = soundScrollbar.value;
    }
    private void SetLevel(){
        gameLevel = 3;
        if(levelScrollbar.value <= 0.25f){
            gameLevel = 1;
        }
        else if(levelScrollbar.value < 0.75f){
            gameLevel = 2;
        }
        pathGeneratorScript.SetGameLevel(gameLevel);
    }
    void Start(){
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
        GameObject pathGenerator = GameObject.Find("PathGenerator");
        GameObject Camera = GameObject.FindWithTag("MainCamera");
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        pathGeneratorScript = pathGenerator.GetComponent<PathGenerator>();
        cameraMovement = Camera.GetComponent<CameraMovement>();

        levelScrollbar.value = PlayerPrefs.GetFloat("level value");
        if(!PlayerPrefs.HasKey("sound value")) PlayerPrefs.SetFloat("sound value", 0.25f);
        soundScrollbar.value = PlayerPrefs.GetFloat("sound value");
        SetVolume();
        SetLevel();
        for(int i = 1; i < 4; i++){
            string keyNameTime = "best time lv" + i.ToString();
            string keyNameStar = "star lv" + i.ToString();
            achievementTable[0,i-1] = PlayerPrefs.GetFloat(keyNameTime);
            achievementTable[1,i-1] = PlayerPrefs.GetFloat(keyNameStar);
        }
        MainMenu.SetActive(true);
        startButton.onClick.AddListener(PressedStartButton);
        settingButton.onClick.AddListener(PressedSettingButton);
        settingOKButton.onClick.AddListener(PressedSettingOKButton);
        levelSettingButton.onClick.AddListener(PressedLevelSettingButton);
        soundSettingButton.onClick.AddListener(PressedSoundSettingButton);
        levelSettingOKButton.onClick.AddListener(PressedLevelSettingOKButton);
        soundSettingOKButton.onClick.AddListener(PressedSoundSettingOKButton);
        continueButton.onClick.AddListener(PressedContinueButton);
        backToMenuButton.onClick.AddListener(PressedBackToMenuButton);
        achievementButton.onClick.AddListener(PressedAchievementButton);
        returnMenuButton.onClick.AddListener(PressedReturnedMenuButton);
        LV1Button.onClick.AddListener(PressedLV1Button);
        LV2Button.onClick.AddListener(PressedLV2Button);
        LV3Button.onClick.AddListener(PressedLV3Button);
        OKAchievementButton.onClick.AddListener(PressedOKAchievementButton);
        doneBackToMenuButton.onClick.AddListener(PressedDoneBackToMenuButton);
        loseBackToMenuButton.onClick.AddListener(PressedLoseBackToMenuButton);
        EscapeButton.onClick.AddListener(PressedEscape);
        ExitButton.onClick.AddListener(PressedExit);
    }
    void Update(){
        achievementTimeText.text = "best time: " + ((achievementTable[0,levelButtonPressed-1] > 0) ? achievementTable[0,levelButtonPressed-1].ToString("0.0") : "invalid");
        achievementStarText.text = "best stars: " + achievementTable[1,levelButtonPressed-1] + "/3";
        if(gameStart){
            if(playerMovement.isUndead) DefenseIcon.SetActive(true);
            else DefenseIcon.SetActive(false);
            DefenseCountDownText.text = playerMovement.undeadTime.ToString();
            if(canStartCountDown){
                StartCoroutine(CountDownTimer());
                canStartCountDown = false;
            }
            timeText.text = "time: " + timePlay.ToString("0.0");
            lifeText.text = "life: " + life;
            starText.text = "stars: " + star + "/3";
        }
        
        if(Input.GetKeyDown(KeyCode.Escape) && !GameStop.activeSelf && gameStart) PressedEscape();
        if(SoundSetting.activeSelf) SetVolume();
    }
    void PressedSettingButton(){
        clickSound.Play();
        MainMenu.SetActive(false);
        Setting.SetActive(true);
    }
    void PressedSettingOKButton(){
        clickSound.Play();
        MainMenu.SetActive(true);
        Setting.SetActive(false);
    }
    void PressedLevelSettingButton(){
        clickSound.Play();
        Setting.SetActive(false);
        LevelSetting.SetActive(true);
    }
    void PressedSoundSettingButton(){
        clickSound.Play();
        Setting.SetActive(false);
        SoundSetting.SetActive(true);
    }
    void PressedLevelSettingOKButton(){
        clickSound.Play();
        Setting.SetActive(true);
        LevelSetting.SetActive(false);
        SetLevel();
    }
    void PressedSoundSettingOKButton(){
        clickSound.Play();
        Setting.SetActive(true);
        SoundSetting.SetActive(false);
        SetVolume();
    }
    void PressedStartButton(){
        clickSound.Play();
        MainMenu.SetActive(false);
        Information.SetActive(true);
        gameManage.SetGameStart(true);
        cameraMovement.SetGameMenu(false);
        gameStart = canStartCountDown = true;
        timePlay = 0;
        star = 0;
        life = 3;
        maxUndead = 1;
        maxHPUp = 1;
        gameManage.SetIsOnFinishLine(false);
    }
    void PressedContinueButton(){
        clickSound.Play();
        GameStop.SetActive(false);
        gameManage.SetGameStop(false);
        gameStart = canStartCountDown = true;
    }
    void PressedEscape(){
        clickSound.Play();
        if(DoneLevel.activeSelf || Lose.activeSelf) return;
        GameStop.SetActive(true);
        gameManage.SetGameStop(true);
        gameStart = false;
    }
    void PressedBackToMenuButton(){
        clickSound.Play();
        GameStop.SetActive(false);
        Information.SetActive(false);
        cameraMovement.SetGameMenu(true);
        gameManage.SetGameStop(false);
        gameManage.SetGameStart(false);
        MainMenu.SetActive(true);
    }
    void PressedAchievementButton(){
        clickSound.Play();
        MainMenu.SetActive(false);
        Achievement.SetActive(true);
    }
    void PressedReturnedMenuButton(){
        clickSound.Play();
        MainMenu.SetActive(true);
        Achievement.SetActive(false);
    }
    void PressedLV1Button(){
        clickSound.Play();
        levelButtonPressed = 1;
        Achievement.SetActive(false);
        ResultOfAchievement.SetActive(true);
    }
    void PressedLV2Button(){
        clickSound.Play();
        levelButtonPressed = 2;
        Achievement.SetActive(false);
        ResultOfAchievement.SetActive(true);
    }
    void PressedLV3Button(){
        clickSound.Play();
        levelButtonPressed = 3;
        Achievement.SetActive(false);
        ResultOfAchievement.SetActive(true);
    }
    void PressedOKAchievementButton(){
        clickSound.Play();
        ResultOfAchievement.SetActive(false);
        Achievement.SetActive(true);
    }
    public void DoDoneLevel(){
        DoneLevel.SetActive(true);
        gameManage.SetGameStop(true);
        gameStart = false;
        doneTimeText.text = "Time: " + timePlay.ToString("0.0");
        doneStarText.text = "Stars: " + star + "/3";
        achievementTable[0,gameLevel-1] = (achievementTable[0,gameLevel-1] == 0) ? timePlay : Math.Min(timePlay,achievementTable[0,gameLevel-1]);
        achievementTable[1,gameLevel-1] = (achievementTable[1,gameLevel-1] == 0) ? star : Math.Max(star,achievementTable[1,gameLevel-1]);
    }
    void PressedDoneBackToMenuButton(){
        clickSound.Play();
        DoneLevel.SetActive(false);
        Information.SetActive(false);
        cameraMovement.SetGameMenu(true);
        gameManage.SetGameStop(false);
        gameManage.SetGameStart(false);
        MainMenu.SetActive(true);
        playerMovement.ResetOnFinishFloor();
    }
    void DoLose(){
        loseSound.Play();
        Lose.SetActive(true);
        gameManage.SetGameStop(true);
        gameStart = false;
        loseTimeText.text = "Time: " + timePlay.ToString("0.0");
        loseStarText.text = "Stars: " + star + "/3";
    }
    void PressedLoseBackToMenuButton(){
        clickSound.Play();
        Lose.SetActive(false);
        Information.SetActive(false);
        cameraMovement.SetGameMenu(true);
        gameManage.SetGameStop(false);
        gameManage.SetGameStart(false);
        MainMenu.SetActive(true);
    }
    void PressedExit(){
        PlayerPrefs.SetFloat("level value",levelScrollbar.value);
        PlayerPrefs.SetFloat("sound value",soundScrollbar.value);
        for(int i = 1; i < 4; i++){
            string keyNameTime = "best time lv" + i.ToString();
            string keyNameStar = "star lv" + i.ToString();
            PlayerPrefs.SetFloat(keyNameTime,achievementTable[0,i-1]);
            PlayerPrefs.SetFloat(keyNameStar,achievementTable[1,i-1]);
        }
        Application.Quit();
    }
    IEnumerator CountDownTimer(){
        while(gameStart){
            yield return new WaitForSeconds(0.1f);
            timePlay += 0.1f;
        }
    }
}
