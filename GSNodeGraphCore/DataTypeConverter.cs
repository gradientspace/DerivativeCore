using System;

namespace Gradientspace.NodeGraph
{
	/**
	 * implementors of IDataTypeConversion provide a method Convert() that can
	 * convert between GraphDataTypes.
	 * 
	 * These conversions are intended to be used during graph construction and evaluation,
	 * eg they can be used automatically inside the graph evaluation at input pins where
	 * a conversion is necessary. 
	 * 
	 * Conversions ideally should be very simple/minimal functions, although this 
	 * facility could be abused for all sorts of fun...
	 */
	public interface IDataTypeConversion
	{
		GraphDataType FromType { get; }
		GraphDataType ToType { get; }

		object Convert(object o);
	}

	/**
	 * optional base class for IDataTypeConversion implementations
	 */
	public abstract class DataTypeConverterBase : IDataTypeConversion
	{
		public GraphDataType FromType { get; init; }
		public GraphDataType ToType { get; init; }

		public DataTypeConverterBase(Type fromType, Type toType)
		{
			FromType = new GraphDataType(fromType);
			ToType = new GraphDataType(toType);
		}

		public DataTypeConverterBase(GraphDataType fromType, GraphDataType toType)
		{
			FromType = fromType;
			ToType = toType;
		}

		public abstract object Convert(object o);
	}



}