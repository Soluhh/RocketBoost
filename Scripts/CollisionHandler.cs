using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = .5f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successVFX;
    [SerializeField] ParticleSystem crashVFX;
    AudioSource myAudioSource;
    bool isTransitioning = false;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning) {return;}

        if(other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Ground")
        {
            StartCrashSequence();
        }
        else if(other.gameObject.tag == "LandingBase")
        {
            myAudioSource.PlayOneShot(successSFX);
            StartLoadSequence();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            StartCrashSequence();
        }
    }

    void StartLoadSequence()
    {
        isTransitioning = true;
        successVFX.Play();
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(successSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadLevelDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        crashVFX.Play();
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", .5f);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
