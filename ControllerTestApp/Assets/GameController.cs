using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int SequencesToGenerate;
    private int VisibleButtons;

    private string Logfile;
    private string Logfile2;
    private float Timer;
    private float TimerLastCorrect;
    private bool ActiveTest;
    private bool ReadyToTest;
    private bool InputLagTest;
    private int Id;
    string[] CurrentLogState;
    string[] CurrentLogStateInputLag;
    private string controllerInUse;
    string Delimiter;

    private DateTime date;

    public GameObject StartText;
    public GameObject ReadyToStartOB;
    public GameObject TestSequence;
    public GameObject SequencePanel;
    public GameObject ButtonsPreviewGO;
    public AudioSource audioSource;
    public AudioClip audioClipCorrect;
    public AudioClip audioClipWrong;
    public TextMeshProUGUI DisplayIdText;

    StreamWriter outStreamA;
    StreamWriter outStreamB;

    // Handle dpad buttons
    private bool dpadUp;
    private bool dpadLeft;
    private bool dpadDown;
    private bool dpadRight;


    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
        TimerLastCorrect = 0;
        Id = 0;
        CurrentLogState = new string[7];
        CurrentLogStateInputLag = new string[1];
        Delimiter = ",";

        dpadUp = false;
        dpadLeft = false;
        dpadDown = false;
        dpadRight = false;

        TestSequence = Instantiate(TestSequence, SequencePanel.transform);



    }

    private void InitInputLagLog()
    {
        string dirPath = Application.persistentDataPath + "\\TestLogs\\";
        Logfile = Application.persistentDataPath + "\\TestLogs\\" + "test_log_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + "InputlagXBOX" + Id + ".csv";
        Logfile2 = Application.persistentDataPath + "\\TestLogs\\" + "test_log_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + "InputlagBLE" + Id + ".csv";

        Debug.Log("Sparar logfilen: " + Logfile + " och i: " + Logfile2);

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        //CurrentLogStateInputLag[0] = "ID";
        CurrentLogStateInputLag[0] = "Time";
        //CurrentLogStateInputLag[2] = "Key";
        

        LogToFileInputLag(CurrentLogStateInputLag);
    }

    private void InitLog(string controllerInUse)
    {
        string dirPath = Application.persistentDataPath + "\\TestLogs\\";
        Logfile = Application.persistentDataPath + "\\TestLogs\\" + "test_log_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + "_Userid_" + Id + ".csv";
        Debug.Log("Sparar logfilen: " + Logfile);

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        CurrentLogState[0] = "ID";
        CurrentLogState[1] = "Time";
        CurrentLogState[2] = "Key";
        CurrentLogState[3] = "Correct";
        CurrentLogState[4] = "TimeSinceLastCorrect";
        CurrentLogState[5] = "Controller";
        CurrentLogState[6] = "#Buttons";

        this.controllerInUse = controllerInUse;

        LogToFile(CurrentLogState);
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveTest && ReadyToTest)
        {
            Timer += Time.deltaTime;

            bool correct = false;
            


            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                Debug.Log("A");
                correct = TestSequence.GetComponent<SequenceController>().CorrectButtonPress(KeyCode.A);
                LogKeyPress("A", correct);
                if (correct)
                {
                    audioSource.PlayOneShot(audioClipCorrect);
                } else
                {
                    audioSource.PlayOneShot(audioClipWrong);

                }

            }

            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                Debug.Log("B");
                correct = TestSequence.GetComponent<SequenceController>().CorrectButtonPress(KeyCode.B);
                LogKeyPress("B", correct);
                if (correct)
                {
                    audioSource.PlayOneShot(audioClipCorrect);
                }
                else
                {
                    audioSource.PlayOneShot(audioClipWrong);

                }


            }

            if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                Debug.Log("X");
                correct = TestSequence.GetComponent<SequenceController>().CorrectButtonPress(KeyCode.X);
                LogKeyPress("X", correct);
                if (correct)
                {
                    audioSource.PlayOneShot(audioClipCorrect);
                }
                else
                {
                    audioSource.PlayOneShot(audioClipWrong);

                }

            }

            if (Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                Debug.Log("Y");
                correct = TestSequence.GetComponent<SequenceController>().CorrectButtonPress(KeyCode.Y);
                LogKeyPress("Y", correct);
                if (correct)
                {
                    audioSource.PlayOneShot(audioClipCorrect);
                }
                else
                {
                    audioSource.PlayOneShot(audioClipWrong);

                }

            }

            if (Input.GetKeyDown(KeyCode.JoystickButton6))
            {
                Debug.Log("LB");
                correct = TestSequence.GetComponent<SequenceController>().CorrectButtonPress(KeyCode.L);
                LogKeyPress("LB", correct);
                if (correct)
                {
                    audioSource.PlayOneShot(audioClipCorrect);
                }
                else
                {
                    audioSource.PlayOneShot(audioClipWrong);

                }

            }

            if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                Debug.Log("RB");
                correct = TestSequence.GetComponent<SequenceController>().CorrectButtonPress(KeyCode.R);
                LogKeyPress("RB", correct);
                if (correct)
                {
                    audioSource.PlayOneShot(audioClipCorrect);
                }
                else
                {
                    audioSource.PlayOneShot(audioClipWrong);

                }

            }
            /*
            if (Input.GetKeyDown(KeyCode.JoystickButton11))
            {
                Debug.Log("START");
                correct = TestSequence.GetComponent<SequenceController>().CorrectButtonPress(KeyCode.S);
                LogKeyPress("START", correct);
                if (correct)
                {
                    audioSource.PlayOneShot(audioClipCorrect);
                }
                else
                {
                    audioSource.PlayOneShot(audioClipWrong);

                }

            }

            if (Input.GetKeyDown(KeyCode.JoystickButton10))
            {
                Debug.Log("SELECT");
                correct = TestSequence.GetComponent<SequenceController>().CorrectButtonPress(KeyCode.E);
                LogKeyPress("SELECT", correct);
                if (correct)
                {
                    audioSource.PlayOneShot(audioClipCorrect);
                }
                else
                {
                    audioSource.PlayOneShot(audioClipWrong);

                }

            }
            */
            if (Input.GetAxis("Horizontal") < -0.75 && !dpadLeft)
            {
                Debug.Log("Left pressed");
                correct = TestSequence.GetComponent<SequenceController>().CorrectButtonPress(KeyCode.LeftArrow);
                LogKeyPress("LEFT", correct);
                if (correct)
                {
                    audioSource.PlayOneShot(audioClipCorrect);
                }
                else
                {
                    audioSource.PlayOneShot(audioClipWrong);

                }

                dpadLeft = true;
            } else if(Input.GetAxis("Horizontal") == 0 && dpadLeft)
            {
                Debug.Log("Left released");
                dpadLeft = false;
            }

            if (Input.GetAxis("Horizontal") == 1 && !dpadRight)
            {
                Debug.Log("Right pressed");
                correct = TestSequence.GetComponent<SequenceController>().CorrectButtonPress(KeyCode.RightArrow);
                LogKeyPress("RIGHT", correct);
                if (correct)
                {
                    audioSource.PlayOneShot(audioClipCorrect);
                }
                else
                {
                    audioSource.PlayOneShot(audioClipWrong);

                }

                dpadRight = true;
            }
            else if (Input.GetAxis("Horizontal") == 0 && dpadRight)
            {
                Debug.Log("Right released");
                dpadRight = false;
            }

            if (Input.GetAxis("Vertical") == -1 && !dpadUp)
            {
                Debug.Log("Up pressed");
                correct = TestSequence.GetComponent<SequenceController>().CorrectButtonPress(KeyCode.UpArrow);
                LogKeyPress("UP", correct);
                if (correct)
                {
                    audioSource.PlayOneShot(audioClipCorrect);
                }
                else
                {
                    audioSource.PlayOneShot(audioClipWrong);

                }

                dpadUp = true;
            }
            else if (Input.GetAxis("Vertical") == 0 && dpadUp)
            {
                Debug.Log("Up released");
                dpadUp = false;
            }

            if (Input.GetAxis("Vertical") == 1 && !dpadDown)
            {
                Debug.Log("Down pressed");
                correct = TestSequence.GetComponent<SequenceController>().CorrectButtonPress(KeyCode.DownArrow);
                LogKeyPress("DOWN", correct);
                if (correct)
                {
                    audioSource.PlayOneShot(audioClipCorrect);
                }
                else
                {
                    audioSource.PlayOneShot(audioClipWrong);

                }

                dpadDown = true;
            }
            else if (Input.GetAxis("Vertical") == 0 && dpadDown)
            {
                Debug.Log("Down released");
                dpadDown = false;
            }

            

            if (correct & TestSequence.GetComponent<SequenceController>().IsEmpty())
            {
                Debug.Log("Try show new buttons.");
                if (!TestSequence.GetComponent<SequenceController>().ShowNewButtons(VisibleButtons))
                {
                    ActiveTest = false;
                    Debug.Log("Test over.");
                    StartText.SetActive(true);
                    ButtonsPreviewGO.SetActive(true);
                    ReadyToTest = false;


                }
            }

            //Debug.Log("X: " + Input.GetAxisRaw("Horizontal") + " Y: " + Input.GetAxisRaw("Vertical"));
        } else if(!ActiveTest && !ReadyToTest && !InputLagTest)
        {
            if(Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.W))
            {
                VisibleButtons = 4;
            } else
            {
                VisibleButtons = 1;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SetCurrentIdText(++Id);

            } else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SetCurrentIdText(--Id);
            }

            
            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("Nu startar vi ett sekvens test med vår kontroller");
                InitLog("BLE Controller");
                StartText.SetActive(false);
                ButtonsPreviewGO.SetActive(false);
                GenerateSequences(SequencesToGenerate, 4);
                ActiveTest = true;

            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Nu startar vi ett sekvens test med en snes kontroller");
                InitLog("SNES Controller");
                StartText.SetActive(false);
                ButtonsPreviewGO.SetActive(false);
                GenerateSequences(SequencesToGenerate, 4);
                ActiveTest = true;

            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Nu startar vi ett singelknappstest med en vår kontroller");
                InitLog("BLE Controller");
                StartText.SetActive(false);
                ButtonsPreviewGO.SetActive(false);
                GenerateSequences(SequencesToGenerate * 2, 1);
                ActiveTest = true;

            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Nu startar vi ett singelknappstest med en snes kontroller");
                InitLog("SNES Controller");
                StartText.SetActive(false);
                ButtonsPreviewGO.SetActive(false);
                GenerateSequences(SequencesToGenerate * 2, 1);
                ActiveTest = true;

            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("Nu startar vi ett input lag test");
                InitInputLagLog();
                StartText.SetActive(false);
                ButtonsPreviewGO.SetActive(false);
                InputLagTest = true;

            }
        }
        else if(InputLagTest){
            Timer += Time.deltaTime;



            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                LogKeyPressInputLag("BLE");

            }
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                LogKeyPressInputLag("XBOX");
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                StartText.SetActive(true);
                ButtonsPreviewGO.SetActive(true);
                InputLagTest = false;
                outStreamA.Close();
                outStreamB.Close();
            }
        }
        else
        {
            ReadyToStartOB.SetActive(true);
            TestSequence.SetActive(false);

            if (Input.GetKeyDown(KeyCode.JoystickButton11))
            {
                ReadyToTest = true;
                ReadyToStartOB.SetActive(false);
                TestSequence.SetActive(true);

            }
        }

            

        
    }

    private void SetCurrentIdText(int NewId)
    {
        DisplayIdText.text = "Current ID: " + NewId;
    }

    private void GenerateSequences(int SequencesToGenerate, int size)
    {
        for(int i = 0; i < SequencesToGenerate; i++)
        {
            TestSequence.GetComponent<SequenceController>().GenerateNewSequence(size);
        }
        TestSequence.GetComponent<SequenceController>().ShowNewButtons(size);


    }

    private void LogKeyPressInputLag(string key)
    {
        //CurrentLogStateInputLag[0] = Id++.ToString();
        CurrentLogStateInputLag[0] = Timer.ToString().Replace(",", "");
        //CurrentLogStateInputLag[2] = key;

        if (key.Equals("XBOX"))
        {
            LogToFileInputLag(CurrentLogStateInputLag);

        }
        else
        {
            LogToFileInputLagB(CurrentLogStateInputLag);

        }

    }

    private void LogKeyPress(string key, bool Correct)
    {
        CurrentLogState[0] = Id.ToString();
        CurrentLogState[1] = Timer.ToString().Replace(",", "");
        CurrentLogState[2] = key;
        CurrentLogState[3] = Correct ? "Correct" : "Wrong";
        CurrentLogState[4] = (Timer - TimerLastCorrect).ToString().Replace(",", "");
        CurrentLogState[5] = controllerInUse;
        CurrentLogState[6] = VisibleButtons.ToString();

        TimerLastCorrect = Correct ? Timer : TimerLastCorrect;

        LogToFile(CurrentLogState);
    }

    private void LogToFile(string[] state)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Join(Delimiter, state));


        StreamWriter outStream = new StreamWriter(Logfile, true);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    private void LogToFileInputLag(string[] state)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Join(Delimiter, state));

        string tempLogPath = "";

        if (state[0].Equals("Time"))
        {
            outStreamA = new StreamWriter(Logfile, true);
            outStreamA.WriteLine(sb);

            outStreamB = new StreamWriter(Logfile2, true);
            outStreamB.WriteLine(sb);

            return;
        }

        
        outStreamA.WriteLine(sb);
        
    }

    private void LogToFileInputLagB(string[] state)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Join(Delimiter, state));

        
        outStreamB.WriteLine(sb);
    }


}
