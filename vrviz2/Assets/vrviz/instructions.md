# System Pipeline
Detailsed here is the outline on how the data moves through the system. There are many files and folders in the workspace so there may be some complexity.


## Step 1. Pipeline.cs Startup
in the start, it connects to mqtt
when connected it subscribes to ns/meta to recieve the .rviz file

when a message comes in, it is passed to the on_message function
this is passed into the if condition where the rviz file is processed
in this, checks data is valid and loops through each display included
for each display, it checks if there is a name, topic and value
if a prefab for it already exists, it passes it the config details
otherwise it adds the details to a buffer for processing

## Step 2. Pipeline.cs Update
in the update loop it checks if there are prefabs needing generating
if there is, it loops through each one identifying is as,
"prefabs/rviz_default_plugins_" + classType
it instantiates the prefab, and sets its parent to the Table
it then applies the config details to it

## Step 3. rviz_default_plugins_Pose.cs on_config_message
on creation of the prefab, properties need to be applied to it
this cannot be done here as the type management gets in the way
instead, the details are saved and a flag set for the Update loop
in this stage though, it also initialises a direct subscriber
this is so the mqtt messages can be handelled internally
back when the prefab was created, 

## Step 4. default_prefab_behavious.cs Update
This is the base class for all display prefabs
in the function, it checks if there are updates to configs or data
if either one, it will apply the data
this is configured as such so the system can process new data inline rather than stalling in callbacks

## Step 5. rviz_default_plugins_Pose.cs apply_new_config
In this function, the configuration properties need to be set
this includes things like colour, shae, rendering object, etc.

## Step 6. Pipeline.cs Update
following the creation of each required prefab,
the system processes newly recieved data from the queue
firstly the system copies and resets the queue
then it loops through each message which was recieved
it identifies the associated prefab to the topic
then passess the data into the prefab via the on_topic_message

## Step 7. rviz_default_plugins_Pose.cs on_topic_message
in this method, the mqtt data is recieved,
it is firtsly parsed to the appropriate message format
then it is saved and the flag set to process it in the update loop

## Step 8. default_prefab_behavious.cs Update
the update loop, seeing the flag, triggers the apply_new_msg method

## Step 9. rviz_default_plugins_Pose.cs apply_new_msg
In this function, the data properties are set
this includes things like position, orientation, or data




# For creating a new rviz_default_plugin
Outlines here are the instructions for creating an interface in Unity for an rviz_default_plugin.

## Step 1: Configure Plugin Data Store
create a copy of the rviz_default_plugins/plugins/default.cs,
using the name of the display type to render
add the properties which appear on the rviz display
if any new ones are used, add them inside utils/rviz_utils
by creating a copy of the default.cs file there

## Step 2: Create the Data Data Store
create a msg file within vrviz/msgs following the conventions used for the other files there
most have already been generated so reference what is available to create what is needed

## Step 3: Configure the Data Handler
this script is used to manage the application of an incoming message
this includes both configuiration file changes and data application
create a copy of rviz_default_plugins/prefab_scripts/rviz_default_plugins_default.cs,

## Step 4: Create the gameobject prefab
In Unity, in the scene create a new gameobject with 'Create Empty'
rename it to match the format of: rviz_default_plugins_DEFAULT
open the project folder vrviz/rviz_default_plugins/Recources/prefabs
if there are any rendering objects which are desired,
drag them now into the prefab from this folder
click on the object and in the inspector click add component
in the search bar, enter the name of the data handler script
drag the prefabs attached to the gameobject to their respective locations on the script's public gameobjects
drag the new gameobject into the prefabs folder
delete it from the scene
