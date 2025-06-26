

namespace Gradientspace.NodeGraph
{
    public enum ENodeInputFlags
    {
        None = 0,
        
        //! A Node-Constant Input is one in which no input wire can be connected, ie it is constant during graph evaluation.
        //! This can be used to implement Node parameters via the node-input system, which is mostly a convenience
        IsNodeConstant = 1
    }


    //! Generic interface for an input to an INode. This generally is intended to be implemented by
    //! "NodeInput" classes, eg an INode will have a list of Input objects that each implement this interface.
    public interface INodeInput
    {
        GraphDataType GetDataType();

        //! get flags for this input. This is optional and inputs may return ENodeInputFlags.None
        ENodeInputFlags GetInputFlags();

        //! returns constant value for this input, which can be used if no connection is wired to this input.
        //! null may be a valid input constant, ie for nullable inputs. So (null,true) is a valid return in those cases.
        //! Inputs with no constant defined should return (null,false), and callers should ignore value if false is returned.
        (object?,bool) GetConstantValue();

        //! Set the constant value on this input. Should not be called if GetConstantValue returns false
        //! Currently also not supported if GetConstantValue returns (null,true)
        void SetConstantValue(object value);
    }

    //! generic interface for an output from an INode
    public interface INodeOutput
    {
        GraphDataType GetDataType();
    }

    //! named wrapper around an INodeInput. The (name/input) mappings are meant to be stored by the INode,
    //! rather than the individual Inputs. 
    public struct INodeInputInfo
    {
        public string InputName;
        public INodeInput Input;

        public GraphDataType DataType { get { return Input.GetDataType(); } }
        public bool IsNodeConstant { get { return (Input.GetInputFlags() & ENodeInputFlags.IsNodeConstant) != 0; } }
    }

    //! named wrapper around an INodeOutput
    public struct INodeOutputInfo
    {
        public string OutputName;
        public INodeOutput Output;

        public GraphDataType DataType { get { return Output.GetDataType(); } }
    }



    //! minimal Node interface
    public interface INode
    {
        string GetNodeName();

        IEnumerable<INodeInputInfo> EnumerateInputs();
        IEnumerable<INodeOutputInfo> EnumerateOutputs();
    }


    //! optional additional Node interface for variable inputs
    public interface INode_VariableInputs
    {
        bool AddInput();
        bool RemoveInput(int SpecifiedIndex = -1);
    }

    //! optional additional Node interface for variable outputs
    public interface INode_VariableOutputs
    {
        bool AddOutput();
        bool RemoveOutput(int SpecifiedIndex = -1);
    }


	//! optional additional Node interface for nodes with dynamic outputs / types
	public interface INode_DynamicOutputs
    {
        void UpdateDynamicOutputs(INodeGraph graph);
    }

    public struct NodeHandle
    {
        static int InvalidHandle = -1;
        public int Identifier = InvalidHandle;
        public NodeHandle() { }
        public NodeHandle(int NewIdentifier) { Identifier = NewIdentifier; }

        public static readonly NodeHandle Invalid = new(InvalidHandle);

        readonly public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            return Identifier == ((NodeHandle)obj).Identifier;
        }
        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }
        public static bool operator ==(NodeHandle A, NodeHandle B)
        {
            return A.Identifier == B.Identifier;
        }
        public static bool operator !=(NodeHandle A, NodeHandle B) { return !(A == B); }

        public static bool operator ==(NodeHandle A, int NodeIdentifier) { return A.Identifier == NodeIdentifier; }
        public static bool operator !=(NodeHandle A, int NodeIdentifier) { return A.Identifier != NodeIdentifier; }
    }



    public struct INodeInfo
    {
        // todo if Identifier was part of Node then we would not need this structure?
        // (but INode is used by graph so this is kind of a wrapper?)
        public int Identifier = -1;
        public INode? Node = null;

        public INodeInfo() { }

        public readonly bool IsValid { get { return Node != null; } }
    }


    public enum EConnectionType
    {
        Data = 0,
        Sequence = 1
    }

    public enum EConnectionState
    {
        OK = 0,
        TypeMismatch = 1,
        InputMissing = 2,
        OutputMissing = 3,

        NotFound = 100
    }

    public struct IConnectionInfo
    {
        public int FromNodeIdentifier = -1;
        public string FromNodeOutputName = "";

        public int ToNodeIdentifier = -1;
        public string ToNodeInputName = "";

        public EConnectionType ConnectionType = EConnectionType.Data;

        public IConnectionInfo() { }

        public static IConnectionInfo Invalid { get; } = new IConnectionInfo();

        public bool IsValid
        {
            get
            {
                return FromNodeIdentifier >= 0 && ToNodeIdentifier >= 0 &&
                    ((ConnectionType == EConnectionType.Data) ? (FromNodeOutputName.Length > 0 && ToNodeInputName.Length >= 0) : true);
            }
        }


        public static bool operator ==(IConnectionInfo A, IConnectionInfo B)
        {
            return A.FromNodeIdentifier == B.FromNodeIdentifier && A.FromNodeOutputName == B.FromNodeOutputName && A.ToNodeIdentifier == B.ToNodeIdentifier && A.ToNodeInputName == B.ToNodeInputName && A.ConnectionType == B.ConnectionType;
        }
        public static bool operator !=(IConnectionInfo A, IConnectionInfo B) { return !(A == B); }
        readonly public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            return this == ((IConnectionInfo)obj);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(FromNodeIdentifier, FromNodeOutputName, ToNodeIdentifier, ToNodeInputName, ConnectionType);
        }
    }

 

}

