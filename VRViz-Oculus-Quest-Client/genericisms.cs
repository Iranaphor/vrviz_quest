input_gameobject = "robot1"
input_component = "unityengine.transform"
input_modifier = "set_position"
input_msg = vrviz.msg

// Identify GAmeObject
Gameobject generic_gameobject = GameObject.Find(input_gameobject)
if (generic_gameobject == null) {
    generic_gameobject = new GameObject(input_gameobject);
}

// Define Component Type /* https://stackoverflow.com/a/1044474 */
Type generic_type = Type.GetType(input_component)

// Get Component
auto generic_component = generic_gameobject.GetComponent<generic_type>
if (generic_component == null) {
    generic_gameobject.AddComponent<generic_component>();
}


// Properties of method  /* https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags?view=net-5.0 */
int binding_flags = BindingFlags.Static | BindingFlags.Public;

//Call method to modify Component
MethodInfo generic_method = 
    vrviz.GetMethod(input_modifier, binging_flags);
generic_method.Invoke(generic_component, msg);


