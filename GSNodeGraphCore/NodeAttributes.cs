// Copyright Gradientspace Corp. All Rights Reserved.


namespace Gradientspace.NodeGraph
{

    /// <summary>
    /// [GraphNodeUIName("NodeName")]
    /// 
    /// This tag can be used on INode-derived classes to set the display name of the node
    /// </summary> 
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class GraphNodeUIName : Attribute
    {
        public string UIName { get; init; }

        public GraphNodeUIName(string name)
        {
            UIName = name;
        }
    }


    /// <summary>
    /// [GraphNodeNamespace("LibraryName")]
    /// 
    /// Use this tag on an INode-derived class to include it in the specified Namespace in the UI/etc
    /// </summary> 
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class GraphNodeNamespace : Attribute
	{
		public string Namespace { get; init; } = "";

		public GraphNodeNamespace()
		{
		}
		public GraphNodeNamespace(string name)
		{
			Namespace = name;
		}
	}


    /// <summary>
    /// ClassHierarchyNode is for INode-derived classes that should not appear as directly-constructible node types
    /// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ClassHierarchyNode : Attribute
    {
    }

    /// <summary>
    /// SystemNode is for INode-derived classes that can exist in a graph but are special in some way and so cannot be directly created
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SystemNode : Attribute
    {
    }




    /// <summary>
    /// [NodeFunctionLibrary("LibraryName")]
    /// 
    /// Classes with this attribute will be inspected to see if they contain any NodeFunction's to expose as nodes.
    /// LibraryName will be used as the path to the nodes. 
    /// </summary> 
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class NodeFunctionLibrary : Attribute
    {
        public string LibraryName { get; init; } = "";

        public NodeFunctionLibrary(string name)
        {
            LibraryName = name;
        }
    }

    /// <summary>
    /// [NodeFunction]
    /// 
    /// this attribute/tag indicates that a static function of a NodeFunctionLibrary should
    /// be exposed as a graph node
    /// </summary> 
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NodeFunction : Attribute
    {
        public bool IsPure { get; init; } = false;
        public string? ReturnName { get; init; } = null;
        public bool Hidden { get; init; } = false;      // hidden nodes are not shown in search

        public string Version { get; init; } = "1.0";
        public string? VersionOf { get; init; } = null;
    }

    /// <summary>
    /// *** DEPRECATED *** - use NodeFunction.ReturnName instead
    /// [NodeReturnValue(DisplayName = "Mesh")]
    /// 
    /// Use to configure the name of the return value in NodeFunctions.
    /// </summary> 
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NodeReturnValue : Attribute
    {
        public string DisplayName { get; init; } = string.Empty;

        public NodeReturnValue()
        {
        }

        public NodeReturnValue(string displayName)
        {
            DisplayName = displayName;
        }
    }

    /// <summary>
    /// [NodeParameter ...]
    /// 
    /// Used to configure display name and/or default value of a graph node  (default value very limited due to Attribute constraints)
    /// </summary> 
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class NodeParameter : Attribute
    {
        // specifies which function argument this parameter will be applied to - required, and must be an exact string match
        public string ArgumentName { get; set; } = string.Empty;

        // string that overrides display string for pin
        public string DisplayName { get; set; } = string.Empty;

        // can be used for POD types (bool, int, float, etc), string, System.Type, Enum types
        public object? DefaultValue { get; set; } = null;

        // default float and int arrays (eg for vectors)
        public double[]? DefaultRealVec { get; set; } = null;
        public int[]? DefaultIntVec { get; set; } = null;

        /// if true, this parameter is not required (eg it can be null). Alternative to nullable declaration in C# code (which doesn't work for single objects)
        public bool IsOptional { get; set; } = false;

        public NodeParameter(string argName)
        {
            ArgumentName = argName;
        }
    }


    /// <summary>
    /// [NodeFunctionUIName("NodeName")]
    /// 
    /// This tag can be used on NodeFunctions to set the display name of the node 
    /// to something other than the static function name
    /// </summary> 
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NodeFunctionUIName : Attribute
    {
        public string UIName { get; init; }
        public NodeFunctionUIName(string name)
        {
            UIName = name;
        }
    }



    //
    // renaming/migration support
    //




    /// <summary>
    /// [MappedFunctionLibraryName("old_name")]
    /// 
    /// Use when renaming a NodeFunctionLibrary. Add this attribute, with the old library name,
    /// and graph loader will map functions from the old library to the new library during loading.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class MappedFunctionLibraryName : Attribute
    {
        public string MappedName { get; init; } = "";

        public MappedFunctionLibraryName(string mappedName)
        {
            MappedName = mappedName;
        }
    }


    /// <summary>
    /// [MappedNodeFunctionName("old_name")]
    /// 
    /// Use when renaming a NodeFunction. Add this attribute, with the old function  name,
    /// and graph loader will map from old function to new function during loading
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class MappedNodeFunctionName : Attribute
    {
        public string MappedName { get; init; } = "";

        public MappedNodeFunctionName(string mappedName)
        {
            MappedName = mappedName;
        }
    }


    /// <summary>
    /// [MappedNodeTypeName("old_name")]
    /// 
    /// Use when renaming an INode-derived Node class. Add this attribute, with the old function class,
    /// and graph loader will map from old node to new node during loading
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class MappedNodeTypeName : Attribute
    {
        public string MappedName { get; init; } = "";

        public MappedNodeTypeName(string mappedName)
        {
            MappedName = mappedName;
        }
    }



    // currently unused...
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class NodeArgumentInfo : Attribute
    {
        public string ArgumentName { get; init; } = "";
        public object? DefaultValue { get; init; } = "";

        public NodeArgumentInfo(string argName)
        {
            ArgumentName = argName;
        }
    }




	/**
     * [GraphDataTypeConversion] method attribute
     * 
     * this attribute/tag indicates that a static function implements a conversion
     * between two graph data types. These functions are automatically discovered and used 
     * by the graph to provide automatic conversions at input pins.
     * 
     * The function must be in the format:
     *     to_type MyConversionFunction( from_type object )
     * 
     */
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class GraphDataTypeConversion : Attribute
	{
	}

	/**
     *  [GraphDataTypeRegisterFunction] method attribute
     *  
     *  This attribute indicates that this function should be called to register
     *  custom datatype-conversions into a DataConversionLibrary.
     *  
     *  The function must have the signature:
     *      void MyRegisterFunction(DataConversionLibrary library)
     *      
     *  This can be used as an alternative to the [GraphDataTypeConversion] attribute
     *  in cases where the C# Type values of the method argument/return are not sufficient
     *  to specify the conversion, and a custom GraphDataType pair must be constructed in code
     *  (this type can't be used as Attribute member/argument metadata).
     */
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class GraphDataTypeRegisterFunction : Attribute
    {
    }

}
