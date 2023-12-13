using UnityEngine;
using System;

namespace VRViz {
    namespace pocso {

        [Serializable]
        public class Stamp{
            public long nsecs;
            public long secs;
        }
            
        [Serializable]
        public class Header{
            public string frame_id;
            public int seq;
            public Stamp stamp;
        }

        [Serializable]
        public class Position{
            public float x;
            public float y;
            public float z;
        }

        [Serializable]
        public class Orientation{
            public float x;
            public float y;
            public float z;
            public float w;
        }

        [Serializable]
        public class Pose{
            public Orientation orientation;
            public Position position;
        }

        [Serializable]
        public class Twist{
            public Angular angular;
            public Linear linear;
        }

        [Serializable]
        public class Angular{
            public float x;
            public float y;
            public float z;
        }

        [Serializable]
        public class Linear{
            public float x;
            public float y;
            public float z;
        }

        [Serializable]
        public class Vector_Three{
            public float x;
            public float y;
            public float z;
        }

        [Serializable]
        public class Point{
            public float x;
            public float y;
            public float z;
        }

        [Serializable]
        public class Color_RGBA{
            public float r;
            public float g;
            public float b;
            public float a;
        }

        [Serializable]
        public class Color_RGB{
            public float r;
            public float g;
            public float b;
        }


        namespace odom{
            
            // [Serializable]
            // public class ODOM_Header{
            //     public string frame_id;
            //     public int seq;
            //     public Stamp stamp;
            // }

            [Serializable]
            public class CovariancePose{
                public float[] covariance;
                public Pose pose;
                
            }
            
            [Serializable]
            public class CovarianceTwist{
                public float[] covariance;
                public Twist twist;
            }

            [Serializable]
            public class ODOM_Message{
                public Header header;
                public string child_frame_id;
                public CovariancePose pose;
                public CovarianceTwist twist;
            }

        }

        namespace marker{
            [Serializable]
            public class Marker{
                public Header header;
                public Pose pose;
                public Vector_Three scale;
                public Color_RGBA color;
                public int duration;
                public bool frame_locked;

                public Point[] points;
                public Color_RGBA[] colors;
                public string text;
                public string mesh_resource;
                public bool mesh_use_embedded_materials;
                public string ns;
                public int id;
                public int type;
                public int action;
            }

            // [Serializable]
            // public class Header{
            //     public string frame_id;
            //     public Stamp stamp;
            // }
        }       



        namespace image{

            [Serializable]
            public class IMAGE_Message{
                public Header header;
                public string child_frame_id;
                public uint height;
                public uint width;
                public string encoding;
                public byte is_bigendian;
                public uint step;
                public string data;
            }
        }


        namespace compressed_image{

            [Serializable]
            public class COMPRESSED_IMAGE_Message{
                public Header header;
                public string format;
                public string data;
            }
        }

        namespace posestamped{

            [Serializable]
            public class POSE_STAMPED_Message{
                public Header header;
                public Pose pose;
            }
        }



/*
geometry_msgs/PoseStamped

geometry_msgs/Pose pose
  geometry_msgs/Point position
    float64 x
    float64 y
    float64 z
  geometry_msgs/Quaternion orientation
    float64 x
    float64 y
    float64 z
    float64 w
*/


    }
       
}


/*
for each message:
define new file of message_name in folder message_package
line 0-M -> using vrviz.message.<MESSAGE_PACKAGE>; for each message package type needed
line M+1 -> namespace vrviz.messages.<MESSAGE_PACKAGE>{
line M+2 -> [Serializable]
line M+3 -> public class <MESSAGE_NAME>{
lines (M+4)-(N-2) -> public $(roscat MESSAGE_PACKAGE MESSAGE_FILE.msg);
line N-1 -> }
line N -> }

e.g.
Point Message:
namespace vrviz.messages.geometry_msgs{
    [Serializable]
    public class Point{
        public float64 x;
        public float64 y;
        public float64 z;
    }
}

Pose Message
namespace vrviz.messages.geometry_msgs{
    [Serializable]
    public class Point{
        public Positon position;
        public Quaternion orientation;
    }
}

Position Message:
namespace vrviz.messages.geometry_msgs{
    [Serializable]
    public class Point{
        public float64 x;
        public float64 y;
        public float64 z;
    }
}


*/


