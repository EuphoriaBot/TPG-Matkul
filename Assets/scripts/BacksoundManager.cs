using UnityEngine;
using UnityEngine.Audio;

public class BacksoundManager : MonoBehaviour
{
    public AudioSource Backsound;
    [SerializeField] private AudioClip CalmBacksound;
    [SerializeField] private AudioClip ChaseBacksound;

    void Awake()
    {
        Backsound = GetComponent<AudioSource>();
    }

    public void Calm()
    {
        if ((Object)Backsound.generator == (Object)CalmBacksound) return;
        Backsound.generator = CalmBacksound;
        Backsound.Play();

    }

    public void Chase()
    {
        if ((Object)Backsound.generator == (Object)ChaseBacksound) return;
        Backsound.generator = ChaseBacksound;
        Backsound.Play();
    }

    private static BacksoundManager s_instance;
    public static BacksoundManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindFirstObjectByType<BacksoundManager>();
            }
            return s_instance;
        }
    }
}
