using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradientspace.NodeGraph
{
    public enum EGraphOutputType
    {
        User = 0,
        Logging = 1,
        Debug = 2,

        GraphWarning = 3,
        GraphError = 4
    }


    public interface IGraphOutput
    {
        void AppendLine(string Line, EGraphOutputType OutputType = EGraphOutputType.User);
        void Clear();
    }

    public static class GlobalGraphOutput
    {
        internal static IGraphOutput? ActiveOutput = null;

		public delegate void GraphOutputUpdatedHandler(string? appendedString, EGraphOutputType OutputType);
		public static event GraphOutputUpdatedHandler? OnGraphOutputUpdated;

        public static bool MirrorDebugToConsole = true;
        public static bool MirrorGraphIssuesToConsole = true;


        public static void SetCurrentOutput(IGraphOutput activeOutput)
        {
            ActiveOutput = activeOutput;
        }

        public static void Clear()
        {
            if (ActiveOutput != null)
            {
                ActiveOutput.Clear();
                OnGraphOutputUpdated?.Invoke(null, EGraphOutputType.GraphError);
            }
		}

		public static void AppendLine(string Line, EGraphOutputType OutputType = EGraphOutputType.User)
        {
            if (ActiveOutput == null ||
                (MirrorDebugToConsole && OutputType == EGraphOutputType.Debug) ||
                (MirrorGraphIssuesToConsole && (OutputType == EGraphOutputType.GraphError || OutputType == EGraphOutputType.GraphWarning) ) )
            {
                System.Console.WriteLine(Line);
                System.Diagnostics.Debug.WriteLine(Line);
            }

            ActiveOutput?.AppendLine(Line, OutputType);
			OnGraphOutputUpdated?.Invoke(Line, OutputType);
		}


        public static void AppendError(string Line)
        {
            AppendLine(Line, EGraphOutputType.GraphError);
        }
	}
}
