using UnityEngine;
using Newtonsoft.Json;
using vrviz.msg.std_msgs;
using System;

namespace VRViz.serialisers 
{
    public class UInt8Converter : JsonConverter
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
            UInt8 obj = new UInt8();
            Debug.LogError(reader.Value);
            obj.data = (byte)reader.Value;

            return obj;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(UInt8);
        }
    }
}