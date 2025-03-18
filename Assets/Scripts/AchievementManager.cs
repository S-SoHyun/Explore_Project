using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;


// ���� ����
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

// ���� ���� ���� ����Ʈ
[System.Serializable]
public class AchievementData
{
    public List<Achievement> achievementData;
}


// ���� ����
public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public List<Achievement> achievementData = new List<Achievement>();

    [SerializeField] private GameObject achievePanel;
    [SerializeField] private TextMeshProUGUI achieveTitleTxt;
    [SerializeField] private TextMeshProUGUI achieveDescTxt;
    // ������ ������?


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
        achievementData.Add(new Achievement("���� ��ŸƮ", "������ ó�� �����ߴ�.", false));
        achievementData.Add(new Achievement("��ֹ�", "��ֹ��� ó������ �ε�����.", false));
        achievementData.Add(new Achievement("500", "���� 500���� �����ߴ�.", false));
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
        
        Debug.Log("���� �Ϸ�.\n" + saveData);
    }

    void LoadAchievementData()
    {
        string loadData = File.ReadAllText(Application.persistentDataPath + "/AchievementData.txt");
        AchievementData data = JsonUtility.FromJson<AchievementData>(loadData);
        achievementData = data.achievementData;

        Debug.Log("�ε� �Ϸ�.\n" + loadData);
    }
}
