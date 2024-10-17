using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;

namespace VRViz.plugins.rviz_default_plugins.general {
    public abstract class Display {

        public string Class;

        public bool Enabled;

        public string Name;

		public rviz_utils::Topic Topic;

        public bool Value;

    }
}



/*
Axes:
------------------
0     t   Pose

1     t   Map
1 s   t   Odometry
1     t   PointStamped

2 s   t   LaserScan
2     t   MarkerArray
2     t   Path
2       f TF

3 s   t   Image
3     t   InteractiveMarkers
3     t   PoseWithCovariance

  s     f Camera
    a t   Effort
  s   t   FluidPressure
        f Grid
      t   GridCells
  s   t   Illuminance
      t   Marker
  s   t   PointCloud2
      t   Polygon
      t   PoseArray
  s   t   Range
  s   t   RelativeHumidity
        f RobotModel
  s   t   Temperature
    a t   Wrench

x s   t   PointCloud
------------------
*/