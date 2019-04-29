using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceController : MonoBehaviour
{
    /*
    public GameObject A;
    public GameObject B;
    public GameObject X;
    public GameObject Y;
    public GameObject L;
    public GameObject R;
    public GameObject LEFT;
    public GameObject RIGHT;
    public GameObject UP;
    public GameObject DOWN;
    */

    public List<GameObject> SequenceButtons;
    private Queue<GameObject> Buttons;
    private Queue<GameObject> VisibleButtons;



    // Start is called before the first frame update
    void Start()
    {
        Buttons = new Queue<GameObject>();
        VisibleButtons = new Queue<GameObject>();
    }

    public bool CorrectButtonPress(KeyCode KeyPressed)
    {
        if(GetCurrentButtonKey() == KeyPressed)
        {
            Debug.Log("Correct key pressed.");
            DequeueCurrentButton();
            Debug.Log("Dequeud a button");
            return true;
        } else
        {
            Debug.Log("Wrong key pressed.");
            return false;
        }
    }

    public bool ShowNewButtons(int size)
    {
        if(Buttons.Count == 0)
        {
            return false;

        } else
        {
            for(int i = 0; i < size; i++)
            {
                GameObject tmp = Buttons.Dequeue();
                GameObject tmpSpawned = Instantiate(tmp, transform);

                if(size == 1)
                {
                    // only spawn one button, don't reposition
                }
                else if(i == 0)
                {
                    tmpSpawned.transform.position -= new Vector3(300, 0, 0);
                }
                else if (i == 1)
                {
                    tmpSpawned.transform.position -= new Vector3(100, 0, 0);
                }
                else if (i == 2)
                {
                    tmpSpawned.transform.position += new Vector3(100, 0, 0);
                }
                else if (i == 3)
                {
                    tmpSpawned.transform.position += new Vector3(300, 0, 0);
                }

                VisibleButtons.Enqueue(tmpSpawned);

            }

            return true;
        }
    }

    public void GenerateNewSequence(int size)
    {
        RandomizeSequence();

        for(int i = 0; i < size; i++)
        {
            Buttons.Enqueue(SequenceButtons[i]);
        }

        Debug.Log("Added " + size + " to Buttons(" + Buttons.Count + ")");
    }

    private void RandomizeSequence()
    {
        for (int i = 0; i < SequenceButtons.Count; i++)
        {
            GameObject temp = SequenceButtons[i];
            int randomIndex = Random.Range(i, SequenceButtons.Count);
            SequenceButtons[i] = SequenceButtons[randomIndex];
            SequenceButtons[randomIndex] = temp;
        }
    }

    public KeyCode GetCurrentButtonKey()
    {
        return VisibleButtons.Peek().GetComponent<ButtonController>().ButtonKeyCode;
    }

    public void DequeueCurrentButton()
    {
        GameObject go = VisibleButtons.Dequeue();
        go.GetComponent<ButtonController>().DestroyButton();
    }

    public bool IsEmpty()
    {
        return VisibleButtons.Count == 0;
    }

    
}
