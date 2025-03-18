using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// BGM ����
public enum BGM
{
    TITLE,
    GAME
}

// SFX ����
public enum SFX
{
    JUMP,       // ����
    SLIDE,      // �����̵�
    CRASH,      // ��ֹ��� �浹
    ITEM,       // ������ �Ծ��� ��

    BUTTON,     // ��ư UI Ŭ��
    ACHIEVED,   // ���� �޼� ��
    GAMEOVER,   // ���� ���� ��
}


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("AudioClips List")]
    [SerializeField] AudioClip[] BGMList;
    [SerializeField] AudioClip[] SFXList;

    [Header("Play Clips")]
    [SerializeField] AudioSource audioBGM;
    [SerializeField] AudioSource audioSFX;


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

    private void Start()
    {
        PlayBGM(BGM.TITLE);
    }

    // BGM ��� / ����
    public void PlayBGM(BGM bgmIdx)
    {
        audioBGM.clip = BGMList[(int)bgmIdx];
        audioBGM.Play();
    }

    public void StopBGM()
    {
        audioBGM.Stop();
    }

    // SFX ���
    public void PlaySFX(SFX sfxIdx)
    {
        if ((int)sfxIdx == 1)       // �����̵��� ���
        {
            audioSFX.clip = SFXList[(int)sfxIdx];
            audioSFX.Play();
            //audioSFX.PlayOneShot(SFXList[(int)sfxIdx]);

        }
        else
            audioSFX.PlayOneShot(SFXList[(int)sfxIdx]);
    }
    public void StopSFX()
    {
        audioSFX.Stop();
    }
}
