using Fairmas.PickupTracking.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fairmas.PickupTracking.Shared.Interfaces
{
    public interface IPickupDataService
    {
        Task<Hotel[]> GetHotelsAsync();

        Task<ResponseFiguresModel[]> GetMonthlyPickupSummaryAsync(Guid hotelId, DateTime pickupFrom, DateTime pickupTo);

        Task<ResponseFiguresModel[]> GetDailyPickupSummaryAsync(Guid hotelId, DateTime pickupFrom, DateTime pickupTo, DateTime month);

        Task<ResponseFiguresModel[]> GetSegmentedPickupAsync(Guid hotelId, DateTime pickupFrom, DateTime pickupTo, DateTime day);
    }
}
