using System;
using System.Threading.Tasks;
using Fairmas.PickupTracking.Shared.Interfaces;
using Fairmas.PickupTracking.Shared.Models;
using System.Collections.Generic;
using System.Globalization;

namespace Fairmas.PickupTracking.Shared.Services
{
    public class PickupTestDataService : IPickupDataService
    {
        public async Task<Hotel[]> GetHotelsAsync()
        {
            // API
            // https://put-development.fairplanner.net/api/Administration/Property/GetProperties

            try
            {
                var client = ServiceLocator.FindService<IWebApiClientService>();
                var hotels = await client.GetAsync<Hotel[]>("Administration/Property/", "GetProperties", new[]
                {
                    new KeyValuePair<string, string>("true", "_=1479254387679"),
                });

                return hotels;
            }
            catch (Exception)
            {
                return new Hotel[] { };
            }
        }

        public async Task<ResponseFiguresModel[]> GetMonthlyPickupSummaryAsync(Guid hotelId, DateTime pickupFrom, DateTime pickupTo)
        {
            var random = new Random();
            var monthlyPickupSummary = new ResponseFiguresModel[12];

            for (int index = 0; index < 12; index++)
            {
                var rooms1 = random.Next(120);
                var rooms2 = random.Next(120);
                var price = 80.0 + (40.0 * random.NextDouble());
                var occupancy = random.NextDouble() * 100d;
                var adr = 60d + random.NextDouble() * 35d;
                var puAdr = -50d + random.NextDouble() * 100d;

                monthlyPickupSummary[index] = (new ResponseFiguresModel()
                {
                    BusinessDate = DateTime.Today.Date.AddMonths(index),
                    Occupancy = new PickupFigures()
                    {
                        SnapshotOneValue = (decimal)occupancy
                    },
                    AverageDailyRate = new PickupFigures()
                    {
                        SnapshotOneValue = (decimal)adr,
                        PickupValue = (decimal)puAdr
                    },
                    Revenue = new PickupFigures()
                    {
                        SnapshotOneValue = rooms1 * (decimal)price,
                        SnapshotTwoValue = rooms2 * (decimal)price,
                        PickupValue = (rooms2 * (decimal)price) - (rooms1 * (decimal)price)
                    },
                    RoomNights = new PickupFigures()
                    {
                        SnapshotOneValue = rooms1,
                        SnapshotTwoValue = rooms2,
                        PickupValue = rooms2 - rooms1
                    }
                });
            }

            return monthlyPickupSummary;
        }

        public async Task<ResponseFiguresModel[]> GetDailyPickupSummaryAsync(Guid hotelId, DateTime pickupFrom, DateTime pickupTo, DateTime month)
        {
            var random = new Random();
            var calendar = new JulianCalendar();
            var days = calendar.GetDaysInMonth(month.Year, month.Month);
            var dailyPickupSummary = new ResponseFiguresModel[days];
            
            for (int day = 1; day <= days; day++)
            {
                var rooms1 = random.Next(120);
                var rooms2 = random.Next(120);
                var price = 80.0 + (40.0 * random.NextDouble());
                var occupancy = random.NextDouble() * 100d;
                var adr = 60d + random.NextDouble() * 35d;
                var puAdr = -50d + random.NextDouble() * 100d;

                dailyPickupSummary[day - 1] = new ResponseFiguresModel()
                {
                    BusinessDate = new DateTime(month.Year, month.Month, day),
                    Occupancy = new PickupFigures()
                    {
                        SnapshotOneValue = (decimal)occupancy
                    },
                    AverageDailyRate = new PickupFigures()
                    {
                        SnapshotOneValue = (decimal)adr,
                        PickupValue = (decimal)puAdr
                    },
                    Revenue = new PickupFigures()
                    {
                        SnapshotOneValue = rooms1 * (decimal)price,
                        SnapshotTwoValue = rooms2 * (decimal)price,
                        PickupValue = (rooms2 * (decimal)price) - (rooms1 * (decimal)price)
                    },
                    RoomNights = new PickupFigures()
                    {
                        SnapshotOneValue = rooms1,
                        SnapshotTwoValue = rooms2,
                        PickupValue = rooms2 - rooms1
                    }
                };
            }

            return dailyPickupSummary;
        }

        public async Task<ResponseFiguresModel[]> GetSegmentedPickupAsync(Guid hotelId, DateTime pickupFrom, DateTime pickupTo, DateTime day)
        {
            var random = new Random();
            var segments = new[] {"A1", "A3", "A4", "B1", "B2", "B3", "C1", "C2", "D1" };
            var segmentPickup = new ResponseFiguresModel[segments.Length];


            for(int i = 1; i <= segments.Length; i++)
            {
                var rooms1 = random.Next(120);
                var rooms2 = random.Next(120);
                var price = 80.0 + (40.0 * random.NextDouble());
                var occupancy = random.NextDouble() * 100d;
                var adr = 60d + random.NextDouble() * 35d;
                var puAdr = -50d + random.NextDouble() * 100d;

                segmentPickup[i - 1] = new ResponseFiguresModel()
                {
                    BusinessDate = new DateTime(day.Year, day.Month, day.Day),
                    Segment = segments[i-1],
                    Occupancy = new PickupFigures()
                    {
                        SnapshotOneValue = (decimal)occupancy
                    },
                    AverageDailyRate = new PickupFigures()
                    {
                        SnapshotOneValue = (decimal)adr,
                        PickupValue = (decimal)puAdr
                    },
                    Revenue = new PickupFigures()
                    {
                        SnapshotOneValue = rooms1 * (decimal)price,
                        SnapshotTwoValue = rooms2 * (decimal)price,
                        PickupValue = (rooms2 * (decimal)price) - (rooms1 * (decimal)price)
                    },
                    RoomNights = new PickupFigures()
                    {
                        SnapshotOneValue = rooms1,
                        SnapshotTwoValue = rooms2,
                        PickupValue = rooms2 - rooms1
                    }
                };
            }

            return segmentPickup;
        }
    }
}
