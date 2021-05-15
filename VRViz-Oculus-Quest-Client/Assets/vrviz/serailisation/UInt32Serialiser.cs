using System;
using UnityEngine;
using Newtonsoft.Json;
using std_msgs = vrviz.msg.std_msgs;


namespace VRViz.serialisers 
{
    public class BaseConverter : JsonConverter
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
            std_msgs.UInt32 obj = new std_msgs.UInt32();
            obj.data = Convert.ToUInt32(reader.Value);
            return obj;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(std_msgs.UInt32);
        }
    }


    public class UInt32Converter : BaseConverter {
        public override object ReadJson(JsonReader reader, Type objectType, object existing, JsonSerializer serializer){
            std_msgs.UInt32 obj = new std_msgs.UInt32();
            obj.data = Convert.ToUInt32(reader.Value);
            return obj;
        }
        public override bool CanConvert(Type objectType){
            return objectType == typeof(std_msgs.UInt32);
        }
    }
}