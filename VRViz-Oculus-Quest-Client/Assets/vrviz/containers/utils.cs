using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VRViz.Containers {

    public static class Utils {
        public static Dictionary<string, object> decodeDictionary(Dictionary<string, string> details){
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

//        public Dictionary<string, object> parallelMagic(Dictionary<string, object> inputs){
//            inputs.AsParallel().ForAll(pair => decode(pair.Key, pair.Value));
//            return inputs;
//        }


//        public object decode(string key, string value){
//            switch (key){
//                case "alpha":               // Integer data types
//                case "decay_time":
//                case "size":
//                    return Convert.ToInt32(value);
//
//                case "angular_tolerance":   // Float data types
//                case "position_tolerance":
//                    return Convert.ToFloat32(value);
//
//                case "normalize_range":     // boolean data types
//                    return Convert.ToBoolean(value);
//
//                case "colour":              // list data types
//                case "flat_colour":
//                    return value.split(",").ToList().ConvertAll<int>(x=>Convert.ToInt32(x));
//
//                default:                    // string data types
//                    return value;
//            }
//        }
    }

//    public class DefaultValues {
//        public string topic;
//        public string param = "/robot_description";
//        public float alpha = 1;
//        public float angular_tolerance = 0.1;
//        public List<int> colour = new List<int> { 255, 0, 0 };
//        public string color_scheme = "raw";
//        public int decay_time = 10;
//        public string display = "2";
//        public List<int> flat_colour = new List<int> { 255, 0, 0 };
//        public bool normalize_range = true;
//        public float position_tolerance = 0.1;
//        public string shape = "arrow";
//        public int size = 3;
//        public string style = "points";
//    }

}