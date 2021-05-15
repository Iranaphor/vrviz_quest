using System;
using UnityEngine;
using Newtonsoft.Json;
using std_msgs = vrviz.msg.std_msgs;


namespace VRViz.serialisers 
{
    public class StringConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("No need to Write");
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existing, JsonSerializer serializer){
            std_msgs.String obj = new std_msgs.String();
            Debug.LogError(reader.Value);
            obj.data = (string)reader.Value;

            return obj;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(std_msgs.String);
        }
    }
}