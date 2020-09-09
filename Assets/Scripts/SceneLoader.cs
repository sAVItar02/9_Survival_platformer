using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader: MonoBehaviour
{
    [SerializeField] Sprite[] Backgrounds;
    [SerializeField] GameObject[] currentBackground;
    [SerializeField] SceneFader fader;
    void Start()
    {
        int random = Random.Range(0, Backgrounds.Length);
        ChangeSprite(random);
        
    }

    public void ChangeSprite(int index)
    {
        for (int i = 0; i < currentBackground.Length; i++)
        {
            var spriteRenderer = currentBackground[i].GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Backgrounds[index];
        }
    }

    public void LoadStartScene()
    {
        fader.FadeTo(0);
    }

    public void LoadGameScene()
    {
        fader.FadeTo(1);
        PlayerPrefs.SetFloat("Score", 0f);
    }

    public void LoadGameOverScene()
    {
        fader.FadeTo(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
