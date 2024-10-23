using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using VRViz.Connections;
using visualization_msgs = VRViz.Messages.visualization_msgs;
// using visualization_msgs = VRViz.interfaces.visualization_msgs.msgs;

using rviz_general = VRViz.plugins.rviz_default_plugins.general;
using rviz_plugins = VRViz.plugins.rviz_default_plugins.plugins;
using rviz_prefabs = VRViz.plugins.rviz_default_plugins.prefabs;

public class rviz_default_plugins_MarkerArray : rviz_prefabs.RvizPrefabBase
{

    public rviz_plugins.MarkerArray config_data;
    public visualization_msgs.MarkerArray message_data;
    public GameObject PointBall;


    public override void on_config_message(rviz_general.Display msg) {
        this.log("New config identified.");

        // save message to associated display
        this.config_data = (rviz_plugins.MarkerArray)msg;
        this.has_new_config = true;

        // Subscribe to the associated topic
        Debug.Log(this.initial_config);
        if (this.initial_config == true){
            this.log("initial config it is.");

            // subscribe to topic
            byte[] qos = new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
            string[] topic = new string[] { this.mqtt_namespace+"/TOPIC"+this.config_data.Topic.Value };
            this.mqtt_client.client.Subscribe(topic, qos);
            
            this.initial_config = false;
        }
    }
    
    public override void on_topic_message(MqttMsgPublishEventArgs msg) {
        this.log("New data identified.");

        // convert string to json object
        string msgdata = System.Text.Encoding.UTF8.GetString(msg.Message);
        Type msgtype = Type.GetType("VRViz.Messages.visualization_msgs.MarkerArray", true);
        var json = JsonConvert.DeserializeObject(msgdata, msgtype);

        // save message to associated display
        this.message_data = (visualization_msgs.MarkerArray)json;
        this.has_new_msg = true;
    }


    // Resond to recieved message
    public override void apply_new_config() {
        //spawn game object of arrow or axes and sets appearence
        this.log("new config being applied of type point");

        // pointball_handler handler = this.PointBall.GetComponent<pointball_handler>();

        // handler.Alpha = this.config_data.Alpha;
        // handler.Color = this.config_data.Color;
        // handler.Radius = this.config_data.Radius;
        // handler.HistoryLength = this.config_data.HistoryLength;
        
        // handler.SetConfig();
    }




    
    // Resond to recieved message
    public override void apply_new_msg() {
        this.log("new msg being applied of type point");
        this.has_new_msg = false;
        
        //string jsonString = JsonConvert.SerializeObject(this.message_data, Formatting.Indented);
        //Debug.Log("Re-Deserialized JSON object: " + jsonString);

        if (this.message_data == null){
            Debug.LogError("this.message_data.position is null");
            return;
        }
        
        //move ball to new position
        // this.PointBall.transform.localPosition = new Vector3(
            // (float)this.message_data.point.x.data, 
            // (float)this.message_data.point.z.data, 
            // (float)this.message_data.point.y.data);

        this.message_data = null;

    }


}




/*

{
  "markers": [
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/legend",
      "id": 0,
      "type": 9,
      "action": 0,
      "pose": {
        "position": {
          "x": 1.0,
          "y": 0.0,
          "z": 0.2
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.0,
        "y": 0.0,
        "z": 0.1
      },
      "color": {
        "r": 1.0,
        "g": 1.0,
        "b": 1.0,
        "a": 0.5
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "NavigateToPose",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/nodes",
      "id": 1,
      "type": 2,
      "action": 0,
      "pose": {
        "position": {
          "x": -4.91968262080432,
          "y": -1.2396582768847129,
          "z": 0.2
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.2,
        "y": 0.2,
        "z": 0.2
      },
      "color": {
        "r": 0.20000000298023224,
        "g": 0.20000000298023224,
        "b": 0.699999988079071,
        "a": 0.4000000059604645
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/names",
      "id": 2,
      "type": 9,
      "action": 0,
      "pose": {
        "position": {
          "x": -4.91968262080432,
          "y": -1.2396582768847129,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.0,
        "y": 0.0,
        "z": 0.12
      },
      "color": {
        "r": 0.30000001192092896,
        "g": 0.30000001192092896,
        "b": 0.30000001192092896,
        "a": 0.8999999761581421
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "WayPoint1",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/zones",
      "id": 3,
      "type": 4,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 0.699999988079071,
        "g": 0.10000000149011612,
        "b": 0.20000000298023224,
        "a": 0.800000011920929
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -5.04968262080432,
          "y": -1.0266582768847128,
          "z": 0.05
        },
        {
          "x": -5.16168262080432,
          "y": -1.180658276884713,
          "z": 0.05
        },
        {
          "x": -5.13268262080432,
          "y": -1.3696582768847128,
          "z": 0.05
        },
        {
          "x": -4.97868262080432,
          "y": -1.4816582768847129,
          "z": 0.05
        },
        {
          "x": -4.78968262080432,
          "y": -1.452658276884713,
          "z": 0.05
        },
        {
          "x": -4.67768262080432,
          "y": -1.2986582768847128,
          "z": 0.05
        },
        {
          "x": -4.70668262080432,
          "y": -1.109658276884713,
          "z": 0.05
        },
        {
          "x": -4.86068262080432,
          "y": -0.9976582768847129,
          "z": 0.05
        },
        {
          "x": -5.04968262080432,
          "y": -1.0266582768847128,
          "z": 0.05
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/edges",
      "id": 4,
      "type": 5,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 1.0,
        "g": 1.0,
        "b": 1.0,
        "a": 0.5
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -4.91968262080432,
          "y": -1.2396582768847129,
          "z": 0.0
        },
        {
          "x": -4.634097201850043,
          "y": -0.047272257914078054,
          "z": 0.0
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/nodes",
      "id": 5,
      "type": 2,
      "action": 0,
      "pose": {
        "position": {
          "x": -4.634097201850043,
          "y": -0.047272257914078054,
          "z": 0.2
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.2,
        "y": 0.2,
        "z": 0.2
      },
      "color": {
        "r": 0.20000000298023224,
        "g": 0.20000000298023224,
        "b": 0.699999988079071,
        "a": 0.4000000059604645
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/names",
      "id": 6,
      "type": 9,
      "action": 0,
      "pose": {
        "position": {
          "x": -4.634097201850043,
          "y": -0.047272257914078054,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.0,
        "y": 0.0,
        "z": 0.12
      },
      "color": {
        "r": 0.30000001192092896,
        "g": 0.30000001192092896,
        "b": 0.30000001192092896,
        "a": 0.8999999761581421
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "WayPoint2",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/zones",
      "id": 7,
      "type": 4,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 0.699999988079071,
        "g": 0.10000000149011612,
        "b": 0.20000000298023224,
        "a": 0.800000011920929
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -4.764097201850043,
          "y": 0.16572774208592195,
          "z": 0.05
        },
        {
          "x": -4.876097201850043,
          "y": 0.011727742085921943,
          "z": 0.05
        },
        {
          "x": -4.847097201850043,
          "y": -0.17727225791407805,
          "z": 0.05
        },
        {
          "x": -4.693097201850043,
          "y": -0.28927225791407807,
          "z": 0.05
        },
        {
          "x": -4.504097201850043,
          "y": -0.26027225791407804,
          "z": 0.05
        },
        {
          "x": -4.392097201850043,
          "y": -0.10627225791407804,
          "z": 0.05
        },
        {
          "x": -4.421097201850043,
          "y": 0.08272774208592196,
          "z": 0.05
        },
        {
          "x": -4.575097201850043,
          "y": 0.19472774208592195,
          "z": 0.05
        },
        {
          "x": -4.764097201850043,
          "y": 0.16572774208592195,
          "z": 0.05
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/edges",
      "id": 8,
      "type": 5,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 1.0,
        "g": 1.0,
        "b": 1.0,
        "a": 0.5
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -4.634097201850043,
          "y": -0.047272257914078054,
          "z": 0.0
        },
        {
          "x": -4.91968262080432,
          "y": -1.2396582768847129,
          "z": 0.0
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/edges",
      "id": 9,
      "type": 5,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 1.0,
        "g": 1.0,
        "b": 1.0,
        "a": 0.5
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -4.634097201850043,
          "y": -0.047272257914078054,
          "z": 0.0
        },
        {
          "x": -4.242298035328489,
          "y": 1.0011501745546092,
          "z": 0.0
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/nodes",
      "id": 10,
      "type": 2,
      "action": 0,
      "pose": {
        "position": {
          "x": -4.242298035328489,
          "y": 1.0011501745546092,
          "z": 0.2
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.2,
        "y": 0.2,
        "z": 0.2
      },
      "color": {
        "r": 0.20000000298023224,
        "g": 0.20000000298023224,
        "b": 0.699999988079071,
        "a": 0.4000000059604645
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/names",
      "id": 11,
      "type": 9,
      "action": 0,
      "pose": {
        "position": {
          "x": -4.242298035328489,
          "y": 1.0011501745546092,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.0,
        "y": 0.0,
        "z": 0.12
      },
      "color": {
        "r": 0.30000001192092896,
        "g": 0.30000001192092896,
        "b": 0.30000001192092896,
        "a": 0.8999999761581421
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "WayPoint3",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/zones",
      "id": 12,
      "type": 4,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 0.699999988079071,
        "g": 0.10000000149011612,
        "b": 0.20000000298023224,
        "a": 0.800000011920929
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -4.372298035328489,
          "y": 1.2141501745546093,
          "z": 0.05
        },
        {
          "x": -4.484298035328489,
          "y": 1.0601501745546091,
          "z": 0.05
        },
        {
          "x": -4.4552980353284894,
          "y": 0.8711501745546092,
          "z": 0.05
        },
        {
          "x": -4.3012980353284895,
          "y": 0.7591501745546092,
          "z": 0.05
        },
        {
          "x": -4.1122980353284895,
          "y": 0.7881501745546092,
          "z": 0.05
        },
        {
          "x": -4.000298035328489,
          "y": 0.9421501745546093,
          "z": 0.05
        },
        {
          "x": -4.029298035328489,
          "y": 1.1311501745546093,
          "z": 0.05
        },
        {
          "x": -4.183298035328489,
          "y": 1.2431501745546092,
          "z": 0.05
        },
        {
          "x": -4.372298035328489,
          "y": 1.2141501745546093,
          "z": 0.05
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/edges",
      "id": 13,
      "type": 5,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 1.0,
        "g": 1.0,
        "b": 1.0,
        "a": 0.5
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -4.242298035328489,
          "y": 1.0011501745546092,
          "z": 0.0
        },
        {
          "x": -4.634097201850043,
          "y": -0.047272257914078054,
          "z": 0.0
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/edges",
      "id": 14,
      "type": 5,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 1.0,
        "g": 1.0,
        "b": 1.0,
        "a": 0.5
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -4.242298035328489,
          "y": 1.0011501745546092,
          "z": 0.0
        },
        {
          "x": -3.164504953664164,
          "y": 0.674121252661821,
          "z": 0.0
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/edges",
      "id": 15,
      "type": 5,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 1.0,
        "g": 1.0,
        "b": 1.0,
        "a": 0.5
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -4.242298035328489,
          "y": 1.0011501745546092,
          "z": 0.0
        },
        {
          "x": -3.851311366399467,
          "y": 1.8800456447694345,
          "z": 0.0
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/nodes",
      "id": 16,
      "type": 2,
      "action": 0,
      "pose": {
        "position": {
          "x": -3.164504953664164,
          "y": 0.674121252661821,
          "z": 0.2
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.2,
        "y": 0.2,
        "z": 0.2
      },
      "color": {
        "r": 0.20000000298023224,
        "g": 0.20000000298023224,
        "b": 0.699999988079071,
        "a": 0.4000000059604645
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/names",
      "id": 17,
      "type": 9,
      "action": 0,
      "pose": {
        "position": {
          "x": -3.164504953664164,
          "y": 0.674121252661821,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.0,
        "y": 0.0,
        "z": 0.12
      },
      "color": {
        "r": 0.30000001192092896,
        "g": 0.30000001192092896,
        "b": 0.30000001192092896,
        "a": 0.8999999761581421
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "WayPoint4",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/zones",
      "id": 18,
      "type": 4,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 0.699999988079071,
        "g": 0.10000000149011612,
        "b": 0.20000000298023224,
        "a": 0.800000011920929
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -3.294504953664164,
          "y": 0.887121252661821,
          "z": 0.05
        },
        {
          "x": -3.406504953664164,
          "y": 0.7331212526618209,
          "z": 0.05
        },
        {
          "x": -3.377504953664164,
          "y": 0.544121252661821,
          "z": 0.05
        },
        {
          "x": -3.2235049536641642,
          "y": 0.432121252661821,
          "z": 0.05
        },
        {
          "x": -3.034504953664164,
          "y": 0.461121252661821,
          "z": 0.05
        },
        {
          "x": -2.922504953664164,
          "y": 0.615121252661821,
          "z": 0.05
        },
        {
          "x": -2.951504953664164,
          "y": 0.804121252661821,
          "z": 0.05
        },
        {
          "x": -3.105504953664164,
          "y": 0.916121252661821,
          "z": 0.05
        },
        {
          "x": -3.294504953664164,
          "y": 0.887121252661821,
          "z": 0.05
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/edges",
      "id": 19,
      "type": 5,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 1.0,
        "g": 1.0,
        "b": 1.0,
        "a": 0.5
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -3.164504953664164,
          "y": 0.674121252661821,
          "z": 0.0
        },
        {
          "x": -4.242298035328489,
          "y": 1.0011501745546092,
          "z": 0.0
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/edges",
      "id": 20,
      "type": 5,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 1.0,
        "g": 1.0,
        "b": 1.0,
        "a": 0.5
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -3.164504953664164,
          "y": 0.674121252661821,
          "z": 0.0
        },
        {
          "x": -2.046225759668267,
          "y": 0.28335467688057864,
          "z": 0.0
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/edges",
      "id": 21,
      "type": 5,
      "action": 0,
      "pose": {
        "position": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.1,
        "y": 0.0,
        "z": 0.0
      },
      "color": {
        "r": 1.0,
        "g": 1.0,
        "b": 1.0,
        "a": 0.5
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [
        {
          "x": -3.164504953664164,
          "y": 0.674121252661821,
          "z": 0.0
        },
        {
          "x": -2.857988581772375,
          "y": 1.6535341289917382,
          "z": 0.0
        }
      ],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    },
    {
      "header": {
        "stamp": {
          "sec": 0,
          "nanosec": 0
        },
        "frame_id": "map"
      },
      "ns": "/nodes",
      "id": 22,
      "type": 2,
      "action": 0,
      "pose": {
        "position": {
          "x": -2.046225759668267,
          "y": 0.28335467688057864,
          "z": 0.2
        },
        "orientation": {
          "x": 0.0,
          "y": 0.0,
          "z": 0.0,
          "w": 1.0
        }
      },
      "scale": {
        "x": 0.2,
        "y": 0.2,
        "z": 0.2
      },
      "color": {
        "r": 0.20000000298023224,
        "g": 0.20000000298023224,
        "b": 0.699999988079071,
        "a": 0.4000000059604645
      },
      "lifetime": {
        "sec": 0,
        "nanosec": 0
      },
      "frame_locked": false,
      "points": [],
      "colors": [],
      "texture_resource": "",
      "texture": {
        "header": {
          "stamp": {
            "sec": 0,
            "nanosec": 0
          },
          "frame_id": ""
        },
        "format": "",
        "data": []
      },
      "uv_coordinates": [],
      "text": "",
      "mesh_resource": "",
      "mesh_file": {
        "filename": "",
        "data": []
      },
      "mesh_use_embedded_materials": false
    }
......continued
  ]
}


*/