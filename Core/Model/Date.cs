using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{ //todo: write documentation about doc comments
    public interface IDate
    {
        int Year { get; }
        int Month { get; }
        bool IsPassed { get; }
    }

    public class Date : IDate
    {//doc: immutable
        private const int MIN_YEAR = 1982;
        private const int MIN_MONTH = 1;
        private const int MAX_MONTH = 12;
        private Date() { }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public bool IsPassed
        {
            get
            {
                var now = DateTime.Now;
                if (Year < now.Year)
                    return true;
                if (Year > now.Year)
                    return false;
                return Month < now.Month;
            }
        }

        public static IDate Create(int year, int month)
        {
            if (year < MIN_YEAR)
                throw new ArgumentException($"Value of {nameof(Year)} cannot be less than {MIN_YEAR}. Received: {year}");
            if (month < MIN_MONTH || month > MAX_MONTH)
                throw new ArgumentException($"Value of {nameof(Month)} need to be between {MIN_MONTH} and {MAX_MONTH}: Received: {month}");
            var instance = new Date();
            instance.Year = year;
            instance.Month = month;
            return instance;
        }
    }
}