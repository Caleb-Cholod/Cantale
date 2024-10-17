using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour
{
    private Animator transition;
    //private Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        //transition = GameObject.FindWithTag("CrossfadeAnimator").GetComponent<Animator>();
        //animation = transition.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(int levelID){
        Time.timeScale = 1;
        StartCoroutine(LoadmyLevel(levelID));
        //GetComponent<MovetoScene>().GoToScene(1);
        
    }
    public void LoadLevelUpDown(int levelID){
        Time.timeScale = 1;
        StartCoroutine(LoadUpDownWipe(levelID));
        //GetComponent<MovetoScene>().GoToScene(1);
        
    }
    IEnumerator LoadUpDownWipe(int levelIndex){
        transition = GameObject.FindWithTag("UpDownAnimator").GetComponent<Animator>();

        transition.SetBool("Start", true);
        yield return new WaitForSeconds(1f);

        Time.timeScale = 1;
        SceneManager.LoadScene(levelIndex);
    }



    IEnumerator LoadmyLevel(int levelIndex){
        transition = GameObject.FindWithTag("CrossfadeAnimator").GetComponent<Animator>();

        transition.SetBool("Start", true);
        yield return new WaitForSeconds(1f);

        Time.timeScale = 1;
        SceneManager.LoadScene(levelIndex);
    }
}
