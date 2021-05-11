import re
import os
import subprocess
import argparse
import shutil
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
	"float16": "half", #float?
	"float32": "float", #double?
	"float64": "double", #decimal?
	"string": "string",
	"time": "Time",
	"bool": "bool",
	"char": "char",
	"byte": "byte",
	"duration": "Duration"
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
	"float16":"Float16",
	"float32":"Float32",
	"float64":"Float64",
	"string": "String",
	"bool": "Bool",
	"char": "Char",
	"byte": "Byte",
	"time":"Time",
	"duration":"Duration"
}

def process_file(msg_type):
	"""
	types: (str) -> None
	Processes a ROS message file found via `rosmsg list` in a C# object.
	It then stores the file in cwd/<ROS_PACKAGE>/<ROS_MESSAGE_NAME>.cs
	:msg_type: (String) Single ROS message definition. 
	:return: None
	"""
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

		"""
		# in Float64.msg: we want to convert float64 to c#:float -> TYPE_MAP
		"""
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
	"""
	Creates the C# source code.
	"""
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



if __name__ == "__main__":
	parser = argparse.ArgumentParser(description="Convert ROS Messages to C# objects")
	parser.add_argument("-p","--packages", metavar="p", type=str, nargs='+',help="The package(s) to parse into C# Objects.", default='', dest="package")
	parser.add_argument("-t","--threads", metavar="t", type=int, default=1, help="Number of threads to use", dest="threads")
	args = parser.parse_args()
	messages = [item for item in subprocess.check_output(["rosmsg", "list"]).split("\n") if item is not ""]
	if args.package:
	    nm = []
	    for pkg in args.package:
    		nm.extend([item for item in messages if item.startswith(pkg)])
    	messages = nm
	packages = [item.split("/")[0] for item in messages]
	for pkg in packages:
		if os.path.exists(pkg):
			shutil.rmtree(pkg)
		os.makedirs(pkg)
	threads = Pool(args.threads)
	threads.map(process_file, messages)

