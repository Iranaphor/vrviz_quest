using System;
using Newtonsoft.Json;
using stds = vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class Header {
		public int seq;
		[JsonIgnoreAttribute]
		public int stamp;
		public string frame_id;
	}
}
