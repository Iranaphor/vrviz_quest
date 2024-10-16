using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using geometry_msgs = VRViz.Messages.geometry_msgs;
using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;

public class covariance_handler : MonoBehaviour
{

    public GameObject PositionCylinder;
    public GameObject OrientationCone;

    public rviz_utils::Orientation Orientation;
    public rviz_utils::Position Position;
    public bool Value;

    public void SetConfig(){

        // Apply Configuration to Position Cylinder
        this.PositionCylinder.SetActive(this.Position.Value);
        this.PositionCylinder.transform.localScale = new Vector3(1, 0.025f, 1);

        // Apply Configuration to Orientation Cone
        this.OrientationCone.SetActive(this.Orientation.Value);

    }

    // public void ApplyMsg(geometry_msgs::Point position, geometry_msgs::Quaternion orientation){

    //     // Update positions for Arrow/Axes and Covariance
    //     Vector3 vector = new Vector3((float)position.x.data, (float)position.z.data, (float)position.y.data);       this.ArrowAxes.transform.localPosition = new 
    //     this.ArrowAxes.transform.localPosition = vector;
    //     this.Covariance.transform.localPosition = vector;

    //     // Update Arrow/Axes orientation
    //     Quaternion quaternion = new Quaternion(
    //         (float)orientation.x.data, 
    //         (float)orientation.y.data, 
    //         (float)orientation.z.data, 
    //         (float)orientation.w.data);
    //     Vector3 euler;

    //     euler = inputQuaternion.eulerAngles;
    //     euler.x = 0.0f;
    //     euler.y = -euler.z;
    //     euler.z = 0.0f;
    //     this.ArrowAxes.transform.localRotation = Quaternion.Euler(euler);

    //     euler = inputQuaternion.eulerAngles;
    //     euler.x = 0.0f;
    //     euler.y = -euler.z;
    //     euler.z = 0.0f;
    //     this.Covariance.transform.localRotation = Quaternion.Euler(euler);

    //     // Update size of covariance bubble
    //     // float scale;
    //     // scale = handler.Position.Scale;
    //     // handler.PositionCylinder.transform.localScale = new Vector3(
    //     //     (float)this.message_data.pose.pose.position.x.data * scale, 
    //     //     (float)0.01f, 
    //     //     (float)this.message_data.pose.pose.position.y.data * scale);
    //     // scale = handler.Orientation.Scale;
    //     // handler.OrientationCone.transform.localScale = new Vector3(
    //     //     (float)this.message_data.pose.pose.position.x.data * scale, (float)0.5f, (float)0.01f);

    // }
}

/*
Check out how it looks on the broker:

      
*/