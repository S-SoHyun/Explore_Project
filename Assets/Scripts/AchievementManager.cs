using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;


// 개별 업적
[System.Serializable]
public class Achievement
{
    public string Name;
    public string Description;
    public bool IsAchieved;

    public Achievement(string name, string description, bool isAchieved)
    {
        Name = name;
        Description = description;
        IsAchieved = isAchieved;
    }
}

// 개별 업적 담을 리스트
[System.Serializable]
public class AchievementData
{
    public List<Achievement> achievementData;
}


// 업적 로직
public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public List<Achievement> achievementData = new List<Achievement>();

    [SerializeField] private GameObject achievePanel;
    [SerializeField] private TextMeshProUGUI achieveTitleTxt;
    [SerializeField] private TextMeshProUGUI achieveDescTxt;
    // 업적별 아이콘?


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        string jsonPath = Application.persistentDataPath + "/AchievementData.txt";
        if (File.Exists(jsonPath))
        {
            LoadAchievementData();
            Debug.Log(Application.persistentDataPath);
        }
        else
        {
            AddAchievenments();
            SaveAchievementData();
        }

        Achieve(0);
    }

    private void AddAchievenments()
    {
        achievementData.Add(new Achievement("게임 스타트", "게임을 처음 시작했다.", false));
        achievementData.Add(new Achievement("장애물", "장애물에 처음으로 부딪혔다.", false));
        achievementData.Add(new Achievement("500", "점수 500점을 돌파했다.", false));
    }

    public void Achieve(int listIdx)
    {
        if (achievementData[listIdx].IsAchieved)
        {
            return;
        }   
        else
        {
            StartCoroutine(SetAchievementUI(listIdx));
            SoundManager.instance.PlaySFX(SFX.ACHIEVED);
            achievementData[listIdx].IsAchieved = true;
            SaveAchievementData();
        }
    }

    private IEnumerator SetAchievementUI(int listIdx)
    {
        achievePanel.gameObject.SetActive(true);
        achieveTitleTxt.text = achievementData[listIdx].Name.ToString();
        achieveDescTxt.text = achievementData[listIdx].Description.ToString();

        yield return new WaitForSeconds(3f);

        achievePanel.gameObject.SetActive(false);
    }

    // JSON
    void SaveAchievementData()
    {
        AchievementData data = new AchievementData { achievementData = achievementData };
        string saveData = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/AchievementData.txt", saveData);
        
        Debug.Log("저장 완료.\n" + saveData);
    }

    void LoadAchievementData()
    {
        string loadData = File.ReadAllText(Application.persistentDataPath + "/AchievementData.txt");
        AchievementData data = JsonUtility.FromJson<AchievementData>(loadData);
        achievementData = data.achievementData;

        Debug.Log("로드 완료.\n" + loadData);
    }
}
