using System;
using vrviz.msg.std_msgs;
using vrviz.msg.sensor_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class CameraInfo {
		public Header header;
		public std_msgs.UInt32 height;
		public std_msgs.UInt32 width;
		public std_msgs.String distortion_model;
		public std_msgs.Float64[] D;
		public std_msgs.Float64[] K;
		public std_msgs.Float64[] R;
		public std_msgs.Float64[] P;
		public std_msgs.UInt32 binning_x;
		public std_msgs.UInt32 binning_y;
		public RegionOfInterest roi;
	}
}
