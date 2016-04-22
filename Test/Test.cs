﻿using UnityEngine;
using System.Collections;
using USS.Timers;
namespace USS.Timers
{
    public class Test : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            //Start simple repeater 
            Timer.Repeater(5f, x => Debug.Log("Repeater test"));
            //Start countdown and call Draw in 2.5 seconds
            Timer.Countdown(2.5f, Draw);
            //Start countdown and tell it to ignore Time Scale
            Timer.Countdown(10f, x => Debug.Log("Terminating Countdown #2")).SetIgnoreTimeScale(true);


            //Start a repeater, if we try to destroy it manually it wont take effect as countdown self destructs on reaching end.
            Timer timer = Timer.Countdown(1f, x => { Debug.Log("Destroyed timer:" + x.GetHashCode()); x.Destroy(); });
            //After we launched sphere repeater we will change its update speed in 8 seconds
            Timer.Countdown(8f, x => sphereRepeater.MainInterval = 0.01f);
            //Timer timer = Timer.Repeater(1f, x => { Debug.Log("Main interval is: " + x.MainInterval); });
            //timer.Destroy();

        }
        Timer sphereRepeater;
        void Draw(Timer timer)
        {
            //premake some data
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //Start new repeater with parameters that will update our data this is completely stupid in this scenario but it shows the usage.
            Debug.Log("Started RepositioningSphere");
            sphereRepeater = Timer.RepeaterParam(
                .1f
                , (x, y) => { Reposition(x, y); }
                , new object[1] { go });
        }

        //Casting is costly, this is just example
        void Reposition(Timer timer, object[] args)
        {
            Debug.Log("Updating data every " + timer.MainInterval + " seconds.");
            //Receive data and process it
            var go = args[0] as GameObject;

            var pos = go.transform.position;
            pos.y = Mathf.Sin(Time.time) * 2.5f;
            go.transform.position = pos;
        }
    }
}