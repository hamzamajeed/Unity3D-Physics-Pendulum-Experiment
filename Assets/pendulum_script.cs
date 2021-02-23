using System.Collections;
using System.Collections.Generic;
using Tajurbah_Gah;
using UnityEngine;
using UnityEngine.UI;

public class pendulum_script : MonoBehaviour
{
    public static pendulum_script instance;
    [SerializeField] Text textref;
    [SerializeField]public Text DraggedNoText;
    public static int DraggedNo = 0;
    GameObject sp1;
    GameObject ep;
    float length;
    Vector3 startPosition;//Start position of Bob
                          //// Start is called before the first frame update
                          //void Start()
                          //{
                          //   //SP is Start point and  EP is end point


    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    CheckForSteps();
    //}

    void SpecifyStringLength()
    {
        length = Mathf.Abs(ep.transform.position.y - sp1.transform.position.y);
        textref.text = "Length: " + length.ToString("f2");
    }
    //void CheckForSteps()
    //{
    //  //  Debug.Log(this.transform.position);

    //    if (((Vector3.Distance(this.transform.position, startPosition)) >= -0.7) || ((Vector3.Distance(this.transform.position, startPosition)) >= 0.7)){

    //        Debug.Log("in less than 5 position");
    //    }
    //    //if (transform.position.x > 0 && transform.position.x < 1)
    //    //{
    //    //    if (transform.position.y > 0 && transform.position.y < 3)
    //    //    {
    //    //        Debug.Log("Hello");
    //    //    }
    //    //}
    //}


    private Vector3 mOffset;
    private float mZCoord;
    bool isDragging = false;
    float firstRestriction = 0.8f;
    float SecondRestriction = 1f;
    float ThirdRestriction = 1.3f;
    public bool Restriction5End = false;
    public bool Restriction7End = false;
    public bool Restriction9End = false;
    private float sp = 0.0f;
    ObjectProperties objectProperties;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        Restriction5End = false;
        Restriction7End = true;
        Restriction9End = true;
        //   checkforUpdatedVal = false;
        objectProperties = GetComponent<ObjectProperties>() ? GetComponent<ObjectProperties>() : null;
        sp = transform.position.x;
        sp1 = GameObject.FindGameObjectWithTag("SP");
        ep = GameObject.FindGameObjectWithTag("EP");
        SpecifyStringLength();
        startPosition = this.transform.position;
    }

    public void OnMouseDown()
    {
        if (this.enabled)
        {
            //transform.position = GetMouseAsWorldPoint();
            isDragging = true;
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
   //         startPosition = gameObject.transform.position - GetMouseAsWorldPoint();
          
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
            transform.position = GetMouseAsWorldPoint() + mOffset;

            objectProperties?.ChangeRigidBodyToKinematic();
            objectProperties?.HighlightObjectOutline();
        }
    }

    private void Update()
    {
        
        if (isDragging)
        {
         if(Restriction5End == false && Restriction7End == true && Restriction9End == true)
            {
                if (transform.position.x < firstRestriction && transform.position.x > -firstRestriction)
                {
                    Debug.Log("position is positive " + transform.position.x);
                    transform.position = GetMouseAsWorldPoint() + mOffset;

                }
                else
                {
                    DetachFromDrag();
                    DraggedNo++;
                    //if(DraggedNo >= 0)
                    //{
                    //    Invoke("InvokeStopWatch", 4f);
                    //}
                    //  textref.gameObject.SetActive(true);
                    textref.text = "You cant move the Bob more than 5cm away from the origin position";
                    //DraggedNoText.text = "Bob Daragged " + DraggedNo;
                }
            }        
            
        else if(Restriction5End == true && Restriction7End == false && Restriction9End == true)
            {
                if (transform.position.x < SecondRestriction && transform.position.x > -SecondRestriction)
                {
                    Debug.Log("position is positive " + transform.position.x);
                    transform.position = GetMouseAsWorldPoint() + mOffset;

                }
                else
                {
                    DetachFromDrag();
                    DraggedNo++;
                    //if (DraggedNo >= 21)
                    //{
                    //    Invoke("InvokeStopWatch", 4f);
                    //}
                    //  textref.gameObject.SetActive(true);
                    textref.text = "You cant move the Bob more than 7cm away from the origin position";
                    //DraggedNoText.text = "Bob Daragged " + DraggedNo;
                }
            }
        else if(Restriction5End == true && Restriction7End == true && Restriction9End == false)
            {
                if (transform.position.x < ThirdRestriction && transform.position.x > -ThirdRestriction)
                {
                    Debug.Log("position is positive " + transform.position.x);
                    transform.position = GetMouseAsWorldPoint() + mOffset;

                }
                else
                {
                    DetachFromDrag();
                    DraggedNo++;
                    //if (DraggedNo >= 21)
                    //{
                    //    Invoke("InvokeStopWatch", 4f);
                    //}
                    //  textref.gameObject.SetActive(true);
                    textref.text = "You cant move the Bob more than 9cm away from the origin position";
                    //DraggedNoText.text = "Bob Daragged " + DraggedNo;
                }
            }     
       
          
            if (Input.GetMouseButtonUp(0))
            {
                DetachFromDrag();
            }
        }

    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void DetachFromDrag()
    {
        isDragging = false;
        objectProperties?.ChangeRigidBodyToDynamic();
        objectProperties?.UnHighlightObjectOutline();
        textref.gameObject.SetActive(true);
    }

    public void InvokeStopWatch()
    {
        StopwatchController.Instance.PauseStopWatch();
    }

}
