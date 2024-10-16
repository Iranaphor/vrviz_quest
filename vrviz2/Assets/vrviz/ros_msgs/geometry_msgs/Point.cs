using std_msgs = VRViz.Messages.std_msgs;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
    
namespace VRViz.Messages.geometry_msgs {
	public class Point {
		public std_msgs::Float64 x;
		public std_msgs::Float64 y;
		public std_msgs::Float64 z;
		public static string ToRosString() { return "geometry_msgs.msg:Point"; }
	}
}