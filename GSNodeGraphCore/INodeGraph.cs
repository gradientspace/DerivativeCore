using System;


namespace Gradientspace.NodeGraph
{
	public interface INodeGraph
	{
		//
		// node queries
		//
		IEnumerable<INodeInfo> EnumerateNodes();
		INodeInfo FindNodeFromIdentifier(int NodeIdentifier);

		bool GetNodeInputType(int NodeIdentifier, string InputName, out GraphDataType DataType);
		bool GetNodeOutputType(int NodeIdentifier, string OutputName, out GraphDataType DataType);

		//! returns constant value for an input, which can be used to evaluate the node if no connection is wired to the input.
		//! null may be a valid input constant, ie for nullable inputs. So (null,true) is a valid return in those cases.
		//! Inputs with no constant defined should return (null,false), and callers should ignore value if false is returned.
		(object? constantValue, bool bIsConstantDefined) GetNodeConstantValue(int NodeIdentifier, string InputName);
		bool SetNodeConstantValue(int NodeIdentifier, string InputName, object NewValue);

		//
		// node edits
		//

		// this throws if Type is not a valid type to add to the graph...
		INodeInfo CreateNewNodeOfType(NodeType nodeType, int UseSpecifiedNodeIdentifier = -1);

		bool RemoveNode(int NodeIdentifier);

		//
		// connection queries
		//
		IEnumerable<IConnectionInfo> EnumerateConnections(EConnectionType connectionType);

		void FindAllNodeConnections(int NodeIdentifier, ref List<IConnectionInfo> connectionInfo, EConnectionType connectionType = EConnectionType.Data);
		IConnectionInfo FindConnectionTo(int NodeIdentifier, string InputName, EConnectionType connectionType = EConnectionType.Data);
		void FindConnectionsFrom(int NodeIdentifier, string OutputName, ref List<IConnectionInfo> connections, EConnectionType connectionType = EConnectionType.Data);

		EConnectionState GetConnectionState(IConnectionInfo connectionInfo);

		//
		// connection edits
		//

		bool CanConnectTypes(GraphDataType FromOutputType, GraphDataType ToInputType);
		bool TryAddNewConnection(IConnectionInfo NewConnectionInfo);
		bool RemoveConnection(IConnectionInfo ConnectionInfo);
	}


	public interface INodeGraphLayoutProvider
	{
		string GetLocationStringForNode(int NodeIdentifier);
		bool SetNodeLocationFromString(int NodeIdentifier, string locationString);
	}


	public abstract class INodeGraphAllocatedData
	{
		public abstract void ReleaseData();
	}
}
