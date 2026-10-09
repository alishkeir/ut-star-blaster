using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Shooting SFX")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 0.5f;


    [Header("Damage SFX")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = 0.5f;

    [Header("Destroy SFX")]
    [SerializeField] AudioClip destroyClip;
    [SerializeField][Range(0f, 1f)] float destroyVolume = 0.5f;



    static AudioManager instance = null;


    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void PlayShootingSFX()
    {
        PlaySFX(shootingClip, shootingVolume);
    }

    public void PlayDamageSFX()
    {
        PlaySFX(damageClip, damageVolume);
    }

    public void PlayDestroySFX()
    {
        PlaySFX(destroyClip, destroyVolume);
    }



    void PlaySFX(AudioClip clip, float volume)
    {
        if (!clip) return;

        // to prevent audio stuttering because of camera shake, keep it at Camera X znd Y but lower Z to prevent Camera and Audio from overlapping
        Vector3 camPos = Camera.main.transform.position;
        camPos.z -= 2f;


        AudioSource.PlayClipAtPoint(clip, camPos, volume);
    }
}
