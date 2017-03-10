namespace Fairmas.PickupTracking.Shared.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            if (LoginViewModel == null)
                LoginViewModel = new LoginViewModel();

            if (SettingsViewModel == null)
                SettingsViewModel = new SettingsViewModel();

            if (HotelListViewModel == null)
                HotelListViewModel = new HotelListViewModel();

            if (MonthlyPickupSummaryViewModel == null)
                MonthlyPickupSummaryViewModel = new MonthlyPickupSummaryViewModel();

            if (DailyPickupSummaryViewModel == null)
                DailyPickupSummaryViewModel = new DailyPickupSummaryViewModel();

            if (SegmentedPickupViewModel == null)
                SegmentedPickupViewModel = new SegmentedPickupViewModel();
        }

        public static LoginViewModel LoginViewModel { get; private set; }

        public static HotelListViewModel HotelListViewModel { get; private set; }

        public static MonthlyPickupSummaryViewModel MonthlyPickupSummaryViewModel { get; private set; }

        public static DailyPickupSummaryViewModel DailyPickupSummaryViewModel { get; set; }

        public static SegmentedPickupViewModel SegmentedPickupViewModel { get; set; }

        public static SettingsViewModel SettingsViewModel { get; }
    }
}
