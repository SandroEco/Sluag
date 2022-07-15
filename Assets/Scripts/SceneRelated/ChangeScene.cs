using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string nameOfScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            SaveManager.instance.Save();
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator LoadScene()
    {
        Movement.Instance.changingScene = true;
        Fade.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nameOfScene);
    }
}
