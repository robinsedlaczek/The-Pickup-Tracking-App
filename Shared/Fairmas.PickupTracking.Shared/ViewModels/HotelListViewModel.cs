using System.Collections.ObjectModel;
using System.Windows.Input;
using Fairmas.PickupTracking.Shared.Commands;

namespace Fairmas.PickupTracking.Shared.ViewModels
{
    public class HotelListViewModel : ViewModelBase
    {
        #region Private Fields

        private HotelViewModel _selectedHotel;

        #endregion

        #region Construction

        public HotelListViewModel()
        {
            // [RS] When created cannonical, we use some test data in order to provide it during design-time.
            //Hotels = TestHotels;
            Hotels = new ObservableCollection<HotelViewModel>();
        }

        #endregion

        #region Public Members

        public ICommand LoadHotelListCommand
        {
            get
            {
                return new LoadHotelListCommand();
            }
        }

        public ObservableCollection<HotelViewModel> Hotels
        {
            get;
            private set;
        }

        public HotelViewModel SelectedHotel
        {
            get
            {
                return _selectedHotel;
            }

            set
            {
                Set(ref _selectedHotel, value, nameof(SelectedHotel));
            }
        }

        #endregion

        #region Private Static Members

        /// <summary>
        /// This property returns some test data that is used as design-time data.
        /// </summary>
        private static ObservableCollection<HotelViewModel> TestHotels
        {
            get
            {
                return new ObservableCollection<HotelViewModel>
                {
                    new HotelViewModel()
                    {
                        Name ="Holiday Inn Berlin",
                        City = "Berlin",
                        UpToDate = true
                    },
                    new HotelViewModel()
                    {
                        Name ="Holiday Inn Frankfurt",
                        City = "Frankfurt",
                        UpToDate = true
                    },
                    new HotelViewModel()
                    {
                        Name ="Holiday Inn München",
                        City = "München",
                        UpToDate = false
                    },
                    new HotelViewModel()
                    {
                        Name ="Intercontinental Berlin",
                        City = "Berlin",
                        UpToDate = true
                    },
                    new HotelViewModel()
                    {
                        Name ="Intercontinental Hamburg",
                        City = "Hamburg",
                        UpToDate = false
                    },
                    new HotelViewModel()
                    {
                        Name ="Holiday Inn Express Stuttgart",
                        City = "Stuttgart",
                        UpToDate = false
                    },
                    new HotelViewModel()
                    {
                        Name ="Holiday Inn Express Wiesbaden",
                        City = "Wiesbaden",
                        UpToDate = true
                    },
                };
            }
        } 

        #endregion
    }
}
