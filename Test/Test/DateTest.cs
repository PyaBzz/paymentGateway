using System;
using Xunit;
using Core;
using Moq;

namespace Test
{
    public class DateTest
    {
        private const int MIN_YEAR = 1982; //doc: assumption

        [Fact]
        public void Create_InitialisesState()
        {
            var instance = Date.Create(2022, 3);
            Assert.Equal(2022, instance.Year);
            Assert.Equal(3, instance.Month);
        }

        [Fact]
        public void Create_ChecksLowerBoundOfYear()
        {
            Assert.Throws<ArgumentException>(
                () => Date.Create(MIN_YEAR - 1, 3)
            );
        }

        [Fact]
        public void Create_ChecksLowerBoundOfMonth()
        {
            Assert.Throws<ArgumentException>(
                () => Date.Create(DateTime.Now.Year, 0)
            );
        }

        [Fact]
        public void Create_ChecksUpperBoundOfMonth()
        {
            Assert.Throws<ArgumentException>(
                () => Date.Create(DateTime.Now.Year, 13)
            );
        }

        [Fact]
        public void IsPassed_IsTrueForPast()
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            var instance = Date.Create(lastMonth.Year, lastMonth.Month);
            Assert.Equal(true, instance.IsPassed);
        }

        [Fact]
        public void IsPassed_IsFalseForCurrentMonth()
        {
            var now = DateTime.Now;
            var instance = Date.Create(now.Year, now.Month);
            Assert.Equal(false, instance.IsPassed);
        }

        [Fact]
        public void IsPassed_IsFalseForFuture()
        {
            var nextMonth = DateTime.Now.AddMonths(+1);
            var instance = Date.Create(nextMonth.Year, nextMonth.Month);
            Assert.Equal(false, instance.IsPassed);
        }
    }
}
