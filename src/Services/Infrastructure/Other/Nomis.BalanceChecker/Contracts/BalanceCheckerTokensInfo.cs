// ------------------------------------------------------------------------------------------------------
// <copyright file="BalanceCheckerTokensInfo.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nethereum.ABI.FunctionEncoding.Attributes;
using Nomis.BalanceChecker.Interfaces.Contracts;

namespace Nomis.BalanceChecker.Contracts
{
    /// <summary>
    /// Balance checker tokens info.
    /// </summary>
    [FunctionOutput]
    internal sealed class BalanceCheckerTokensInfo :
        IFunctionOutputDTO
    {
        [Parameter("tuple[]", "tokenInfos", 1)]
        public List<BalanceCheckerTokenInfo> TokenInfos { get; set; } = new();
    }
}