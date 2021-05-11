using UnityEngine;
using System;
using System.Reflection;
using System.Diagnostics;

namespace VRViz
{
    //private static string[] odom = new string[]{"__dynamic_server", "/odom","vrviz/odom", "nav_msgs.msg:Odometry"};
    public static class MessageLookup
    {
        public static Type GetMsgType(string message_type){
            string cls_nm = message_type.Split(':')[1];
            string pkg_nm = message_type.Split('.')[0];
            return Type.GetType("vrviz.msgs."+pkg_nm+"."+cls_nm);
        }
    } 

    public class MessageLookupTests{
        // [UnityTest]
        public void TestTypeBuild(){
            string msg_t = "nav_msgs.msg:Odometry";
            Type t = MessageLookup.GetMsgType(msg_t);
            // Assert.
        }
    }
    
}