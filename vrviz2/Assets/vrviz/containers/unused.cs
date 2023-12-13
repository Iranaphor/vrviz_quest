using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using sensor_msgs = VRViz.Messages.sensor_msgs;
using nav_msgs = VRViz.Messages.nav_msgs;
using geometry_msgs = VRViz.Messages.geometry_msgs;
using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Containers_backup {

//    public class Laserscan : VRViz.Containers.Base {
//        public override string msg_type { get; set; } = "sensor_msgs/LaserScan";
//        public override string reference_type { get; set; } = "rostopic";
//
//        public Laserscan (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}
//        public override void ApplyMessage() { Debug.Log("Applying " + this.mqtt_reference); }
//    }
//
//    public class Map : VRViz.Containers.Base {
//        public override string msg_type { get; set; } = "nav_msgs/OccupancyGrid";
//        public override string reference_type { get; set; } = "rostopic";
//
//        public Map (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}
//        public override void ApplyMessage() { Debug.Log("Applying " + this.mqtt_reference); }
//    }
//
//    public class PoseArray : VRViz.Containers.Base {
//        public override string msg_type { get; set; } = "geometry_msgs/PoseArray";
//        public override string reference_type { get; set; } = "rostopic";
//
//        public PoseArray (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}
//        public override void ApplyMessage() { Debug.Log("Applying " + this.mqtt_reference); }
//    }
//
//    public class Pose : VRViz.Containers.Base {
//        public override string msg_type { get; set; } = "geometry_msgs/Pose";
//        public override string reference_type { get; set; } = "rostopic";
//
//        public Pose (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}
//        public override void ApplyMessage() { Debug.Log("Applying " + this.mqtt_reference); }
//    }
//
//    public class PoseWithCovarianceStamped : VRViz.Containers.Base {
//        public override string msg_type { get; set; } = "geometry_msgs/PoseWithCovarianceStamped";
//        public override string reference_type { get; set; } = "rostopic";
//
//        public PoseWithCovarianceStamped (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}
//        public override void ApplyMessage() { Debug.Log("Applying " + this.mqtt_reference); }
//    }
//
//    public class Path : VRViz.Containers.Base {
//        public override string msg_type { get; set; } = "nav_msgs/Path";
//        public override string reference_type { get; set; } = "rostopic";
//
//        public Path (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}
//        public override void ApplyMessage() { Debug.Log("Applying " + this.mqtt_reference); }
//    }
//
//    public class Polygon : VRViz.Containers.Base {
//        public override string msg_type { get; set; } = "geometry_msgs/Polygon";
//        public override string reference_type { get; set; } = "rostopic";
//
//        public Polygon (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}
//        public override void ApplyMessage() { Debug.Log("Applying " + this.mqtt_reference); }
//    }
//
//    public class RobotModel : VRViz.Containers.Base {
//        public override string msg_type { get; set; } = "std_msgs/String";
//        public override string reference_type { get; set; } = "rostopic";
////        public override string msg_type { get; set; } = "vrviz_msgs/urdf";
////        public override string reference_type { get; set; } = "rosparam";
//
//        public RobotModel (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}
//        public override void ApplyMessage() { Debug.Log("Applying " + this.mqtt_reference); }
//    }

}








































//
//
//
//using System;
//namespace Polymorphism {
//    class A     { public     void Test() { Console.WriteLine("A::Test()"); } }
//    class B : A { public new void Test() { Console.WriteLine("B::Test()"); } }
//    class C : B { public new void Test() { Console.WriteLine("C::Test()"); } }
//
//    class Program {
//        static void Main(string[] args) {
//            A a = new A();
//            B b = new B();
//            C c = new C();
//
//            a.Test(); // output --> "A::Test()"
//            b.Test(); // output --> "B::Test()"
//            c.Test(); // output --> "C::Test()"
//
//            a = new B();
//            a.Test(); // output --> "A::Test()"
//
//            b = new C();
//            b.Test(); // output --> "B::Test()"
//
//            Console.ReadKey();
//        }
//    }
//}
//
//
