using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class BodySourceView : MonoBehaviour 
{
    //public Material BoneMaterial;
    public GameObject BodySourceManager;
    public GameObject circle;
    public GameObject circle2;
    public GameObject footR;
    public GameObject footL;
    public GameObject footR2;
    public GameObject footL2;
    public AudioSource Left;
    public AudioSource right;
    public AudioClip rightClip;
    public AudioClip leftClip;
    bool leftOK;
    bool rightOK;

    //public Renderer rend;
    
    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;
    
    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };

    private void Start()
    {
        //circle = circle.gameObject.GetComponent<GameObject>();
        //rend = GetComponent<Renderer>();
        //rend.enabled = false;
    }


    void Update () 
    {
        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }
        
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                    //_Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                
                RefreshBodyObject(body);
            }
        }
    }
    
    /*private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            lr.SetVertexCount(2);
            lr.material = BoneMaterial;
            lr.SetWidth(0.05f, 0.05f);
            
            jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
        }
        
        return body;
    }*/
    
    private void RefreshBodyObject(Kinect.Body body) //, GameObject bodyObject
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;
            
            if(_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }
            
            //Transform jointObj = bodyObject.transform.Find(jt.ToString());
            //jointObj.localPosition = GetVector3FromJoint(sourceJoint);

            var distance = body.Joints[Kinect.JointType.SpineBase].Position;
            Vector3 points = new Vector3(- distance.X * 25, 7.0f, (distance.Z - 0.5f )* 20);
            circle.transform.position = points;

            var distance2 = body.Joints[Kinect.JointType.SpineBase].Position;
            Vector3 points2 = new Vector3(-distance2.X * 25, 7.0f, (distance2.Z + 3f) * 20);
            circle2.transform.position = points2;

            var FRdistance = body.Joints[Kinect.JointType.FootRight].Position;
            Vector3 pointsRF = new Vector3(-FRdistance.X * 25, (FRdistance.Y + 0.6f) * 5, FRdistance.Z * 20);
            footR.transform.position = pointsRF;
            
            var FLdistance = body.Joints[Kinect.JointType.FootLeft].Position;
            Vector3 pointsLF = new Vector3(-FLdistance.X * 25, (FLdistance.Y + 0.6f) * 5, FLdistance.Z * 20);
            footL.transform.position = pointsLF;
            

            Vector3 pointsRF2 = new Vector3(-FRdistance.X * 25, (FRdistance.Y + 0.8f) * 5, (FRdistance.Z + 3f) * 20);
            footR2.transform.position = pointsRF2;
            
            Vector3 pointsLF2 = new Vector3(-FLdistance.X * 25, (FLdistance.Y + 0.8f) * 5, (FLdistance.Z + 3f) * 20);
            footL2.transform.position = pointsLF2;

            

         //   Debug.Log("Left foot: " + pointsLF.normalized.y + "Right foot: " + pointsRF.y);
            //if (pointsRF.y > 22)
            //{
            //    rightOK = true;
            //}
            //if (pointsLF.y > 22)
            //{
            //    leftOK = true;
            //}

            //if ( pointsRF.y <= 21.8f && !right.isPlaying && rightOK)
            //{
            //    //right.Play();
            //   // right.PlayOneShot(rightClip);
            //    Debug.Log("right play");
            //    rightOK = false;
               
            //}
            /*else
            {
                right.Stop();
            }*/
            //if (pointsLF.y <= 21.8f && !Left.isPlaying && leftOK)
            //{
            //    //Left.PlayOneShot(leftClip);
            //    //Left.Play();
            //   Debug.Log("left play");
            //    leftOK = false;
            //}
            /*else
            {
                Left.Stop();
            }*/
            //LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if (targetJoint.HasValue)
            {
                //lr.SetPosition(0, jointObj.localPosition);
                //lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                //lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
                //Debug.Log("position x: " + FRdistance.X + "position y: " + FRdistance.Y + "position z: " + FRdistance.Z);
                //Debug.Log("left foot: " + pointsLF + "right foot: " + pointsRF);
            }
            else
            {
                //lr.enabled = false;
            }
        }
    }
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
