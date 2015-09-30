// Guids.cs
// MUST match guids.h
using System;

namespace NBR.VSBackGroundPackage
{
    static class GuidList
    {
        public const string guidVSBackGroundPackagePkgString = "72de6ff0-f2d2-4e5c-b9ca-d09d9b1d9013";
        public const string guidVSBackGroundPackageCmdSetString = "e4062518-4975-49eb-8ada-40baa1c0d75c";

        public static readonly Guid guidVSBackGroundPackageCmdSet = new Guid(guidVSBackGroundPackageCmdSetString);
    };
}