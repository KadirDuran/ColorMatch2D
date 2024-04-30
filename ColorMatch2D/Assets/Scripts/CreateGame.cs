using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CreateGame : MonoBehaviour
{
    int counter = 0;
    public List<Sprite> sprites = new List<Sprite>();
    public List<GameObject> rightGroup = new List<GameObject>();
    public List<GameObject> leftGroup = new List<GameObject>();
    public TextMeshProUGUI txtTime, trueCountText;
    int trueCount = 0;
    public AudioClip aClipYes, aClipNo;
    AudioSource aSource  ;
    float timeX;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        timeX= Time.time;   
        CreateGames(); 
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        txtTime.text=(60-(Time.time-timeX)).ToString("F0");
    }
    public void CreateGames()
    {
        List<int> list = new List<int>();
        list = RandomNumber();
        Shuffle(leftGroup);
        Shuffle(rightGroup);
        for (int i = 0; i < list.Count; i++)
        {
            leftGroup[i].GetComponent<SpriteRenderer>().sprite = sprites[list[i]];
            leftGroup[i].name = sprites[list[i]].name;

            rightGroup[i].GetComponent<SpriteRenderer>().sprite = sprites[list[i]];
            rightGroup[i].name = sprites[list[i]].name;



        }
    }
    public void EnableScript()
    {
        for (int i = 0; i < leftGroup.Count; i++)
        {
            leftGroup[i].GetComponent<TestClick>().enabled = true;
        }

        panel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScreen");
        
        
    }
    private List<int> RandomNumber()
    {
        List<int> number = new List<int>();
       
        while (counter < 7)
        {
            int randomN = Random.Range(0, 15);

            if (!number.Contains(randomN))
            {
                number.Add(randomN);
                counter++;

            }

        }
        return number;
    }
    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void TrueAnswer()
    {
        trueCount++;
        Camera.main.backgroundColor = Color.green;
        trueCountText.text = trueCount.ToString() + " / 7"; 
        
        aSource.clip = aClipYes;
        aSource.Play();
        if (trueCount == 7)
        {
            trueCountText.text = "Tebrikler Kazandýnýz..";
            Time.timeScale = 0f;
            panel.SetActive(true);
         
           
        }

        
      
    }
    public void FalseAnswer()
    {
        Camera.main.backgroundColor = Color.red;
        aSource.clip = aClipNo;
        aSource.Play();
    }
}
