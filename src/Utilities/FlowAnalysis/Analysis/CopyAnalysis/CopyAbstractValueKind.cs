﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.CopyAnalysis
{
    /// <summary>
    /// Kind for the <see cref="CopyAbstractValue"/>.
    /// </summary>
    internal enum CopyAbstractValueKind
    {
        /// <summary>
        /// Not applicable.
        /// </summary>
        NotApplicable,

        /// <summary>
        /// Copy of a reference shared by one or more <see cref="AnalysisEntity"/> instances.
        /// </summary>
        KnownReferenceCopy,

        /// <summary>
        /// Copy of a value shared by one or more <see cref="AnalysisEntity"/> instances.
        /// </summary>
        KnownValueCopy,

        /// <summary>
        /// Copy may or may not be shared by other <see cref="AnalysisEntity"/> instances.
        /// </summary>
        Unknown,

        /// <summary>
        /// Invalid state for an unreachable path from predicate analysis.
        /// </summary>
        Invalid,
    }

    internal static class CopyAbstractValueKindExtensions
    {
        public static bool IsKnown(this CopyAbstractValueKind kind)
        {
            switch (kind)
            {
                case CopyAbstractValueKind.KnownValueCopy:
                case CopyAbstractValueKind.KnownReferenceCopy:
                    return true;

                default:
                    return false;
            }
        }

        public static CopyAbstractValueKind MergeIfBothKnown(this CopyAbstractValueKind kind, CopyAbstractValueKind kindToMerge)
        {
            if (!kind.IsKnown() ||
                !kindToMerge.IsKnown())
            {
                return kind;
            }

            // Can only ensure value copy if one of the kinds is a value copy.
            return kind == CopyAbstractValueKind.KnownValueCopy || kindToMerge == CopyAbstractValueKind.KnownValueCopy ?
                CopyAbstractValueKind.KnownValueCopy :
                CopyAbstractValueKind.KnownReferenceCopy;
        }
    }
}
