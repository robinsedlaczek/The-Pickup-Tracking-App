using Fairmas.PickupTracking.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fairmas.PickupTracking.Shared.DesignTimeData
{
    public class SampleData
    {
        public ObservableCollection<PickupFiguresViewModel> Pickup
        {
            get
            {
                return new ObservableCollection<PickupFiguresViewModel>
                {
                    new PickupFiguresViewModel()
                    {
                        Segment = "TLK",
                        BusinessDate = new DateTime(2016, 10, 1),
                        Occ = "90 %",
                        RN = 40000,
                        ADR = 555.00m,
                        Rev = 22500.0m,
                        PURN = 122,
                        PUADR = 102.0m,
                        PURev = 12.4m
                    },
                    new PickupFiguresViewModel()
                    {
                        Segment = "GRP",
                        BusinessDate = new DateTime(2016, 11, 1),
                        Occ = "83 %",
                        RN = 8255,
                        ADR = 75.18m,
                        Rev = 620.6m,
                        PURN = 44,
                        PUADR = 122.0m,
                        PURev = 5.4m
                    },
                    new PickupFiguresViewModel()
                    {
                        Segment = "CKH",
                        BusinessDate = new DateTime(2016, 11, 2),
                        Occ = "68 %",
                        RN = 255,
                        ADR = 90.20m,
                        Rev = 120.6m,
                        PURN = -24,
                        PUADR = 122.0m,
                        PURev = -125.4m
                    },
                    new PickupFiguresViewModel()
                    {
                        Segment = "CRK",
                        BusinessDate = new DateTime(2016, 11, 5),
                        Occ = "89 %",
                        RN = 563,
                        ADR = 120.20m,
                        Rev = 3420.6m,
                        PURN = 244,
                        PUADR = 102.0m,
                        PURev = 1425.4m
                    },
                    new PickupFiguresViewModel()
                    {
                        Segment = "LES",
                        BusinessDate = new DateTime(2016, 12, 1),
                        Occ = "65 %",
                        RN = 6555,
                        ADR = 72.41m,
                        Rev = 474.6m,
                        PURN = -17,
                        PUADR = -156.0m,
                        PURev = -2.7m
                    }
                };
            }
        }

    }
}
