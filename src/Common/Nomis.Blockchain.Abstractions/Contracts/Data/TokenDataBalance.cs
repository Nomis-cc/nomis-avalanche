// ------------------------------------------------------------------------------------------------------
// <copyright file="TokenDataBalance.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

using Nomis.DefiLlama.Interfaces.Models;

namespace Nomis.Blockchain.Abstractions.Contracts.Data
{
    /// <summary>
    /// Token data balance.
    /// </summary>
    public class TokenDataBalance :
        TokenData
    {
        private decimal _amount;
        private decimal _totalAmountPrice;

        /// <summary>
        /// Initialize <see cref="TokenDataBalance"/>.
        /// </summary>
        public TokenDataBalance()
        {
        }

        /// <summary>
        /// Initialize <see cref="TokenDataBalance"/>.
        /// </summary>
        /// <param name="tokenData"><see cref="TokenData"/>.</param>
        public TokenDataBalance(
            TokenData tokenData)
            : base(tokenData)
        {
        }

        /// <summary>
        /// Initialize <see cref="TokenDataBalance"/>.
        /// </summary>
        /// <param name="tokenDataBalance"><see cref="TokenDataBalance"/>.</param>
        /// <param name="balance">Balance.</param>
        public TokenDataBalance(
            TokenDataBalance tokenDataBalance,
            BigInteger balance)
            : base(tokenDataBalance)
        {
            _amount = 0;
            Balance = balance;
            Price = tokenDataBalance.Price;
            LastPriceDateTime = tokenDataBalance.LastPriceDateTime;
            Confidence = tokenDataBalance.Confidence;
            ChainId = tokenDataBalance.ChainId;
            Source = tokenDataBalance.Source;
        }

        /// <summary>
        /// Balance.
        /// </summary>
        [JsonIgnore]
        public BigInteger Balance { get; set; }

        /// <summary>
        /// Balance amount.
        /// </summary>
        public decimal Amount
        {
            get
            {
                if (_amount > 0)
                {
                    return _amount;
                }

                if (Decimals != null && int.TryParse(Decimals, out int decimals))
                {
                    var realAmount = Balance;
                    if (realAmount <= new BigInteger(decimal.MaxValue))
                    {
                        _amount = (decimal)realAmount;
                        for (int i = 0; i < decimals; i++)
                        {
                            _amount /= 10;
                        }

                        return _amount;
                    }
                    else
                    {
                        ulong multiplier = 1;
                        for (int i = 0; i < decimals; i++)
                        {
                            multiplier *= 10;
                        }

                        try
                        {
                            _amount = (decimal)(realAmount / new BigInteger(multiplier));
                            return _amount;
                        }
                        catch
                        {
                            _amount = 0;
                            return _amount;
                        }
                    }
                }

                _amount = 0;
                return _amount;
            }

            set
            {
                _amount = value;
            }
        }

        /// <summary>
        /// Price.
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// Last price date and time.
        /// </summary>
        public DateTime? LastPriceDateTime { get; set; }

        /// <summary>
        /// Confidence.
        /// </summary>
        public decimal Confidence { get; set; } = 0;

        /// <summary>
        /// Total token balance amount price.
        /// </summary>
        public decimal TotalAmountPrice
        {
            get
            {
                if (_totalAmountPrice > 0)
                {
                    return _totalAmountPrice;
                }

                try
                {
                    if (Price == 0)
                    {
                        _totalAmountPrice = 0;
                    }
                    else
                    {
                        _totalAmountPrice = Price * Amount;
                    }
                }
                catch
                {
                    _totalAmountPrice = 0;
                }

                return _totalAmountPrice;
            }

            set
            {
                _totalAmountPrice = value;
            }
        }

        /// <summary>
        /// Token blockchain id.
        /// </summary>
        public ulong ChainId { get; set; }

        /// <summary>
        /// Token data source.
        /// </summary>
        public string? Source { get; set; }

        /// <summary>
        /// Convert to <see cref="TokenPriceData"/>.
        /// </summary>
        public TokenPriceData ToTokenPriceData()
        {
            return new TokenPriceData
            {
                Id = Id,
                Decimals = int.TryParse(Decimals, out int decimals) ? decimals : 0,
                LogoUri = LogoUri,
                Price = Price,
                Symbol = Symbol,
                Confidence = 0.9M
            };
        }
    }
}