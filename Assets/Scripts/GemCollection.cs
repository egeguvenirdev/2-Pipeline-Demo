using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollection : MonoBehaviour
{
    [SerializeField] private UIManager UIM;
    private ObjectPooler objPooler;
    public ParticleColor colorType;
    private Haptic haptic;

    public static GemCollection gemCollection;
    [SerializeField] private AudioClip collectAudio;

    private AudioSource camSound;

    public enum ParticleColor
    {
        Blue, 
        Green,
        Yellow,
        Pink,
        Purple,
        Orange
    } 

    private void Start()
    {
        objPooler = FindObjectOfType<ObjectPooler>();
        haptic = FindObjectOfType<Haptic>();
        UIM = FindObjectOfType<UIManager>();
        camSound = Camera.main.GetComponent<AudioSource>();
        gemCollection = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        //collect vibration
        if (PlayerPrefs.GetInt("vibrationOnOff", 1) == 1)
        {
            haptic.HapticFeedback(MoreMountains.NiceVibrations.HapticTypes.Selection); //Vibration
        }

        //collect sound
        if (PlayerPrefs.GetInt("soundOnOff", 1) == 1)
        {
            camSound.PlayOneShot(collectAudio);
        }

        gameObject.SetActive(false);

        //take some particles from pool and instantiate them
        var particle = objPooler.GetPooledObject(colorType.ToString());
        particle.transform.position = transform.position;
        particle.transform.rotation = Quaternion.Euler(180, Random.Range(-25f, 26f), 0);
        particle.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();

        StressReceiver.shake.InduceStress(0.01f); //Cam shake

        UIM.CurrentScore(); //calling the func which is adding the every gem's score to a variable 

        Invoke("Reactivate", 2f); //reactivating the gems for reusability
    }

    private void Reactivate()
    {
        gameObject.SetActive(true);
    }
}