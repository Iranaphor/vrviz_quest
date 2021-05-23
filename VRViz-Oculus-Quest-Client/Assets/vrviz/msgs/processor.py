import re
import os
import subprocess
import argparse
import shutil
from pprint import pprint
from multiprocessing import Pool


# Remapping for std_msgs.msg primatives to C# primatives
# https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types
type_map = {
	"uint8":  "byte",#byte
	"uint16": "ushort",#ushort
	"uint32": "uint",#uint
	"uint64": "ulong",#ulong
	"int8":  "sbyte",#sbyte
	"int16": "short",#short
	"int32": "int",#int
	"int64": "long",#long
	"float32": "float", #double?
	"float64": "double", #decimal?
	"string": "string",
	"bool": "bool",
	"char": "char",
	"byte": "byte",
	"time": "ulong",
	"duration": "ulong"
}

# Remapping for std_msgs.msg references to C# objects
capital_map = {
	"uint8":"UInt8",
	"uint16":"UInt16",
	"uint32":"UInt32",
	"uint64":"UInt64",
	"int8": "Int8",
	"int16":"Int16",
	"int32":"Int32",
	"int64":"Int64",
	"float32":"Float32",
	"float64":"Float64",
	"string": "String",
	"bool": "Bool",
	"char": "Char",
	"byte": "Byte",
	"time": "Time",
	"duration": "Duration"
}

convert_map = {
	"int8":"SByte",
	"uint8":"Byte",
	"float32":"Single",
	"float64":"Double",
	"bool":"Boolean",
	"duration":"UInt64",
	"time":"UInt64"
}

prefix_msgs = "VRViz.Messages"
prefix_serialiser = "VRViz.Serialiser"

binary_list = ["uint8", "char"]


"""
Newtonsoft Seraliser Class definitions
"""

def process_message(msg_type):
	pckg, class_name = msg_type.split("/")
	if msg_type == "std_msgs/Time":
		msg = _create_time_msg("Time")
	elif msg_type == "std_msgs/Duration":msg = _create_time_msg("Duration")
	else:
		message = create_msg_object(msg_type)
		msg = message.__repr__()

	with open(os.path.join(pckg, class_name+".cs"), "w") as output:
		output.write(msg)


def _create_time_msg(type_name):
	return """using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {{
	public class {0}{{
		public ulong secs;
		public ulong nsecs;
		public static string ToRosString() {{ return "std_msgs.msg:{0}}"; }}
	}}
}}
""".format(type_name)




def create_msg_object(msg_type):
	"std_msgs/String"
	if msg_type.split('/')[-1] in capital_map.values():
		return Primative_message(msg_type)
	return Basic_message(msg_type)


class _message(object):
	def __init__(self, msg_type):
		self.pckg, self.class_name = msg_type.split("/")
		self.using = [Using("System"), Using("Newtonsoft.Json"), Using(prefix_serialiser)]

	def as_string(self):
		return "\t\tpublic static string ToRosString() {{ return \"{0}.msg:{1}\"; }}\n".format(self.pckg, self.class_name)


class Primative_message(_message):
	def __init__(self, msg_type):
		""" @msg_type -> "std_msgs/String" || "std_msgs/Uint8" """
		super(Primative_message, self).__init__(msg_type)
	
	def from_array_function(self):
		return """
			public {types}[] FromArray({clazz}[] inArr){{
				{types}[] ret = new {types}[inArr.length];
				for(int i=0;i<inArr.length;i++)
					ret[i]=inArr[i].data;
				return ret;
			}}\n
		""".format(types=type_map[self.class_name.lower()], clazz=self.class_name)

	def __repr__(self):
		return "{0}\nnamespace {1}.{2} {{\n\t[JsonConverter(typeof({3}Converter))]\n\tpublic class {3}{{\n\t\tpublic {4} data;\n{5}\t{6}\t}}\n}}"\
			.format("".join(str(u) for u in self.using),
					prefix_msgs,
					self.pckg, 
					self.class_name, 
					type_map[self.class_name.lower()],
					self.as_string(),
					self.from_array_function()
					)
	def __str__(self):
		return self.__repr__()

class Basic_message(_message):
	def __init__(self, msg_type):
		""" @msg_type -> "nav_msgs/Odometry" || "geometry_msgs/Transform """
		super(Basic_message, self).__init__(msg_type)
		contents = subprocess.check_output(["rosmsg", "show", msg_type]).split("\n")
		spls = [item.split(" ") for item in contents if item and not item.startswith("  ") and "=" not in item] #TODO: test `echo $(rosmsg info geometry_msgs/PoseStamped | grep -v "  ")`
		self.fields = [Field(*spl) for spl in spls ]

		typs = [item[0] for item in spls if "/" in item[0]]
		typs = [item for item in typs if not item.startswith("std_msgs/")]
		typs = [item.split("/")[0] for item in typs]
		self.using.extend([Using("{1} = {0}.{1}".format(prefix_msgs, item)) for item in typs])


	def __repr__(self):
		return """{0}\nusing std_msgs = {4}.std_msgs;\n\nnamespace {4}.{1} {{\n\tpublic class {2} {{\n{3}\t{5}\t}}\n}}"""\
			.format("".join(set([str(u) for u in self.using])), #TODO: replace set with ordered list
					self.pckg,
					self.class_name, 
					''.join([str(f) for f in self.fields]), 
					prefix_msgs,
					self.as_string()
					)
	def __str__(self):
		return self.__repr__()


class Field(object):
	def __repr__(self): return "{3}\t\t{0} {1} {2};\n".format(self._scope, self._type, self._field_name, self.binaryConverter)
	def __str__(self): return self.__repr__()
	def __init__(self, _type, _field_name, _scope="public"):
		self._scope = _scope
		self._field_name = _field_name
		
		# Define default values
		arr=''
		pck="std_msgs"

		#Replace defaults where appropriate
		if "[" in _type:	#clean array
			arr='[]'
			_type = re.sub(r'\[.*\]','', _type)

		if "/" in _type:	#clean package
			pck, _type = _type.split("/")

		if _type in capital_map: 	#capitalise
			_type = capital_map[_type]
		
		#Build message		
		self._type = "{0}::{1}{2}".format(pck, _type, arr)
		self.binaryConverter = ""
		if _type in binary_list:
			self.binaryConverter = "\t\t[JsonConverter(typeof({0}B64Converter))]\n"
			

class Using(object):
	def __repr__(self): return "using {0};\n".format(self._pckg)
	def __str__(self): return self.__repr__()
	def __init__(self, _pckg): self._pckg = _pckg


def _create_file_serialiser():
	return """
using System;
using UnityEngine;
using Newtonsoft.Json;
using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Serialiser {
	
	public class BaseConverter : JsonConverter{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer){throw new NotImplementedException(\"No need to Write\");}
		public override bool CanWrite{get {return false;}}
		public override bool CanRead{get {return true;}}
		public override object ReadJson(JsonReader r, Type o, object e, JsonSerializer s){ return e; }
        public override bool CanConvert(Type o) { return true; }
	}

"""


def _create_type_serialiser(type_name, convert_name=None):
	return """
	 //Autogenerated NewtonSoft Json type converters for ROS std_msgs messages following the format of ;=>
	 public class {0}Converter : BaseConverter {{
        public override object ReadJson(JsonReader reader, Type objectType, object existing, JsonSerializer serializer){{
            std_msgs.{0} obj = new std_msgs.{0}();
            obj.data = Convert.To{1}(reader.Value);
            return obj;	
        }}
        public override bool CanConvert(Type objectType) {{
            return objectType == typeof(std_msgs.{0});
        }}
    }}
	""".format(type_name, convert_name or type_name)


def _create_binary_serialiser(type_name):
	return """
	//Autogenerated NewtonSoft Json type converters for Base64 strings
	 public class {0}B64Converter : BaseConverter {{
        public override object ReadJson(JsonReader reader, Type objectType, object existing, JsonSerializer serializer){{
            byte[] array = Convert.FromBase64String(Convert.ToString(reader.Value));
            std_msgs.{0}[] obj = Enumerable.Range(1, array.Length).Select(i => new std_msgs.{0}()).ToArray();
            for (int i = 0; i < array.Length; i++) {{
                obj[i].data = array[i];
            }}
			return obj;	
        }}
        public override bool CanConvert(Type objectType) {{
            return objectType == typeof(std_msgs.{0});
        }}
    }}
	""".format(type_name)
	

#cdAssets; cd vrviz/msgs/; python processor.py -c -t 4 -p std_msgs geometry_msgs actionlib_msgs nav_msgs sensor_msgs visualization_msgs
if __name__ == "__main__":
	#Manage args
	parser = argparse.ArgumentParser(description="Convert ROS Messages to C# objects")
	parser.add_argument("-p","--packages", metavar="p", type=str, nargs='+',help="The package(s) to parse into C# Objects.", default='', dest="package")
	parser.add_argument("-t","--threads", metavar="t", type=int, default=1, help="Number of threads to use", dest="threads")
	parser.add_argument("-c","--converters", const=True, default=False, nargs='?', help="Create converters for ros messages to C# primitives", dest="convert")	
	args = parser.parse_args()
	
	# Get list of all availible messages
	messages = [item for item in subprocess.check_output(["rosmsg", "list"]).split("\n") if item is not ""]

	# Remove packages not defined in packages argument
	if args.package:
	    nm = []
	    for pkg in args.package:
    		nm.extend([item for item in messages if item.startswith(pkg)])
    	messages = nm
	packages = [item.split("/")[0] for item in messages]

	#Make directories for each package
	for pkg in packages:
		if os.path.exists(pkg):
			shutil.rmtree(pkg)
		os.makedirs(pkg)

	# Map creation of C# objects to filtered messages
	threads = Pool(args.threads)
	threads.map(process_message, messages)	
	
	# Compile serialiser file
	if args.convert:
		conversion_text = []
		conversion_text.append(_create_file_serialiser())
		for key in capital_map:
			if key == "time":
				continue
			elif key in convert_map:
				conversion_text.append(_create_type_serialiser(capital_map[key], convert_map[key]))
			else:
				conversion_text.append(_create_type_serialiser(capital_map[key]))
		for itm in binary_list:
			conversion_text.append(_create_binary_serialiser(itm))
		conversion_text.append("}")
		with open("SerialisationAdapters.cs", "w") as output:
			output.writelines(conversion_text)



	



"""
def process_file(msg_type):
	""
	types: (str) -> None
	Processes a ROS message file found via `rosmsg show $msg` into a C# object.
	Then store the file in cwd/<ROS_PACKAGE>/<ROS_MESSAGE_NAME>.cs
	:msg_type: (String) Single ROS message definition. 
	:return: None
	""
	class_name = msg_type.split("/")[1]
	package_name = msg_type.split("/")[0]
	contents = subprocess.check_output(["rosmsg", "show", msg_type]).split("\n")
	filtered = [item for item in contents if "  " not in item if item is not ""]
	required_packages = []
	internal_text = []
	comments = []
	for line in filtered: 

		# Processing steps:
		# Convert Standard Message data types to C# primitives in the std_msgs package
		# Convert Standard Messages to our C# Wrapper in vrviz.msgs.std_msgs
		# Extract Package and Type from not standard message types
		# Remove numbers from square brackets

		""
		# in Float64.msg: we want to convert float64 to c#:float -> TYPE_MAP
		""
		# Set primative std_msgs classes to reference c# style data-types
		# E.g. "std_msgs/Int16" -> "short"
		typ, dat = line.split(" "); 
		if class_name.lower() == typ.lower():
				line = line.replace(typ, type_map[typ]) 
				internal_text.append("public {0};\n".format(line))
				continue

		# Ignore fields which are constants
		# E.g. visualization_msgs::Marker contains = "uint8 ARROW=0"
		if "=" in line: continue
		
		# Convert rosmsg packages to C# object references {include std_msg where applicable}
		if "/" in line:
			# Make explicit, rosmsg references {excl. std_msg}
			# E.g. "geometry_msgs/Pose" -> "geometry_msgs::Pose"
			# 		&&
			# 		"using geometry_msgs = vrviz.msg.geometry_msgs"
			pkg, prop = line.split("/")
			line = pkg+"::"+prop
			required_packages.append("using {0} = vrviz.msg.{0};\n".format(pkg)) 			# TODO: "using {0} = vrviz.msg.{0};\n".format(pkg)
		else:
			# Make explicit, rosmsg references {only. std_msg}
			# If package is not explicitly defined, it is assumed to be a std_msg. 
			# E.g. "[std_msgs/]float64" -> "std_msgs::Float64"
			# 		&&
			# 		"using std_msgs = vrviz.msg.std_msgs"
			if any([line.startswith(k) for k in capital_map.keys()]):
				for k, v in capital_map.items():
					line = line.replace(k, v) if line.startswith(k) else line
			line = "std_msgs::"+line
			required_packages.append("using std_msgs = vrviz.msg.std_msgs;\n")					# TODO: "using std_msgs = vrviz.msg.std_msgs;\n"

		
		# using std_msgs = vrviz.msg.std_msgs
		# -> std_msgs::Image
		

		# Remove array length identifier [#]
		# E.g. std_msgs.Float64[20] -> std_msgs.Float64[]
		if "[" in line:
			line = re.sub(r'\[.*\]','[]',line) 
			#TODO: line = 'List<'+re.sub(r'\[.*\]','>',line)
			#TODO: required_packages.append("using UnityEngine.Collections.Generic;\n")
		
		# Set access restriction to public
		# E.g. "geometry_msgs::Pose" -> "public geometry_msgs::Pose"
		internal_text.append("public {0};\n".format(line))

	required_packages = set(required_packages)
	final_text = _create_final_text(class_name, package_name, required_packages, comments, internal_text)
	with open(os.path.join(package_name, class_name+".cs"), "w") as output:
		output.writelines(final_text)
	

def _create_final_text(class_name, package_name, required_packages, comments, internal_text):
	""
	# Creates the C# source code.
	""
	final_text = ["using System;\n"]
	for item in required_packages:
		final_text.append(item)
	final_text.append("\nnamespace vrviz.msg." + package_name + " {\n")
	final_text.append("\t[Serializable]\n")
	final_text.append("\tpublic class "+class_name + " {\n")
	for item in comments:
		final_text.append("\t\t"+item)
	for item in internal_text:
		final_text.append("\t\t"+item)
	final_text.append("\t}\n")
	final_text.append("}\n")
	return final_text




"""