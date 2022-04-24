using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;

using sensor_msgs = VRViz.Messages.sensor_msgs;
using nav_msgs = VRViz.Messages.nav_msgs;
using geometry_msgs = VRViz.Messages.geometry_msgs;
using std_msgs = VRViz.Messages.std_msgs;

using VRViz.Modifiers;

namespace VRViz.Containers {

    public class Image : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "sensor_msgs/Image";
        public override string reference_type { get; set; } = "rostopic";

        public GameObject frame;
        public RawImage raw_image;

        public Image (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {
            Profiler.BeginSample("VRViz.Containers::Image.Image");
            this.frame = GameObject.Find("Display_3_panel");
            this.raw_image = this.frame.GetComponent(typeof(RawImage)) as RawImage;
            Profiler.EndSample();
        }
        public override void ApplyMessage() {
            Profiler.BeginSample("VRViz.Containers::Image.ApplyMessage");
            Debug.Log("Applying " + this.mqtt_reference);
            sensor_msgs::Image data = (sensor_msgs.Image)this.message_data;
            VRViz.Modifiers.MessageApplications.SetImage(data, this.raw_image);
            Profiler.EndSample();
        }

//        sensor_msgs::Image data = null;
//        public override void ApplyMessage() { //frame_stagger
//            Profiler.BeginSample("VRViz.Containers::Image.ApplyMessage");
//            if (this.data == null)
//                 { this.ApplyMessage1(); this.message_data = null; }
//            else { this.ApplyMessage2(); this.data = null; }
//            Profiler.EndSample();
//        }
//        public void ApplyMessage1() {
//            Profiler.BeginSample("VRViz.Containers::Image.ApplyMessage1");
//            this.data = (sensor_msgs.Image)this.message_data;
//            Profiler.EndSample();
//        }
//        public void ApplyMessage2() {
//            Profiler.BeginSample("VRViz.Containers::Image.ApplyMessage2");
//            VRViz.Modifiers.MessageApplications.SetImage(this.data, this.raw_image);
//            Profiler.EndSample();
//        }
    }

    public class Odometry : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "nav_msgs/Odometry";
        public override string reference_type { get; set; } = "rostopic";

        public GameObject frame;
        public Transform tf;

        public Odometry (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {
            Profiler.BeginSample("VRViz.Containers::Odometry.Odometry");
            this.frame = GameObject.Find("RobotModel");
            this.tf = this.frame.GetComponent(typeof(Transform)) as Transform;
            Profiler.EndSample();
        }

        public override void ApplyMessage() {
            Profiler.BeginSample("VRViz.Containers::Odometry.ApplyMessage");
            nav_msgs::Odometry data = (nav_msgs.Odometry)this.message_data;
            Debug.Log("Applying " + this.mqtt_reference);
            VRViz.Modifiers.MessageApplications.SetPose(data.pose.pose, this.tf);
            Profiler.EndSample();
        }
    }

    public class PointCloud2 : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "sensor_msgs/PointCloud2";
        public override string reference_type { get; set; } = "rostopic";

        public GameObject frame;
        public Transform tf;

        public Transform base_link_tf;
        public Transform buffer;
        public GameObject marker_prefab;
        public GameObject[] markers;

        public PointCloud2 (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {
            Profiler.BeginSample("VRViz.Containers::PointCloud2.PointCloud2");

            //Identify the robot's Transform
            this.base_link_tf = GameObject.Find("RobotModel").transform;

            Profiler.EndSample();
        }


        public bool first=true;
        public void First (sensor_msgs::PointCloud2 data) {
            Profiler.BeginSample("VRViz.Containers::PointCloud2.First");
            this.first=false;

            // Identify the frame by which the Container is relative to
            this.tf = this.DefineFrame(this.base_link_tf, data.header.frame_id.data);
            this.tf.localPosition = new Vector3(0f, 0.1f, -1f);
            this.tf.localRotation = Quaternion.Euler(90,0,0);

            // Create a buffer for holding all the PointCloud pixel panels
            this.buffer = this.DefineFrame(this.tf, "point_cloud").transform;
            this.buffer.localPosition = new Vector3(0f, 0f, 0f);
            this.buffer.localRotation = Quaternion.Euler(0,180,0);
            this.buffer.localScale = new Vector3(0.01f,0.01f,0.01f);

            this.first=false;
            Profiler.EndSample();
        }

        public override void ApplyMessage() {
            Profiler.BeginSample("VRViz.Containers::PointCloud2.ApplyMessage");

            // Convert the POSCO to a PointCloud object
            sensor_msgs::PointCloud2 data = (sensor_msgs.PointCloud2)this.message_data;
            if (this.first) { this.First(data);

            // Do any required initialisation
            Debug.Log("Applying " + this.mqtt_reference);

            // Display the PointCloud
            VRViz.Modifiers.MessageApplications.SetPointCloud2(data, this.buffer, "panel-");
            }

            Profiler.EndSample();
        }
    }
}

            //instantiate a collection of objects to manipulate
            //this.buffer = Instantiate(buffer_prefab, new Vector3(0, 0, 0), Quaternion.identity);


/*
 public class ProjectileRenderer : MonoBehaviour
     {
         [Header("Data")]
         public ConquestBattleConfiguration Config;
         public Mesh mesh;
         public Material material;
         public float life;
         public float speed;
         public float damage;

         [Header("Instances")]
         public List<ProjectileData> projectiles = new List<ProjectileData>();
         public List<GameObject> splashPool = new List<GameObject>();

         //Working values
         private RaycastHit[] rayHitBuffer = new RaycastHit[1];
         private Vector3 worldPoint;
         private Vector3 transPoint;
         private List<Matrix4x4[]> bufferedData = new List<Matrix4x4[]>();

         public void SpawnProjectile(Vector3 position, Quaternion rotation, int team, float damageScale)
         {
             ProjectileData n = new ProjectileData();
             n.pos = position;
             n.rot = rotation;
             n.scale = Vector3.one;
             n.experation = life;
             n.team = team;
             n.damage = damage;
             n.damageScale = damageScale;

             projectiles.Add(n);
         }

         private void Update()
         {
             UpdateProjectiles(Time.deltaTime);
             BatchAndRender();
         }

         private void BatchAndRender()
         {
             //If we dont have projectiles to render then just get out
             if (projectiles.Count <= 0)
                 return;

             //Clear the batch buffer
             bufferedData.Clear();

             //If we can fit all in 1 batch then do so
             if (projectiles.Count < 1023)
                 bufferedData.Add(projectiles.Select(p => p.renderData).ToArray());
             else
             {
                 //We need multiple batches
                 int count = projectiles.Count;
                 for (int i = 0; i < count; i += 1023)
                 {
                     if (i + 1023 < count)
                     {
                         Matrix4x4[] tBuffer = new Matrix4x4[1023];
                         for(int ii = 0; ii < 1023; ii++)
                         {
                             tBuffer[ii] = projectiles[i + ii].renderData;
                         }
                         bufferedData.Add(tBuffer);
                     }
                     else
                     {
                         //last batch
                         Matrix4x4[] tBuffer = new Matrix4x4[count - i];
                         for (int ii = 0; ii < count - i; ii++)
                         {
                             tBuffer[ii] = projectiles[i + ii].renderData;
                         }
                         bufferedData.Add(tBuffer);
                     }
                 }
             }

             //Draw each batch
             foreach (var batch in bufferedData)
                 Graphics.DrawMeshInstanced(mesh, 0, material, batch, batch.Length);
         }

         private void UpdateProjectiles(float tick)
         {
             foreach(var projectile in projectiles)
             {
                 projectile.experation -= tick;

                 if (projectile.experation > 0)
                 {
                     //Sort out the projectiles 'forward' direction
                     transPoint = projectile.rot * Vector3.forward;
                     //See if its going to hit something and if so handle that
                     if (Physics.RaycastNonAlloc(projectile.pos, transPoint, rayHitBuffer, speed * tick, Config.ColliderLayers) > 0)
                     {
                         projectile.experation = -1;
                         worldPoint = rayHitBuffer[0].point;
                         SpawnSplash(worldPoint);
                         ConquestShipCombatController target = rayHitBuffer[0].rigidbody.GetComponent<ConquestShipCombatController>();
                         if (target.teamId != projectile.team)
                         {
                             target.ApplyDamage(projectile.damage * projectile.damageScale, worldPoint);
                         }
                     }
                     else
                     {
                         //This project wont be hitting anything this tick so just move it forward
                         projectile.pos += transPoint * (speed * tick);
                     }
                 }
             }
             //Remove all the projectiles that have hit there experation, can happen due to time or impact
             projectiles.RemoveAll(p => p.experation <= 0);
         }

         private void SpawnSplash(Vector3 worlPoint)
         {
             //TODO: implament spawning of your splash effect e.g. the visual effect of a projectile hitting something
         }
     }

     public class ProjectileData
     {
         public Vector3 pos;
         public Quaternion rot;
         public Vector3 scale;
         public float experation;
         public int team;
         public float damage;
         public float damageScale;

         public Matrix4x4 renderData { get { return Matrix4x4.TRS(pos, rot, scale); } }
     }
*/