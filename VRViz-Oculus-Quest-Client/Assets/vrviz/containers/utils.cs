using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VRViz.Containers {

    public static class Utils {
        public static Dictionary<string, object> decodeDisplayDictionary(Dictionary<string, string> details){
            Dictionary<string, object> ret = new Dictionary<string, object>();
            foreach ( KeyValuePair<string, string> entry in details ){
                //Debug.Log("entry: " + entry.Key + " " + entry.Key);
                switch (entry.Key){

                    // Integer data types
                    case "decay_time":
                    case "size":
                        ret[entry.Key]=Convert.ToInt32(entry.Value); break;

                    // Float data types
                    case "alpha":
                    case "angular_tolerance":
                    case "position_tolerance":
                        ret[entry.Key]=Convert.ToDouble(entry.Value); break;

                    // boolean data types
                    case "normalize_range":
                        ret[entry.Key] = Convert.ToBoolean(entry.Value); break;

                    // list data types
                    case "colour":
                    case "flat_colour":
                        ret[entry.Key]= entry.Value.Split(',').ToList().ConvertAll<int>(x=>Convert.ToInt32(x)); break;

                    // string data types
                    default:
                        ret[entry.Key]= entry.Value; break;

                }
            }
            return ret;
        }

//        public static Dictionary<string, string> decodeTopicDictionary(Dictionary<string, string> details, Base container){
//            Dictionary<string, object> ret = new Dictionary<string, object>() {
//                { 'control_topic', '__dynamic_server' },
//                { 'mqtt_reference', '/vrviz' },
//                { 'frequency', '1.0' },
//                { 'latched', 'false' },
//                { 'qos', '2' }
//            };
//
//            foreach ( KeyValuePair<string, string> entry in details ) {
//                switch (entry.Key) {
//                    case "mqtt_reference":
//                        ret[entry.Key] = ret[entry.Key] + reference;
//                        break;
//                    case "latched":
//                        ret[entry.Key] = entry.Value ? "true" : "false";
//                        break;
//                ret[entry.Key]= entry.Value;
//            }
//
//            return ret;
//        }
    }

}