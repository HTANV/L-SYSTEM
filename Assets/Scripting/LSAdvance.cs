using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LSAdvance : MonoBehaviour
{
    /// <summary>
    /// This component holds information for nodes this is use to build turtle graphics
    /// </summary>
    public class TurtleTransform
    {
        public Vector3 Position;
        public Quaternion Angle;
    }

    [Header("Spawning Settings")]
    //This is starter for iterations
    public string Axiom = "A";
    //This is the lastly generated iterations
    private string LastContent = string.Empty;
    //This is the object which contains LineRenderer to display what is going on
    public GameObject SpawnableObject;
    //Generations stands for Iterations - simply it telling L-Sys to create how many branches
    public int Generations = 4;

    [Header("Transforming Settings")]
    //Angle is used to modify the direction of each branch created by L-sys
    public float Angle = 30f;
    //Size stands for the length of each branch
    public float Size = 2f;

    //All below ui elements are helping the system to run
    [Header("UI Elements")]
    public InputField GenerationsInput;
    public Slider Thikness;
    public Slider SizeSlider;
    public Slider AngleSlider;
    public Camera camera;
    public Text StatusText;
    public Toggle FastResult;
    //The list of all generated nodes in one loop
    private Stack<TurtleTransform> Nodes = new Stack<TurtleTransform>();
    //This is the value we take step forward
    private float MovingForward = 2f;
    //Direction we move forward in 2D aspect we go up for forward other then in 3D space Z-axis is forward axis
    private Vector3 MovingForwardDir = Vector3.up;
    //Rules are the patterns of L-System
    //There is a default pattern added in list
    //Using dictionary for rules because while iteration it helps to get the leader sentence easily without extra loops
    private Dictionary<char, string> Rules = new Dictionary<char, string>()
    {
        { 'A', "[F-[A+A]+F[+FA]-A]" },
        { 'F', "FF" }
    };
    //These are generated game objects we can use this list for reseting the generated content
    private List<GameObject> NodesObjectsList = new List<GameObject>();

    //initial values - all initial values are use to reset the changes in runtime
    private int _generation = 0;
    private float _angle = 0;
    private float _size = 0;
    private Dictionary<char, string> _rules = new Dictionary<char, string>();

    //This dictionary contains the petterns we imported using file 
    private Dictionary<char, string> _importedRules = new Dictionary<char, string>();
    /// <summary>
    /// Generating and returning the new node object
    /// </summary>
    private GameObject InitNewNode
    {
        get
        {
            var g = Instantiate(SpawnableObject, Vector3.zero, Quaternion.identity, transform);
            return g;
        }
    }
    /// <summary>
    /// This is the Helper return type function to return filtered iteration as per previous iteration
    /// </summary>
    /// <returns></returns>
    private string GetNewIterations
    {
        get
        {
            //Setting inital value to iterations as axiom which is our starter
            var _iterations = Axiom;
            //using string builder to build new iteration
            StringBuilder _iterationsBuilder;
            //Generating the iteration as per generations value
            for (int i = 0; i < Generations; i++)
            {
                //reseting the string builder object to giving it new iteration string
                _iterationsBuilder = new StringBuilder();
                //this loop handling the previous iteration to create new according to chars in it
                //checking the each char in sentence to build new itertaion
                foreach (var c in _iterations)
                {
                    _iterationsBuilder.Append(Rules.ContainsKey(c) ? Rules[c] : c.ToString());
                }

                _iterations = _iterationsBuilder.ToString();
            }
            return _iterations;
        }
    }
    /// <summary>
    /// This function controlling the Status text which is displaying the information what is going on
    /// </summary>
    private void SetStatus(string status)
    {
        StatusText.text = status;
    }
    /// <summary>
    /// Zooming camera with slider
    /// </summary>
    public void CameraZoom(Single sing)
    {
        camera.orthographicSize = sing;
        camera.transform.position = new Vector3(0, sing - 4f, -140);
    }
    /// <summary>
    /// Unity's default function once called on when program runs or in editor we press play button
    /// </summary>
    private void Start()
    {
        _generation = Generations;
        _angle = Angle;
        _size = Size;
        _rules = Rules;
        SizeSlider.value = Size;
        AngleSlider.value = Angle;
    }
    //Update is unity's function to update according to the fps
    //We will use it for HotKeys
    private void Update()
    {
        //Open file dialog
        if(Input.GetKeyDown(KeyCode.O))
        {
            SelectFile();
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            Generate();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomGenerate();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ResetCurrentProcess();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            ResetRules();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectFile("Rule_A.txt");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectFile("Rule_B.txt");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectFile("Rule_C.txt");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectFile("Rule_D.txt");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectFile("Rule_E.txt");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectFile("Rule_F.txt");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectFile("Rule_G.txt");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectFile("Rule_H.txt");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectFile("Rule_I.txt");
        }
    }
    /// <summary>
    /// Reseting the rules and all variables to their initial value
    /// </summary>
    public void ResetRules()
    {
        Rules = _rules;
        Generations = _generation;
        Angle = _angle;
        Size = _size;
        SizeSlider.value = _size;
        AngleSlider.value = _angle;

        SetStatus("EVERTYTHING IS RESET TO DEFAULT!");
    }
    /// <summary>
    /// Handling the open rules file directly
    /// </summary>
    public void SelectFile(string fileName)
    {
        string file = Application.dataPath.Replace("/Assets", string.Empty) + "/Patterns/" + fileName;
        if (file == string.Empty || file == null)
        {
            return;
        }

        string content = File.ReadAllText(file);
        string[] Lines = content.Split('\n');

        _importedRules = new Dictionary<char, string>();

        foreach (var l in Lines)
        {
            char _key = l.Split(':')[0][0];
            string _value = l.Split(':')[1];

            _importedRules.Add(_key, _value);
        }


        SetStatus("NEW PATTERN ADDED\n" + content);
    }
    /// <summary>
    /// Handling the open rules file
    /// </summary>
    public void SelectFile()
    {
        string file = EditorUtility.OpenFilePanel("LS-System", Application.dataPath.Replace("/Assets", string.Empty), "txt");
        if (file == string.Empty || file == null)
        {
            return;
        }

        string content = File.ReadAllText(file);
        string[] Lines = content.Split('\n');

        _importedRules = new Dictionary<char, string>();

        foreach (var l in Lines)
        {
            char _key = l.Split(':')[0][0];
            string _value = l.Split(':')[1];

            _importedRules.Add(_key, _value);
        }


        SetStatus("NEW PATTERN ADDED\n" + content);
    }
    /// <summary>
    /// Reseting the current process, it stops the on going process and clear all generated data
    /// </summary>
    public void ResetCurrentProcess()
    {
        StopAllCoroutines();
        if (NodesObjectsList.Count > 0)
        {
            foreach (var g in NodesObjectsList.ToArray())
            {
                Destroy(g);
            }
        }
        NodesObjectsList.Clear();
        Nodes.Clear();
        transform.position = Vector3.zero; 
        transform.rotation = Quaternion.identity;

        SetStatus("PROCESS STOPPED");
    }
    /// <summary>
    /// This handles to Generate L-Sys using given rules 
    /// </summary>
    public void Generate()
    {
        //Stopping the current process
        ResetCurrentProcess();
        //Checking if there are imported rules via file
        if(_importedRules.Count>0)
        {
            Rules = _importedRules;
        } 

        //if rules list is empty, reset the rules to default pattern
        if(Rules.Count == 0)
        {
            Rules = _rules;
        }
        //Getting the generating value from ui to parse in integer
        Generations = int.Parse(GenerationsInput.text);

        StartGenerationProcess();
    }
    /// <summary>
    /// Making values random for variations
    /// </summary>
    public void RandomGenerate()
    {
        //Random angle between 15 and 60, 15 is much straight and 60 is not much closer to the sharp turn
        Angle = Random.Range(15, 60);
        //Random length of branches
        Size = Random.Range(0.3f, 0.75f);
        //Random generation(iterations)
        int rg = Random.Range(3, 6);
        GenerationsInput.text = rg.ToString();

        AngleSlider.value = Angle;
        SizeSlider.value = Size;

        Generate();
    }
    /// <summary>
    /// Starting live generation
    /// </summary>
    private void StartGenerationProcess()
    {
        //Sending the axiom for filtering and getting new generation iteration
        LastContent = GetNewIterations;
        //getting the status for Watching live result or instant result 
        if (FastResult.isOn)
        {
            //This loop Handling the each character in newly generated Iterations
            for (int i = 0; i < LastContent.Length; i++)
            {
                //This is empty object just to helping the iterations
                if (LastContent[i] == 'A') continue;

                _Generate(LastContent[i]);
            }
        }
        else
        {
            //Starting the coroutine to show the result
            StartCoroutine(LiveGeneration());
        }
        //Displaying the status that system is generating the content
        SetStatus("GENERATING\n" + Rules[Axiom[0]] + "\nWITH " + Generations + " ITERATIONS"); ;
    }
    /// <summary>
    /// This is coroutine that showing a live build with small delay in generating new objects
    /// </summary>
    IEnumerator LiveGeneration()
    {
        //This loop Handling the each character in newly generated Iterations
        for (int i = 0; i < LastContent.Length; i++)
        {
            //This is empty object just to helping the iterations
            if (LastContent[i] == 'A') continue;

            _Generate(LastContent[i]);

            yield return new WaitForSeconds(0.001f);
        }
    }
    private void _Generate(char _char)
    {
        //here also checking again generated iterations characters
        //Switch handles the character type and doing the thier job
        switch (_char)
        {

            case 'F'://In standard rules F is moving node forward
                     //Getting the start position for node
                var StartPosition = transform.position;
                //Getting new node - it generate the gameobject and returning here
                var _node = InitNewNode;
                //Getting the linerenderer component from newly generated node to apply the properties
                var _lr = _node.GetComponent<LineRenderer>();
                //After applying all moving the node forward
                MoveForward();
                //Setting the line properties
                _lr.SetPosition(0, StartPosition);
                _lr.SetPosition(1, transform.position);
                _lr.startWidth = Thikness.value;
                _lr.endWidth = Thikness.value;
                _lr.startColor = Color.red;

                //adding the lastly created node to nodes-gameobjects list
                NodesObjectsList.Add(_node);

                break;
            case '['://This char commands L-sys to create new node
                     //Turtle object to hold the information about new node to be created
                var nt = new TurtleTransform()
                {
                    Angle = transform.rotation,
                    Position = transform.position
                };
                //Pushing this node in pile of nodes
                Nodes.Push(nt);

                break;
            case ']'://This char commands L-sys to Closing the node
                     //Getting the last node from stack and remove it from stack
                TurtleTransform T_Object = Nodes.Pop();
                //Applying the positiong and rotation to parent
                transform.position = T_Object.Position;
                transform.rotation = T_Object.Angle;

                break;
            case '+'://This char is stands for rotating Clockwise
                     //Adding the angle in Clockwise as per given value
                Angle = AngleSlider.value;
                //Applying tranform to parent
                transform.Rotate(Vector3.back * Angle);

                break;
            case '-'://This char is stands for rotating Anti-Clockwise
                     //Adding the angle in Anti-Clockwise as per given value
                Angle = AngleSlider.value;
                //Applying tranform to parent
                transform.Rotate(Vector3.forward * Angle);
                break;
        }
    }
    /// <summary>
    /// Translate the transform to step forward for placing new node
    /// </summary>
    private void MoveForward()
    {
        //Getting size from the slider value
        Size = SizeSlider.value;
        //applying the transformation to the parent object
        transform.Translate(MovingForwardDir * MovingForward * Size);
    }
}
