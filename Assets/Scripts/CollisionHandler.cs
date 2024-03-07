using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem thrustersParticles;
    [SerializeField] ParticleSystem rightThrustersParticles;
    [SerializeField] ParticleSystem leftThrustersParticles;
    private AudioSource audioSource;
    private bool isTransitioning = false;

    private void Start() {
        
    }
    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Friendly":
                break;

            case "Finish":
                SuccessSequence();
                break;

            default:
                CrashSequence();
                break;    
        }
    }

    private void SuccessSequence() {
        if (!isTransitioning) {
            GetComponent<Movement>().enabled = false;
            audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
            audioSource.PlayOneShot(success);
            thrustersParticles.Stop();
            rightThrustersParticles.Stop();
            leftThrustersParticles.Stop();
            successParticles.Play();
            isTransitioning = true;
            Invoke("LoadNextLevel", delayTime);
        }
    }

    private void CrashSequence() {
        if (!isTransitioning) {
            GetComponent<Movement>().enabled = false;
            audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
            thrustersParticles.Stop();
            rightThrustersParticles.Stop();
            leftThrustersParticles.Stop();
            crashParticles.Play();
            audioSource.PlayOneShot(crash);
            isTransitioning = true;
            Invoke("ReloadLevel", delayTime);
        }
    }

    private void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}