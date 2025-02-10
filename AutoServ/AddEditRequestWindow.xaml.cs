using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ContextLibrary;
using ContextLibrary.Entities;

namespace AutoServ
{
    public partial class AddEditRequestWindow : Window, INotifyPropertyChanged
    {
        public Request Request { get; set; }
        public List<CarType> CarTypes { get; } = Enum.GetValues(typeof(CarType)).Cast<CarType>().ToList();
        public List<RequestStatus> RequestStatuses { get; } = Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>().ToList();
        public List<Mechanic> Mechanics { get; } 
        public Mechanic SelectedMechanic { get; set; } 

        private readonly ApplicationContext _context;

        public event PropertyChangedEventHandler PropertyChanged;

        public AddEditRequestWindow(Request request = null, bool canEdit = true)
        {
            InitializeComponent();
            _context = new ApplicationContext();
            Mechanics = _context.Mechanics; 

            Request = request ?? new Request
            {
                CarModel = "Не указано",
                Description = "Не указано",
                ClientLFP = "Не указано",
                PhoneNumber = "Не указано",
                AddedDate = DateTime.Now,
                Status = RequestStatus.New,
                CarType = CarType.SmallCar
            };

            SelectedMechanic = _context.MechanicRequests
                .FirstOrDefault(mr => mr.Request == Request)?.Mechanic;

            DataContext = this;
            SetFieldsEditable(canEdit);
        }
        private void SetFieldsEditable(bool canEdit)
        {

            CarModelTextBox.IsEnabled = canEdit;
            ClientLFPTextBox.IsEnabled = canEdit;
            PhoneNumberTextBox.IsEnabled = canEdit;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                if (SelectedMechanic != null)
                {
                    _context.MechanicRequests.Add(new MechanicRequests
                    {
                        Mechanic = SelectedMechanic,
                        Request = Request
                    });
                }
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Некорректные данные!");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool Validate()
        {
            return !string.IsNullOrWhiteSpace(Request.CarModel) &&
                   !string.IsNullOrWhiteSpace(Request.Description) &&
                   !string.IsNullOrWhiteSpace(Request.ClientLFP) &&
                   !string.IsNullOrWhiteSpace(Request.PhoneNumber);
        }
    }
}
