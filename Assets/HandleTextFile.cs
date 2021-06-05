using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Linq;

public class HandleTextFile : MonoBehaviour
{
    public GameObject speechBubble;
    public GameObject thinkBubble;
    public Button GreenFlag;
    public int myVariable = 60;

    public TextAsset textt;
    string s;
    private List<string> eachLine;
    private List<string> eachWord;
    private ArrayList Blocks = new ArrayList();
    private ArrayList NoEventsBlocks = new ArrayList();
    private ArrayList ControlBlocks = new ArrayList();
    private ArrayList ForeverBlocks = new ArrayList();
    ArrayList blocksInsideEvent;

    private int index;
    private bool timesFlag = false;
    private int times;
    private bool condition = false;
    private int foreverIndex;

    //--------------------------------------------------- start ----------------------------------------------------//
    void Start()
    {
        StartCoroutine(EventsBlocks(0f));

        //s = textt.text;
        //ArrayList d = new ArrayList();
        //d.AddRange(s.Split("\n"[0]));
        //StartCoroutine(MainFunc(d, 3.0f));

        //StartCoroutine(LooksBlocks("Say", "Hello!", 0, 5));
        //StartCoroutine(LooksBlocks("Say", "Hello!", 3, 5f));
        //StartCoroutine(LooksBlocks("Think", "Hmm...", 0, 5f));
        //StartCoroutine(LooksBlocks("Think", "Hmm...", 3, 5f));

       


    }

    //--------------------------------------------------- GF ----------------------------------------------------//
    public void GF()
    {
        Debug.Log("Green flag up");

        blocksInsideEvent = GetBlocksInEvent("GF");
        if (blocksInsideEvent.Count > 0)
        {
            //Debug.Log("Count is more than zero");
            StartCoroutine(MainFunc(blocksInsideEvent, 1f));
        }
    }

    //--------------------------------------------------- update ---------------------------------------------------//
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("key a pressed");

            blocksInsideEvent = GetBlocksInEvent("key a");
            if (blocksInsideEvent.Count > 0)
            {
                //Debug.Log("Count is more than zero");
                StartCoroutine(MainFunc(blocksInsideEvent, 1f));
            }
        }


        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    Debug.Log("Green flag up");
        //    blocksInsideEvent = GetBlocksInEvent("GF");
        //    if (blocksInsideEvent.Count > 0)
        //    {
        //        //Debug.Log("Count is more than zero");
        //        StartCoroutine(MainFunc(blocksInsideEvent, 1f));
        //    }
        //}


        if (ForeverBlocks.Count > 0)
        {
            Debug.Log("--> In update forever");
            StartCoroutine(MainFunc(ForeverBlocks, 0f));
        }

    }


    //------------------------------------------- get blocks inside event ------------------------------------------//
    private ArrayList GetBlocksInEvent(String keyName)
    {
       // Debug.Log("Get blocks in event");
        ArrayList output = new ArrayList();
        for (int i = 0; i < Blocks.Count; i++)
        {
            ArrayList a = (ArrayList)Blocks[i];
           // Debug.Log("a[0] --> " + a[0].ToString());

            if (a[0].ToString().Contains(keyName))
            {
                
              //  Debug.Log("arrayList size = " + a.Count);
                return a;
            }

        }
        return output;
    }


    //------------------------------------------- get blocks inside event ------------------------------------------//
    private ArrayList GetBlocksInControl(String keyName)
    {
        // Debug.Log("Get blocks in event");
        ArrayList output = new ArrayList();
        for (int i = 0; i < Blocks.Count; i++)
        {
            ArrayList a = (ArrayList)Blocks[i];
            // Debug.Log("a[0] --> " + a[0].ToString());

            if (a[0].ToString().Contains(keyName))
            {

                //  Debug.Log("arrayList size = " + a.Count);
                return a;
            }

        }
        return output;
    }


    //---------------------------------------------------Main Func--------------------------------------------------//
    // main function that prints out the pseudo code in the console 
    //and perform the actions on the game object
    IEnumerator MainFunc(ArrayList array, float delay)
    {
        Debug.Log("array count in main = " + array.Count);
        //Debug.Log(array.ToString());

        for (int i = 0; i < array.Count; i++)
        {    
           // Debug.Log(array[i].ToString());
            eachWord = new List<string>();
            eachWord.AddRange(array[i].ToString().Split(' '));
            int temp;
            float tempf1;
            float tempf2;

            //  Debug.Log("each word length = " + eachWord.Count);
            // Debug.Log("Each word [0] = " + eachWord[0]);

            ////------ Motion Blocks --------

            if (eachWord[0].ToLower() == "move")
            {
                // debug.log("first word " + eachword[0]);
                temp = (int)(Int16.Parse(eachWord[1]) * 0.25);
                Debug.Log("move " + temp + " steps");


                StartCoroutine(MotionBlocks(0, 0, 0, "", "move", temp, 0.0f));
                yield return new WaitForSeconds(delay);

            }

            if (eachWord[0].ToLower() == "turn")
            {
                if (eachWord[1] == "left")
                {
                    //debug.log("first word " + eachword[0]);
                    tempf1 = (float)(float.Parse(eachWord[2]));
                    Debug.Log("turn left by " + tempf1 + " degrees");

                    StartCoroutine(MotionBlocks(0, 0, tempf1, "left", "turn", 0, 0.0f));
                }
                else if (eachWord[1] == "right")
                {
                    //  debug.log("first word " + eachword[0]);
                    tempf1 = (float)(float.Parse(eachWord[2]));
                    Debug.Log("turn right by " + tempf1 + " degrees");

                    StartCoroutine(MotionBlocks(0, 0, tempf1, "right", "turn", 0, 0.0f));
                }

                yield return new WaitForSeconds(delay);

            }

            if (eachWord[0].ToLower() == "go")
            {
                tempf1 = (float)(float.Parse(eachWord[3]) * 0.25);
                tempf2 = (float)(float.Parse(eachWord[6]) * 0.25);
                Debug.Log("go by x: " + tempf1 + " and y :" + tempf2);

                StartCoroutine(MotionBlocks(tempf1, tempf2, 0, "", "go", 0, 0.0f));
                yield return new WaitForSeconds(delay);

            }

            if (eachWord[0].ToLower() == "change")
            {
                if (eachWord[1] == "x")
                {
                    //  debug.log("first word " + eachword[0]);
                    tempf1 = (float)(float.Parse(eachWord[3]) * 0.25);
                    Debug.Log("change x by " + tempf1);

                    StartCoroutine(MotionBlocks(tempf1, 0, 0, "", "changeX", 0, 0.0f));
                }
                else if (eachWord[1] == "y")
                {
                    //   debug.log("first word " + eachword[0]);
                    tempf1 = (float)(float.Parse(eachWord[3]) * 0.25);
                    Debug.Log("change y by " + tempf1);

                    StartCoroutine(MotionBlocks(0, tempf1, 0, "", "changeY", 0, 0.0f));
                }

                yield return new WaitForSeconds(delay);


            }

            if (eachWord[0].ToLower() == "set")
            {
                if (eachWord[1] == "x")
                {
                    // debug.log("first word " + eachword[0]);
                    tempf1 = (float)(float.Parse(eachWord[3]) * 0.25);
                    Debug.Log("set x by " + tempf1);

                    StartCoroutine(MotionBlocks(tempf1, 0, 0, "", "setX", 0, 0.0f));
                }
                else if (eachWord[1] == "y")
                {
                    // debug.log("first word " + eachword[0]);
                    tempf1 = (float)(float.Parse(eachWord[3]) * 0.25);
                    Debug.Log("set y by " + tempf1);

                    StartCoroutine(MotionBlocks(0, tempf1, 0, "", "setY", 0, 0.0f));
                }

                yield return new WaitForSeconds(delay);

            }


            ////------- Looks Blocks ---------

            if (eachWord[0].ToLower() == "say")
            {
                //Debug.Log(">>> "+ array[i]);

                if (array[i].ToString().Contains("for"))
                {
                    string[] input = array[i].ToString().Split(' ');
                    int index = input.Count() - 3;
                    //Debug.Log(input[index]);
                    // Debug.Log(eachWord[input.Count() - 2]);
                    tempf1 = (float)(float.Parse(eachWord[input.Count() - 2]));
                    string str = "";
                    for (int k = 1; k < index; k++)
                    {
                        str += " " + input[k];
                    }
                    //  Debug.Log("Say"+ str + " for " + tempf1 + " seconds");

                    StartCoroutine(LooksBlocks("Say", str, tempf1, 0f));
                }
                else
                {
                    string str = "";
                    for (int k = 1; k < eachWord.Count; k++)
                    {
                        str += " " + eachWord[k];
                    }
                    //   Debug.Log(str);

                    StartCoroutine(LooksBlocks("Say", str, 0, 3f));
                }

                yield return new WaitForSeconds(delay);


            }

            if (eachWord[0].ToLower() == "think")
            {
                // Debug.Log(">>> " + array[i]);

                if (array[i].ToString().Contains("for"))
                {
                    string[] input = array[i].ToString().Split(' ');
                    int index = input.Count() - 3;
                    //Debug.Log(input[index]);
                    // Debug.Log(eachWord[input.Count() - 2]);
                    tempf1 = (float)(float.Parse(eachWord[input.Count() - 2]));
                    string str = "";
                    for (int k = 1; k < index; k++)
                    {
                        str += " " + input[k];
                    }
                    //  Debug.Log("Think" + str + " for " + tempf1 + " seconds");

                    StartCoroutine(LooksBlocks("Think", str, tempf1, 0f));
                }
                else
                {
                    string str = "";
                    for (int k = 1; k < eachWord.Count; k++)
                    {
                        str += " " + eachWord[k];
                    }
                    // Debug.Log(str);

                    StartCoroutine(LooksBlocks("Think", str, 0, 3f));
                }

                yield return new WaitForSeconds(delay);


            }


            ////------- Control Blocks ---------

            if (eachWord[0].ToLower() == "if")
            {
                
                string check = eachWord[5];
                bool condition = CheckCondition(array[i].ToString());
                //Debug.Log("check -->  " + check);
                //Debug.Log("condition -->  " + condition);
                if (check == "then")  // if else 
                {
                    if (condition)
                    {
                        //Debug.Log(">>> salma condition = " + condition);
                        ArrayList arr1 = new ArrayList();
                        for (int a = i + 1; a < array.Count; a += 1)
                        {
                            arr1.Add(array[a]);
                            //Debug.Log("------------>>> " + array[a]);
                        }
                        StartCoroutine(MainFunc(arr1, delay));
                        break;
                    }
                    else
                    {
                        //Debug.Log(">>> lina else ");
                        int counter = 0;
                        for (int a = i + 1; a < array.Count; a++)
                        {
                            List<string> eachWord2 = new List<string>();
                            eachWord2.AddRange(array[a].ToString().Split(' '));
                            if (eachWord2[0].ToLower() == "if")
                            {
                                counter += 1;
                            }
                            if (eachWord2[0].Contains("Else") && eachWord2.Count == 1)
                            {
                                if (counter > 0)
                                {
                                    counter -= 1;
                                }
                                else
                                {
                                    ArrayList arr1 = new ArrayList();
                                    for (int a1 = a + 1; a1 < array.Count; a1++)
                                    {
                                        arr1.Add(array[a1]);
                                    }
                                  //  Debug.Log(">>> bala7 ");
                                    StartCoroutine(MainFunc(arr1, delay));
                                    break;
                                }
                            }
                        }

                    }
                    break;
                }


                else if (check == "start")  // if
                {
                    if (condition)
                    {
                        //Debug.Log(">>> salma condition = " + condition);
                        ArrayList arr1 = new ArrayList();
                        for (int a = i + 1; a < array.Count; a += 1)
                        {
                            arr1.Add(array[a]);
                            //Debug.Log("------------>>> " + array[a]);
                        }
                        StartCoroutine(MainFunc(arr1, delay));
                        break;
                    }
                    else
                    {
                        Debug.Log(">>> lina else ");
                        int counter = 0;
                        for (int a = i + 1; a < array.Count; a++)
                        {
                            List<string> eachWord2 = new List<string>();
                            eachWord2.AddRange(array[a].ToString().Split(' '));
                            if (eachWord2[0].ToLower() == "if")
                            {
                                counter += 1;
                            }
                            if (eachWord2[0].Contains("END") && eachWord2[1].Contains("IF") && eachWord2.Count == 2)
                            {
                                if (counter > 0)
                                {
                                    counter -= 1;
                                }
                                else
                                {
                                    ArrayList arr1 = new ArrayList();
                                    for (int a1 = a + 1; a1 < array.Count; a1++)
                                    {
                                        arr1.Add(array[a1]);
                                    }
                                    Debug.Log(">>> bala7 ");
                                    StartCoroutine(MainFunc(arr1, delay));
                                    break;
                                }
                            }
                        }

                    }
                    break;


                }
            }

            if (eachWord[0].Contains("Else") && eachWord.Count == 1)
            {
                //Debug.Log(">>> Mayar in else ");
                int counter = 0;

                for ( int m = i; m < array.Count; m++)
                {
                    List<string> eachWord2 = new List<string>();
                    eachWord2.AddRange(array[m].ToString().Split(' '));
                    if (eachWord2[0].ToLower() == "if")
                    {
                        counter += 1;
                    }
                    else
                    {
                        //Debug.Log(array[m].ToString());
                        if (array[m].ToString().Contains("End if else"))
                        {
                            //Debug.Log("hiiiiiiii ");
                            if (counter > 0)
                            {
                                counter -= 1;
                            }
                            else
                            {
                                ArrayList arr1 = new ArrayList();
                                for (int a1 = m + 1; a1 < array.Count; a1+=1)
                                {
                                    arr1.Add(array[a1]);
                                   // Debug.Log("a1 === " + array[a1]);
                                }
                                StartCoroutine(MainFunc(arr1, delay));
                                break;
                            }
                        }
                    }
                }
                break;

            }

            if (eachWord[0].ToLower() == "wait" && eachWord[1].ToLower() == "for")
            {
                int seconds = (int)(Int16.Parse(eachWord[2]));
                Debug.Log("wait for " + seconds + " seconds");
                yield return new WaitForSeconds(seconds);
            }

            if (eachWord[0].ToLower() == "wait" && eachWord[1].ToLower() == "until")
            {
                bool condition = CheckCondition(array[i].ToString());
                Debug.Log("wait until condition" );
                while (!condition)
                {
                    //Debug.Log("xxxxxxxxxxx     "+myVariable + condition);
                    condition = CheckCondition(array[i].ToString());
                    yield return new WaitForSeconds(1f);
                }

            }

            if (eachWord[0].ToLower() == "repeat")
            {
                int counter = 0;
                for (int a = i + 1; a < array.Count; a++)
                {
                    List<string> eachWord2 = new List<string>();
                    eachWord2.AddRange(array[a].ToString().Split(' '));
                    if (eachWord2[0].ToLower() == "repeat")
                    {
                        counter += 1;
                    }
                    if (eachWord2[0].Contains("until") )
                    {
                        Debug.Log(">> in until");
                        Debug.Log("counter = " + counter);
                        if (counter > 0)
                        {
                            counter -= 1;
                        }
                        else
                        {
                            Debug.Log(">> in until else");
                            index = a;
                            break;
                        }
                    }
                    if (eachWord2[1].Contains("times"))
                    {
                        Debug.Log(">> in times");
                        Debug.Log("counter = " + counter);
                        if (counter > 0)
                        {
                            counter -= 1;
                        }
                        else
                        {
                            Debug.Log(">> in times else");
                            index = a;
                            timesFlag = true;
                            times = (int)(Int16.Parse(eachWord2[0]));
                            break;

                        }
                    }
                }

                Debug.Log("index = " + index);
                ArrayList arr1 = new ArrayList();
                for (int a1 = i + 1; a1 < index; a1++)
                {
                    arr1.Add(array[a1]);
                }
                Debug.Log("arr1 length " + arr1.Count);
                if (arr1.Count > 0)
                {
                    if (!timesFlag)
                    {
                        condition = CheckCondition(array[index].ToString());
                        Debug.Log("condition -->" + condition);
                        while (!condition)
                        {
                            condition = CheckCondition(array[index].ToString());
                            StartCoroutine(MainFunc(arr1, delay));
                            yield return new WaitForSeconds(1f);
                        }
                    }
                    else
                    {
                        Debug.Log(">> times =" +times);
                        for(int z = 0; z< times; z++)
                        {
                            StartCoroutine(MainFunc(arr1, delay));
                            yield return new WaitForSeconds(1f);
                        }
                        timesFlag = false;
                    }
                }
                i = index;

            }

            if (eachWord[0].ToLower() == "while" && eachWord[1].ToLower() == "true")
            {
                for (int a1 = i + 1; a1 < array.Count; a1++)
                {
                    List<string> eachWord2 = new List<string>();
                    eachWord2.AddRange(array[a1].ToString().Split(' '));
                    if (!(eachWord2[0].ToLower() == "end" && eachWord2[1].ToLower() == "forever"))
                    {
                        if (eachWord2.Contains("Say") && eachWord2.Contains("for") || eachWord2.Contains("Think") && eachWord2.Contains("for"))
                        {
                            Debug.Log(" for forever --> say for or think for");

                            List<string> s = new List<string>();
                            s.AddRange(array[a1].ToString().Split(' '));
                            string j = "";
                            if(eachWord2[0] == "Say")
                            {
                                j += "Say";
                            }
                           else 
                            {
                                j += "Think";
                            }
                            for (int l = 0; l < s.Count - 3; l++)
                            {
                                j += " "+ s[l];
                            }
                            ForeverBlocks.Add(j);
                            Debug.Log(">>> new in forever === "+ j );
                        }
                        else
                        {
                            ForeverBlocks.Add(array[a1]);

                        }

                    }
                    else
                    {
                        foreverIndex = a1;
                        break;
                    }


                }
                i = foreverIndex;
               // Debug.Log("forever blocks length " + ForeverBlocks.Count);
            }

        }

    }


    //-------------------------------------------------check condition----------------------------------------------//
    bool CheckCondition(string condition)
    {
        bool check = false;
        string operand1 = "";
        string operand2 = "";
        string operators = "";

        //int operands = (int)Int16.Parse(operand);

        eachWord = new List<string>();
        eachWord.AddRange(condition.ToString().Split(' '));

        if (eachWord[2] == ">" || eachWord[2] == "<" || eachWord[2] == "=" || eachWord[2] == "and" || eachWord[2] == "or" || eachWord[2] == "not" )
        {
            operand1 = eachWord[1];
            operand2 = eachWord[3];
            operators = eachWord[2]; 
        }
        else if (eachWord[3] == ">" || eachWord[3] == "<" || eachWord[3] == "=" || eachWord[3] == "and" || eachWord[3] == "or" || eachWord[3] == "not")
        {
            operand1 = eachWord[2];
            operand2 = eachWord[4];
            operators = eachWord[3];
        }
        else if (eachWord[4] == ">" || eachWord[4] == "<" || eachWord[4] == "=" || eachWord[4] == "and" || eachWord[4] == "or" || eachWord[4] == "not")
        {
            operand1 = eachWord[3];
            operand2 = eachWord[5];
            operators = eachWord[4];
        }

        //Debug.Log(">>>>>>>>> operand 1 = "+ operand1);
        //Debug.Log(">>>>>>>>> operand 2 = " + operand2);
        //Debug.Log(">>>>>>>>> operator = " + operators);

        int numb1;
        int numb2;
        if(int.TryParse(operand1, out numb1))
        {
            //Debug.Log(">>>>>>>>>>>>>>>>>>>>>>> in case 1");
            
            switch (operators)
            {
                case ">": return numb1 > myVariable;
                case "<": return numb1 < myVariable;
                case "=": return numb1 == myVariable;
                //case "and": bool f = numb1 && myVariable; return f; 
                //case "or": return numb1 > myVariable; 
                //case "not": return numb1 > myVariable; 
            }
        }
        else if (int.TryParse(operand2, out numb2))
        {
            //Debug.Log(">>>>>>>>>>>>>>>>>>>>>>> in case 2");

            switch (operators)
            {
                case ">": return myVariable > numb2;
                case "<": return myVariable < numb2;
                case "=": return myVariable == numb2;
            }

        }
        return check;
    }


    //---------------------------------------------------Event Block-----------------------------------------------//
    // event blocks extract the blocks inside the event till it hits "end event" 
    // then add those extracted blocks as a new cell in the Block ArrayList
    IEnumerator EventsBlocks(float delay)
    {
        yield return new WaitForSeconds(delay);
        s = textt.text;
        eachLine = new List<string>();
        eachLine.AddRange(s.Split("\n"[0]));

        for (int i = 0; i < eachLine.Count; i++)
        {
            ArrayList eventBlocks = new ArrayList();
           // ArrayList NotEvent = new ArrayList();

            eachWord = new List<string>();
            eachWord.AddRange(eachLine[i].Split(' '));
           
            if (eachWord[0].ToLower() == "when")
            {
                for (int j = i; j < eachLine.Count; j++)
                {
                   // Debug.Log(eachLine[j].ToLower());
                    if (!eachLine[j].ToLower().Contains("event"))
                    {
                        eventBlocks.Add(eachLine[j]);
                    }
                    else
                    {
                       // Debug.Log(">>>>>>>>>>>>>>>>>> end");
                        break;
                    }
                }

                Blocks.Add(eventBlocks);

            }
            else
            {
                //// Linaaa
                //NotEvent.Add(eachLine[i]);
                //Blocks.Add(NotEvent);

                // Mayarrr
                NoEventsBlocks.Add(eachLine[i]);
            }

        }


        if (Blocks.Count > 0)
        {
            //Debug.Log(Blocks[0].GetType());
        }
        else Debug.Log("empty");

    }


    //---------------------------------------------------Motion Block----------------------------------------------//
    IEnumerator MotionBlocks(float x, float y, float degree, string direction, string blockName, int steps, float delay)
    {
        if (blockName == "move")
        {
            gameObject.transform.Translate(steps, 0f, 0f);
            yield return new WaitForSeconds(delay);
        }

        if (blockName == "turn")
        {
            if (direction == "left")
            {
                gameObject.transform.Rotate(0f, -degree, 0f);
            }
            else if (direction == "right")
            {
                gameObject.transform.Rotate(0f, degree, 0f);

            }
        }

        if (blockName == "go")
        {
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }

        if (blockName == "changeX")
        {
            gameObject.transform.Translate(x, 0f, 0f, Space.World);
        }
        if (blockName == "changeY")
        {
            gameObject.transform.Translate(0f, y, 0f, Space.World);
        }

        if (blockName == "setX")
        {
            gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (blockName == "setY")
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);
        }

    }

   
    //---------------------------------------------------Looks Block-----------------------------------------------//
    IEnumerator LooksBlocks(string blockName, string word, float seconds ,float delay)
    {
        if (blockName == "Say")
        {
            Debug.Log("seconds === " +seconds);
            speechBubble.SetActive(true);
            Text t = speechBubble.GetComponentsInChildren<Text>()[0];
            t.text = word;

            if(seconds > 0)
            {
            yield return new WaitForSeconds(seconds);
            speechBubble.SetActive(false);
            }
        }

        if (blockName == "Think")
        {
            thinkBubble.SetActive(true);
            Text t = thinkBubble .GetComponentsInChildren<Text>()[0];
            t.text = word;

            if (seconds > 0)
            {
                yield return new WaitForSeconds(seconds);
                thinkBubble.SetActive(false);
            }
            //  Debug.Log(t.text);

        }




    }


    //---------------------------------------------------Looks Block-----------------------------------------------//
    IEnumerator ControlsBlocks(float delay)
    {
        yield return new WaitForSeconds(delay);
        s = textt.text;
        eachLine = new List<string>();
        eachLine.AddRange(s.Split("\n"[0]));

        for (int i = 0; i < eachLine.Count; i++)
        {
            ArrayList eventBlocks = new ArrayList();
            // ArrayList NotEvent = new ArrayList();

            eachWord = new List<string>();
            eachWord.AddRange(eachLine[i].Split(' '));

            if (eachWord[0].ToLower() == "Repeat")
            {
                for (int j = i; j < eachLine.Count; j++)
                {
                    // Debug.Log(eachLine[j].ToLower());
                    if (!eachLine[j].ToLower().Contains("until"))
                    {
                        ControlBlocks.Add(eachLine[j]);
                    }
                    else
                    {
                        // Debug.Log(">>>>>>>>>>>>>>>>>> end");
                        break;
                    }
                }

                Blocks.Add(eventBlocks);

            }
            else
            {
                //// Linaaa
                //NotEvent.Add(eachLine[i]);
                //Blocks.Add(NotEvent);

                // Mayarrr
                NoEventsBlocks.Add(eachLine[i]);
            }

        }


        if (Blocks.Count > 0)
        {
            //Debug.Log(Blocks[0].GetType());
        }
        else Debug.Log("empty");

    }



}