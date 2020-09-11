using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    //config 
    [SerializeField] float levelLoadDelay = 2f;

    //chache
    Scene scene;
    int currentSceneIndex;

    Animator myAnimator;
    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        currentSceneIndex = scene.buildIndex;
        Debug.Log("On start the scene is:  "+currentSceneIndex);
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentSceneIndex++;
        StartCoroutine(LoadNextLevel());
        Debug.Log("Loading scene: " + currentSceneIndex);
    }

    IEnumerator LoadNextLevel()
    {

        myAnimator.SetTrigger("Winning");
        yield return new WaitForSeconds(levelLoadDelay);
        //load next scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}
