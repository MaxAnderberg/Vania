using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    //config 
    [SerializeField] float levelLoadDelay = 4f;

    //chache
    Scene scene;
    

    Animator myAnimator;
    private void Start()
    {
       
        
        
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        StartCoroutine(LoadNextLevel());
        
    }

    IEnumerator LoadNextLevel()
    {

        // maybe freeze the time here as well. 
        
        myAnimator.SetTrigger("Winning");
        Time.timeScale = 0.4f;

        yield return new WaitForSeconds(levelLoadDelay);
        
        Time.timeScale = 1f;

        //load next scene
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex++);
    }
}
