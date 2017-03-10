using System;
using System.Threading.Tasks;
using Fairmas.PickupTracking.Shared.Interfaces;
using Fairmas.PickupTracking.Shared.Models;
using System.Collections.Generic;

namespace Fairmas.PickupTracking.Shared.Services
{
    public class PickupDataService : IPickupDataService
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
            //// API
            //// https://put-development.fairplanner.net/api/PickupTracking/PickupTracking/MonthlyPickupSummary

            var parameter = new[]
            {
                new KeyValuePair<string, string>("from", DateTime.Today.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("to", DateTime.Today.AddMonths(24).ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("pickupFrom", pickupFrom.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("pickupTo", pickupTo.ToString("yyyy-MM-dd")),
                //new KeyValuePair<string, string>("_", "1481968644632")
            };

            var requestedFigures = new RequestFiguresModel
            {
                Revenue = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                RoomNights = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                AvailableRooms = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                AverageDailyRate = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                AverageDailyRate2 = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                Occupancy = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                OutOfOrderRooms = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                OutOfServiceRooms = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                Overbooking = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                RevPar = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
            };

            var client = ServiceLocator.FindService<IWebApiClientService>();
            var changed = await client.ChangePropertyAsync(hotelId);

            if (!changed)
                throw new InvalidOperationException("Cannot switch to the selected property at the PickupTracking server.");

            var monthlyPickupSummary = await client.PostAsync<ResponseFiguresModel[], RequestFiguresModel>("PickupTracking/PickupTracking/", "MonthlyPickupSummary", requestedFigures, parameter);

            return monthlyPickupSummary;
        }

        public async Task<ResponseFiguresModel[]> GetDailyPickupSummaryAsync(Guid hotelId, DateTime pickupFrom, DateTime pickupTo, DateTime month)
        {
            //// API
            //// https://put-development.fairplanner.net/api/PickupTracking/PickupTracking/DailyPickupSummary

            var parameter = new[]
            {
                new KeyValuePair<string, string>("from", DateTime.Today.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("to", DateTime.Today.AddMonths(24).ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("pickupFrom", pickupFrom.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("pickupTo", pickupTo.ToString("yyyy-MM-dd")),
                //new KeyValuePair<string, string>("_", "1481968644632")
            };

            var requestedFigures = new RequestFiguresModel
            {
                Revenue = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                RoomNights = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                AvailableRooms = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                AverageDailyRate = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                AverageDailyRate2 = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                Occupancy = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                OutOfOrderRooms = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                OutOfServiceRooms = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                Overbooking = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                RevPar = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
            };

            var client = ServiceLocator.FindService<IWebApiClientService>();
            var changed = await client.ChangePropertyAsync(hotelId);

            if (!changed)
                throw new InvalidOperationException("Cannot switch to the selected property at the PickupTracking server.");

            var dailyPickupSummary = await client.PostAsync<ResponseFiguresModel[], RequestFiguresModel>("PickupTracking/PickupTracking/", "DailyPickupSummary", requestedFigures, parameter);

            return dailyPickupSummary;
        }

        public async Task<ResponseFiguresModel[]> GetSegmentedPickupAsync(Guid hotelId, DateTime pickupFrom, DateTime pickupTo, DateTime day)
        {
            //// API
            //// https://put-development.fairplanner.net/api/PickupTracking/PickupTracking/SegmentedPickupSummary

            var parameter = new[]
            {
                new KeyValuePair<string, string>("from", DateTime.Today.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("to", DateTime.Today.AddMonths(24).ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("pickupFrom", pickupFrom.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("pickupTo", pickupTo.ToString("yyyy-MM-dd")),
                //new KeyValuePair<string, string>("_", "1481968644632")
            };

            var requestedFigures = new RequestFiguresModel
            {
                Revenue = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                RoomNights = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                AvailableRooms = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                AverageDailyRate = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                AverageDailyRate2 = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                Occupancy = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                OutOfOrderRooms = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                OutOfServiceRooms = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                Overbooking = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
                RevPar = new FigureOptions() { IncludePickupValue = true, IncludeSnapshotOneValue = true, IncludeSnapshotTwoValue = true },
            };

            var client = ServiceLocator.FindService<IWebApiClientService>();
            var changed = await client.ChangePropertyAsync(hotelId);

            if (!changed)
                throw new InvalidOperationException("Cannot switch to the selected property at the PickupTracking server.");

            var segmentedPickupSummary = await client.PostAsync<ResponseFiguresModel[], RequestFiguresModel>("PickupTracking/PickupTracking/", "SegmentedPickupSummary", requestedFigures, parameter);

            return segmentedPickupSummary;
        }
    }
}
