

namespace Gradientspace.NodeGraph
{

    /**
     * [GraphNodeUIName("NodeName")]
     * 
     * This tag can be used on graph node classes (ie subclasses of NodeBase) to set
     * the display name of the node
     */
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class GraphNodeUIName : Attribute
    {
        public string UIName { get; init; }

        public GraphNodeUIName(string name)
        {
            UIName = name;
        }
    }



    /**
     * [GraphNodeFunctionLibrary("LibraryName")]
     * 
     * Classes with this attribute will be inspected to see if they contain any NodeFunction's to expose as nodes.
     * LibraryName will be used as the path to the nodes. If it is not defined, 
     */
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class GraphNodeFunctionLibrary : Attribute
    {
        public string LibraryName { get; init; } = "";

        public GraphNodeFunctionLibrary()
        {
        }
        public GraphNodeFunctionLibrary(string name)
        {
            LibraryName = name;
        }
    }

	/**
     * [GraphNodeNamespace("LibraryName")]
     * 
     * Use this tag on an INode-derived class to include it in the specified Namespace in the UI/etc
     */
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


	//! ClassHierarchyNode is for node classes that should not appear as directly-constructible node types
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ClassHierarchyNode : Attribute
    {
    }

    //! SystemNode is for node classes that can exist in a graph but are special in some way and so cannot be directly created
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SystemNode : Attribute
    {
    }


    /**
     * [NodeFunction]
     * 
     * this attribute/tag indicates that a static function of a GraphNodeFunctionLibrary should
     * be exposed as a graph node
     */
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NodeFunction : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NodeReturnValue : Attribute
    {
        public string DisplayName { get; init; } = string.Empty;

        public NodeReturnValue()
        {
        }
    }


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class NodeParameter : Attribute
    {
        public string ArgumentName { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;
        public object? DefaultValue { get; set; } = null;

        public NodeParameter(string argName)
        {
            ArgumentName = argName;
        }
    }


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NodeFunctionUIName : Attribute
    {
        public string UIName { get; init; }
        public NodeFunctionUIName(string name)
        {
            UIName = name;
        }
    }



    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class MappedLibraryName : Attribute
    {
        public string MappedName { get; init; } = "";

        public MappedLibraryName(string mappedName)
        {
            MappedName = mappedName;
        }
    }


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class MappedNodeFunctionName : Attribute
    {
        public string MappedName { get; init; } = "";

        public MappedNodeFunctionName(string mappedName)
        {
            MappedName = mappedName;
        }
    }


    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class MappedNodeTypeName : Attribute
    {
        public string MappedName { get; init; } = "";

        public MappedNodeTypeName(string mappedName)
        {
            MappedName = mappedName;
        }
    }



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
