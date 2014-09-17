﻿using System;
using System.Collections.Generic;
using PPPDDDChap23.Auction.Application.Infrastructure;

namespace PPPDDDChap23.Auction.Application.Model
{
    public class Price : ValueObject<Price>
    {
        private Price()
        { }

        public Price(Money amount)
        {
            if (amount == null)
                throw new ArgumentNullException("Amount cannot be null");

            Amount = amount;
        }

        public Money Amount { get; private set; }

        public Money BidIncrement()
        {
            if (Amount.IsGreaterThanOrEqualTo(new Money(0.01m)) && Amount.IsLessThanOrEqualTo(new Money(0.99m)))
                    return Amount.add(new Money(0.05m));

            if (Amount.IsGreaterThanOrEqualTo(new Money(1.00m)) && Amount.IsLessThanOrEqualTo(new Money(4.99m)))
                    return Amount.add(new Money(0.20m));
                
            if (Amount.IsGreaterThanOrEqualTo(new Money(5.00m)) && Amount.IsLessThanOrEqualTo(new Money(14.99m)))
                    return Amount.add(new Money(0.50m));

            return Amount.add(new Money(1.00m));
            
        }

        public bool CanBeExceededBy(Money offer)
        {
            return offer.IsGreaterThanOrEqualTo(BidIncrement());
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>() {Amount};
        }
    }
}
