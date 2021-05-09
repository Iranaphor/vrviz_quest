using UnityEngine;
using System;

namespace VRViz {
    namespace pocso {
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
        public class Stamp{
            public long nsecs;
            public long secs;
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
            
            [Serializable]
            public class ODOM_Header{
                public string frame_id;
                public int seq;
                public Stamp stamp;
            }

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
                public string child_frame_id;
                public ODOM_Header header;
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

            }

            [Serializable]
            public class Header{
                public string ns;
                public int id;
            }
        }       

    }
       
}

