using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NotesController : MonoBehaviour
{
    //各キーの判定検知flag
    private bool Aflag = false;
    private bool Bflag = false;
    private bool Xflag = false;
    private bool Yflag = false;

    [SerializeField] private float moveTime;
    //使いたいエフェクトを入れる
    [SerializeField] private ParticleSystem tapEffect;　

    private GameObject[] _gameObject = new GameObject[4];
    private GameObject[] destination = new GameObject[4];
    private GameObject[] start = new GameObject[4];
    private Vector3[] v_start = new Vector3[4];
    private Vector3[] v_destination = new Vector3[4];

    private Transform transform;

    private float time = 0f;

    private int score = 0;

    private GameObject UIobj;

    private GameController Audio;

    private ScoreController scoreController;
    private ScoreController comboController;
    private ScoreController judgController;

    void Start()
    {
        transform = GetComponent<Transform>();
        NotesSetting();

        UIobj = GameObject.Find("GameController");
        scoreController = UIobj.GetComponent<ScoreController>();
        comboController = UIobj.GetComponent<ScoreController>();
        judgController =  UIobj.GetComponent<ScoreController>();
    }

    void Update()
    {
        //各ノーツを動かす
        switch (transform.gameObject.tag)
        {
            case "Notes0":
                var v0 = time / moveTime;
                transform.position = Vector3.Lerp(v_start[0], v_destination[0], v0);
                time += Time.deltaTime;
                break;
            case "Notes1":
                var v1 = time / moveTime;
                transform.position = Vector3.Lerp(v_start[1], v_destination[1], v1);
                time += Time.deltaTime;
                break;
            case "Notes2":
                var v2 = time / moveTime;
                transform.position = Vector3.Lerp(v_start[2], v_destination[2], v2);
                time += Time.deltaTime;
                break;
            case "Notes3":
                var v3 = time / moveTime;
                transform.position = Vector3.Lerp(v_start[3], v_destination[3], v3);
                time += Time.deltaTime;
                break;
        }
        Checkinput();
    }

    //notesに各情報を持たせる
    private void NotesSetting()
    {
        switch (gameObject.tag)
        {
            //各ノーツに生成したpos目的地etc...必要な情報の取得
            case "Notes0":
                destination[0] = GameObject.Find("NotesDestroy0");
                start[0] = GameObject.Find("PopNote0");
                _gameObject[0] = this.gameObject;
                v_destination[0] = destination[0].transform.position;
                v_start[0] = start[0].transform.position;
                break;
            case "Notes1":
                destination[1] = GameObject.Find("NotesDestroy1");
                start[1] = GameObject.Find("PopNote1");
                _gameObject[1] = this.gameObject;
                v_destination[1] = destination[1].transform.position;
                v_start[1] = start[1].transform.position;
                break;
            case "Notes2":
                destination[2] = GameObject.Find("NotesDestroy2");
                start[2] = GameObject.Find("PopNote2");
                _gameObject[2] = this.gameObject;
                v_destination[2] = destination[2].transform.position;
                v_start[2] = start[2].transform.position;
                break;
            case "Notes3":
                destination[3] = GameObject.Find("NotesDestroy3");
                start[3] = GameObject.Find("PopNote3");
                _gameObject[3] = this.gameObject;
                v_destination[3] = destination[3].transform.position;
                v_start[3] = start[3].transform.position;
                break;
        }
    }

    //キーを押したときの判定
    private void Checkinput()
    {
        float vert = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");

        //Bボタンの判定
        if (Bflag == true && Input.GetKeyDown(KeyCode.Joystick1Button1) || Bflag == true && hori == 1.0f)
        {
            InputData();
            Bflag = false;
        }
        //Ａボタンの判定
        if (Aflag == true && Input.GetKeyDown(KeyCode.Joystick1Button0) || Aflag == true && vert == -1.0f)
        {
            InputData();
            Aflag = false;
        }
        //Ｘボタンの判定
        if (Xflag == true && Input.GetKeyDown(KeyCode.Joystick1Button2) || Xflag == true && hori == -1.0f)
        {
            InputData();
            Xflag = false;
        }
        //Ｙボタンの判定
        if (Yflag == true && Input.GetKeyDown(KeyCode.Joystick1Button3) || Yflag == true && vert == 1.0f)
        {
            InputData();
            Yflag = false;
        }
    }
    private void InputData()
    {
        Judgment();
        scoreController.AddScore(score);
        Destroy(this.gameObject);
    }

    //タップした時のノーツの判定(高橋)
    private void Judgment()
    {
        var NotesPos = gameObject.transform.position;
        comboController.AddCombo();
        //エフェクトの再生
        var obj = Instantiate(tapEffect, NotesPos, Quaternion.identity);
        GameObject game = new GameObject("Empty");
        obj.transform.parent = game.transform;
        game.AddComponent<Effect>();
    }
    //flagをtrueにする処理と叩けなかったノーツのdestroy
    private void OnTriggerEnter(Collider other)
    {        
        switch (other.gameObject.name)
        {
            //NotesCheckに触れたらflagをtrueにする
            case "NotesCheck0":
                Bflag = true;
                score = 50;
                break;
            case "NotesCheck1":
                Aflag = true;
                score = 50;
                break;
            case "NotesCheck2":
                Yflag = true;
                score = 50;
                break;
            case "NotesCheck3":
                Xflag = true;
                score = 50;
                break;
            //NotesDestroyに触れたらそのオブジェクトを破壊する
            case "NotesDestroy0":
                Destroy(this.gameObject);
                break;
            case "NotesDestroy1":
                Destroy(this.gameObject);
                break;
            case "NotesDestroy2":
                Destroy(this.gameObject);
                break;
            case "NotesDestroy3":
                Destroy(this.gameObject);
                break;
            //objDestroyCheckerに触れたらミス判定にする
            case "objDestroyChecker0":
                judgController.MissJudgment();
                Bflag = false;
                break;
            case "objDestroyChecker1":
                judgController.MissJudgment();
                Aflag = false;
                break;
            case "objDestroyChecker2":
                judgController.MissJudgment();
                Yflag = false;
                break;
            case "objDestroyChecker3":
                judgController.MissJudgment();
                Xflag = false;
                break;
        }
    }
}
