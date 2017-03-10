using System;

namespace Fairmas.PickupTracking.Shared.ViewModels
{
    public class PickupFiguresViewModel : ViewModelBase
    {
        public string Segment { get; set; }

        public decimal ADR { get; internal set; }

        public DateTime? BusinessDate { get; internal set; }

        public string Occ { get; internal set; }

        public decimal PUADR { get; internal set; }

        public decimal PURev { get; internal set; }

        public decimal PURN { get; internal set; }

        public decimal Rev { get; internal set; }

        public decimal RN { get; internal set; }
    }
}