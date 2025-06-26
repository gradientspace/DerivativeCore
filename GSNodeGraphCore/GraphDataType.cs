using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gradientspace.NodeGraph
{
	/**
     * GraphDataFormat defines the underlying language/format of a GraphDataType
     * This allows (eg) GraphDataType converters to filter on underlying language type.
     * Generally non-CSharp data will be boxed in an Object and so the C# Type is
     * not necessarily very informative...
     */
	public enum EGraphDataFormat
    {
        CSharp = 0,

        Python = 100,
        CStruct = 101,


        ClientDefined1 = 1001,
		ClientDefined2 = 1002
	}


    /**
     * IExtendedGraphDataTypeInfo implementations can be attached to a GraphDataType, which will
     * allow Type-handling code to access extended functionality (eg type-compatibility checks, etc).
     */
    public interface IExtendedGraphDataTypeInfo
    {
        //! extend standard type-compatibility checks
        bool IsCompatibleWith(in GraphDataType incomingType);

        //! provide a custom string for use in UI/etc
        string? GetCustomTypeString();
    }

    /**
     * GraphDataType represents type information for data that can travel over Connections, ie from node Outputs to Inputs.
     * Every Output and Input has a single GraphDataType. In many cases this is simply a wrapper around the C# Type,
     * however sometimes additional handling is required.
     */
    public struct GraphDataType
    {
        //! graph data always has an underlying C# type - for data from other languages this will generally be 'object'...
        public Type DataType { get; init; }

        //! DataFormat enum is used to identify which underlying language a GraphDataType comes from
        public EGraphDataFormat DataFormat { get; init; }

        //! Optional extended type. This can be used to store (eg) an external non-CSharp type representation
        //! for a boxed/wrapped object. For example a python datatype/class.
        public object? ExtendedType = null;
        // todo: would be nice to have an interface here. The problem is that conceptually we want external
        // libraries/etc to provide the ExtendedType object, and they (possibly) would not have a dependency on
        // GSNodeGraphCore. Eg Python support in GSPythonUtils. So then we would need another library for this interface...

		//! a dynamic datatype generally will depend on ExtendedTypeInfo.IsCompatibleWith to determine
		//! if it is compatible with another DataType, rather than the Type directly
		public bool IsDynamic { get; init; }

        //! optional extra type-level handling
        public IExtendedGraphDataTypeInfo? ExtendedTypeInfo { get; init; }

        public GraphDataType() { 
            DataType = typeof(object);
            DataFormat = EGraphDataFormat.CSharp;
            IsDynamic = false;
            ExtendedType = null;
			ExtendedTypeInfo = null; 
        }

        public GraphDataType(Type t, 
            EGraphDataFormat dataFormat = EGraphDataFormat.CSharp, 
            object? extendedType = null) { 
            DataType = t;
			DataFormat = dataFormat;
			IsDynamic = false;
			ExtendedType = extendedType;
			ExtendedTypeInfo = null; 
        }

		public static readonly GraphDataType Default = new GraphDataType(typeof(object));

		public static GraphDataType MakeDynamic(Type baseType, IExtendedGraphDataTypeInfo? extendedInfo = null) 
        {
            return new() { DataType = baseType, DataFormat = EGraphDataFormat.CSharp, ExtendedType = null, IsDynamic = true, ExtendedTypeInfo = extendedInfo };
        }

		public static GraphDataType MakeDynamic(Type baseType, EGraphDataFormat dataFormat, object? extendedType, IExtendedGraphDataTypeInfo? extendedInfo = null)
		{
			return new() { DataType = baseType, DataFormat = dataFormat, ExtendedType = extendedType, IsDynamic = true, ExtendedTypeInfo = extendedInfo };
		}

		// IsCompatibleWith function that checks standard Type compatibility and also calls IExtendedGraphDataTypeInfo.IsCompatibleWith if available?


        public readonly bool IsSameType(in GraphDataType other)
        {
            if (DataFormat != other.DataFormat) return false;
            if (DataType != other.DataType) return false;
            if (ExtendedType != null)
            {
                if (other.ExtendedType != null)
                    return ExtendedType.Equals(other.ExtendedType);
                return false;
            }
            if (other.ExtendedType != null)
                return false;
            return true;
        }


		//readonly public override bool Equals(object? obj)
		//{
		//    if (obj == null || GetType() != obj.GetType())
		//        return false;
		//    return Identifier == ((NodeHandle)obj).Identifier;
		//}
		//public override int GetHashCode()
		//{
		//    return Identifier.GetHashCode();
		//}
		//public static bool operator ==(NodeHandle A, NodeHandle B)
		//{
		//    return A.Identifier == B.Identifier;
		//}
		//public static bool operator !=(NodeHandle A, NodeHandle B) { return !(A == B); }

		//public static bool operator ==(NodeHandle A, int NodeIdentifier) { return A.Identifier == NodeIdentifier; }
		//public static bool operator !=(NodeHandle A, int NodeIdentifier) { return A.Identifier != NodeIdentifier; }
	}
}
