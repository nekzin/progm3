using ContextLibrary;
using ContextLibrary.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace AutoServ
{
    public partial class MainWindow : Window
    {
        private readonly ApplicationContext _context;

        public ObservableCollection<Request> Requests { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _context = new ApplicationContext();
            Requests = new ObservableCollection<Request>(_context.Requests);
            DataContext = this;
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddEditRequestWindow(canEdit: true);
            if (addWindow.ShowDialog() == true)
            {
                Requests.Add(addWindow.Request);
                _context.Requests.Add(addWindow.Request);
            }
        }

        private void EditRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RequestList.SelectedItem is Request selectedRequest)
            {
                var requestCopy = new Request
                {
                    Number = selectedRequest.Number,
                    AddedDate = selectedRequest.AddedDate,
                    CarType = selectedRequest.CarType,
                    CarModel = selectedRequest.CarModel,
                    Description = selectedRequest.Description,
                    ClientLFP = selectedRequest.ClientLFP,
                    PhoneNumber = selectedRequest.PhoneNumber,
                    Status = selectedRequest.Status
                };

                var editWindow = new AddEditRequestWindow(requestCopy, canEdit: false);
                if (editWindow.ShowDialog() == true)
                {
                    int index = Requests.IndexOf(selectedRequest);
                    Requests[index] = editWindow.Request;
                    int contextIndex = _context.Requests.IndexOf(selectedRequest);
                    if (contextIndex >= 0)
                    {
                        _context.Requests[contextIndex] = editWindow.Request;
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
