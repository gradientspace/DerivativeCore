// Copyright Gradientspace Corp. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gradientspace.NodeGraph
{
    //! type for a graph node
    public class NodeType
    {
        public Type ClassType;
        public string UIName;
        public string UICategory;
        public string Variant;
        public object? VariantData = null;

        //! instance of this node type, this is required to be able to (eg)
        //! enumerate the parameters of a node...
        public INode? NodeArchetype = null;

        public NodeType(Type classType)
        {
            this.ClassType = classType;
            UIName = classType.Name;
            UICategory = classType.Namespace ?? "Default";
            Variant = "";
        }

        public NodeType(Type classType, string uiName)
        {
            this.ClassType = classType;
            UIName = uiName;
            UICategory = classType.Namespace ?? "Default";
            Variant = "";
        }

        public NodeType(Type classType, string variant, string uiName)
        {
            this.ClassType = classType;
            UIName = uiName;
            UICategory = classType.Namespace ?? "Default";
            Variant = variant;
        }

        public string GetNodeTypeUIName()
        {
            return UIName;
        }

		public override string ToString()
		{
            if (Variant.Length > 0)
				return $"{UIName} ({ClassType.ToString()}//{Variant})";
			else
                return $"{UIName} ({ClassType.ToString()})";
		}
	}
}
