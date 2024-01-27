// ------------------------------------------------------------------------------------------------------
// <copyright file="DeBankTokenData.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

namespace Nomis.BalanceChecker.Interfaces.Contracts
{
    /// <summary>
    /// DeBank token data.
    /// </summary>
    public sealed class DeBankTokenData
    {
        /// <summary>
        /// The address of the token contract.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The chain's name.
        /// </summary>
        [JsonPropertyName("chain")]
        public string Chain { get; set; } = null!;

        /// <summary>
        /// The token's name.
        /// </summary>
        /// <remarks>
        /// <c>null</c> if not defined in the contract and not available from other sources.
        /// </remarks>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// The token's symbol.
        /// </summary>
        /// <remarks>
        /// <c>null</c> if not defined in the contract and not available from other sources.
        /// </remarks>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        /// <summary>
        /// The token's displayed symbol.
        /// </summary>
        /// <remarks>
        /// If two tokens have the same symbol, they are distinguished by display_symbol.
        /// </remarks>
        [JsonPropertyName("display_symbol")]
        public string? DisplaySymbol { get; set; }

        /// <summary>
        /// For front-end display.
        /// </summary>
        [JsonPropertyName("optimized_symbol")]
        public string? OptimizedSymbol { get; set; }

        /// <summary>
        /// The number of decimals of the token.
        /// </summary>
        /// <remarks>
        /// <c>null</c> if not defined in the contract and not available from other sources.
        /// </remarks>
        [JsonPropertyName("decimals")]
        public int? Decimals { get; set; }

        /// <summary>
        /// URL of the token's logo image.
        /// </summary>
        /// <remarks>
        /// <c>null</c> if not available.
        /// </remarks>
        [JsonPropertyName("logo_url")]
        public string? LogoUrl { get; set; }

        /// <summary>
        /// Token protocol id.
        /// </summary>
        [JsonPropertyName("protocol_id")]
        public string? ProtocolId { get; set; }

        /// <summary>
        /// Whether or not to show as a common token in the wallet.
        /// </summary>
        [JsonPropertyName("is_core")]
        public bool IsCore { get; set; }

        /// <summary>
        /// Token is verified.
        /// </summary>
        [JsonPropertyName("is_verified")]
        public bool IsVerified { get; set; }

        /// <summary>
        /// USD price.
        /// </summary>
        /// <remarks>
        /// Price of 0 means no data.
        /// </remarks>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// The amount of user's token.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// The raw amount of user's token.
        /// </summary>
        public BigInteger RawAmount
        {
            get
            {
                if (Amount == 0)
                {
                    return 0;
                }

                if (Decimals == null)
                {
                    return 0;
                }

                decimal amount = Amount;
                int decimals = Decimals ?? 0;
                while (decimals > 0 && amount < decimal.MaxValue / 10)
                {
                    amount *= 10;
                    decimals--;
                }

                var result = new BigInteger(amount) * BigInteger.Pow(10, decimals);
                return result;
            }
        }
    }
}