// Copyright Gradientspace Corp. All Rights Reserved.
using System;

namespace Gradientspace.NodeGraph
{
    public struct NodeVersion : IComparable<NodeVersion>, IEquatable<NodeVersion>
    {
        public int Major = 1;
        public int Minor = 0;

        public NodeVersion() {}
        public NodeVersion(int major, int minor) { Major = major; Minor = minor; }

        public static NodeVersion Default = new();
        public static NodeVersion MostRecent = new(-1, -1);

        public readonly bool IsMostRecent => (Major == -1);

        public readonly int CompareTo(NodeVersion other)
        {
            int major = Major.CompareTo(other.Major);
            if (major != 0) return major;
            return Minor.CompareTo(other.Minor);
        }
        public readonly bool Equals(NodeVersion other) {
            return Major == other.Major && Minor == other.Minor;
        }
        public override readonly bool Equals(object? o) {
            return (o != null && o is NodeVersion && (NodeVersion)o == this) ? true : false;
        }
        public static bool operator ==(NodeVersion a, NodeVersion b) {
            return (a.Major == b.Major && a.Minor == b.Minor);
        }
        public static bool operator !=(NodeVersion a, NodeVersion b) {
            return (a.Major != b.Major || a.Minor != b.Minor);
        }
        public override int GetHashCode() {
            unchecked  {
                int hash = (int)2166136261;
                hash = (hash * 16777619) ^ Major.GetHashCode();
                hash = (hash * 16777619) ^ Minor.GetHashCode();
                return hash;
            }
        }

        public override string ToString() {
            return $"{Major}.{Minor}";
        }

        public static bool ValidateVersion(string s)
        {
            bool bSawOneDot = false;
            for ( int i = 0; i < s.Length; ++i ) {
                if (Char.IsDigit(s[i]) == false) {
                    if (s[i] == '.')
                        bSawOneDot = true;
                    else
                        return false;
                }
            }
            if (bSawOneDot == false) return false;
            return true;
        }

        public static NodeVersion Parse(string s)
        {
            int dotIndex = s.IndexOf('.');
            bool bHaveMajor = int.TryParse(s.AsSpan().Slice(0, dotIndex), out int Major); ;
            dotIndex++;
            bool bHaveMinor = int.TryParse(s.AsSpan().Slice(dotIndex, s.Length-dotIndex), out int Minor);
            if (!bHaveMajor)
                return NodeVersion.Default;
            Minor = (bHaveMinor) ? Math.Max(0, Minor) : 0;
            Major = (bHaveMajor) ? Math.Clamp(Major, 1, int.MaxValue) : 1;
            return new NodeVersion(Major, Minor);
        }


        public static (string?, string?) ParseNodeNameWithVersion(string nodeName)
        {
            int idx = nodeName.LastIndexOf("_v");
            if (idx < 0)
                return (null, null);
            for (int j = idx+2; j < nodeName.Length; j++) {
                if (Char.IsDigit(nodeName[j]) == false && nodeName[j] != 'p')
                    return (null, null);
            }
            string basename = nodeName.Substring(0, idx);
            string versionName = nodeName.Substring(idx+2).Replace('p', '.');
            return (basename, versionName);
        }
    }
}
