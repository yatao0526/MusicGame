using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] notes;
    [SerializeField] private GameObject[] notesPop;
    [SerializeField] private GameObject[] TapPoint;
    [SerializeField] private GameObject StartBottun;

    [SerializeField] private GameObject background;   //難易度選択画面
    [SerializeField] private GameObject course;       //レーン
    [SerializeField] private AudioSource audioSource; //曲の選択        

    private SongReproduction songReproduction; //SongReproductionスクリプトをここに入れる

    //Check関数のtrigger 
    private bool checkTrigger = true;
    private bool _isNotesInstance = false;
    private bool isStart = false;
    private bool musicTime = false;

    private float[] _timing;
    private int[] _lineNum;

    private float _startTime = 0;
    private float timeoffSet = -1;
    private float _resulttime_i;
    private int _resulttime;    
    private int _noteCount = 0;

    private void Start()
    {
        songReproduction = GameObject.Find("TapSound").GetComponent<SongReproduction>(); //スクリプトを読み込む
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
        _timing = new float[1024];
        _lineNum = new int[1024];
        for (int num = 0; num < 4; num++)
        {
            notes[num].transform.position = notesPop[num].transform.position;
        }
        Cursor.visible = false;
    }
    private void Update()
    {
        if (checkTrigger == true)
        {
            Check();
        }
        else if (checkTrigger == false)
        {
            background.SetActive(false);
            course.SetActive(true);
            if (isStart == false && Input.GetKeyDown(KeyCode.Joystick1Button1) || isStart == false && Input.GetMouseButtonDown(0))
            {
                //ボタン非表示
                StartBottun.SetActive(false);
                //曲再生
                audioSource.Play();
                _startTime = Time.time;
                musicTime = true;
                _isNotesInstance = true;
                isStart = true;
                Application.LoadLevelAdditive("BackMMD");
            }
            if (isStart == false && Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                course.SetActive(false);
                background.SetActive(true);
                checkTrigger = true;
            }
        }        
        //ゲーム終了までのタイムカウント
        if (musicTime == true)
        {
            //今日俺が書き換えた方
            _resulttime_i += Time.deltaTime;
            _resulttime = (int)_resulttime_i;

            //やたろうが書き換えた方
            //_resulttime += (int)Time.deltaTime;
        }
        //notesの生成
        if (_isNotesInstance == true)
        {
            CheckNotes();
        }
        //曲の再生時間
        if (_resulttime == 76)
        {
            SceneManager.LoadScene("Result");
        }
    }
    //次に生成するノーツのチェック
    private void CheckNotes()
    {
        //
        while (_timing[_noteCount] + timeoffSet < GetMusicTime() && _timing[_noteCount] != 0)
        {
            NotesPop(_lineNum[_noteCount]);
            _noteCount++;
        }
    }
    //ノーツ生成
    private void NotesPop(int num)
    {
        Instantiate(notes[num]);
    }
    //難易度選択
    private void Check()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            LoadCSVMode("NotesEasy");
            checkdata();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            LoadCSVMode("NotesNormal");
            checkdata();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            LoadCSVMode("NotesHard");
            checkdata();
        }
    }
    private void checkdata()
    {
        songReproduction.ChoiceAudio();
        checkTrigger = false;
    }
    //CSVの読み込み
    private void LoadCSVMode(string name)
    {
        int i = 0, j;
        TextAsset csv = Resources.Load(name) as TextAsset;
        StringReader reader = new StringReader(csv.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            for (j = 0; j < values.Length; j++)
            {
                _timing[i] = float.Parse(values[0]);
                _lineNum[i] = int.Parse(values[1]);
            }
            i++;
        }
    }
    private float GetMusicTime()
    {
        return Time.time - _startTime;
    }
}
