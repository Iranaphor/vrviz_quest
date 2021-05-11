using System;
using System.Text;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class Image {
		public std_msgs::Header header;
		public int height;
		public std_msgs::UInt32 width;
		public std_msgs::String encoding;
		public std_msgs::UInt8 is_bigendian;
		public std_msgs::UInt32 step;
		
		public string data; 
		// {
			// set { byteData = Encoding.UTF8.GetBytes(value); }
			// get { return Encoding.UTF8.GetString(byteData); }
		// }
		// protected byte[] byteData;


	}
}


/*
class Person
{
  private string name; // field
  public string Name   // property
  {
    get { return name; }
    set { name = value; }
  }
}


*/