using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UxParticles.Demo.Libs.ViewModel;

namespace UxParticles.Demo.ListBoxPerformance
{
    using System.Windows;

    using UxParticles.Demo.Libs.Debug;

    public class MainWindowViewModel : NotifyPropertyChangedEx
    {
        private string status;

        private ObservableCollection<DemoItem> items = new ObservableCollection<DemoItem>();

        private bool canStart;

        public MainWindowViewModel()
        {
            this.StartCommand  = new RelayCommand(x => this.Start(), x => true);
            this.ChangeColumnsCommand = new RelayCommand(x => this.ChangeColumns(), x => true);
            this.CanStart = true;
        }

        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand StartCommand { get; }
        public ICommand ChangeColumnsCommand { get; }

        public void ChangeColumns()
        {
            if (!this.CanStart)
            {
                return;
            }

            App.Current.Dispatcher.Log("Column Change", ms => this.Status = $"Done in {ms} ms");           
            foreach (var demoColumn in Columns)
            {
                demoColumn.Width = 22;
            }
            
            

        }
        public void Start()
        {
            if (!this.CanStart)
            {
                return;
            }

            this.CanStart = false;
            this.Status = "Running...";
            var items = new List<DemoItem>();
            var columnCount = 52;
            Task.Run(
                () =>
                    {
                        var columns = new DemoColumn[columnCount];
                        for (int i = 0; i < columnCount; i++)
                        {
                            columns[i] = new DemoColumn();
                            columns[i].Width = 45;
                        }

                        for (var i = 0; i < 100; i++)
                        {
                            items.Add(new DemoItem(columnCount) {Widths = columns});
                        }
                    }).ContinueWith(
                        x =>
                            {
                                App.Current.Dispatcher.Invoke(
                                    () =>
                                        {
                                            App.Current.Dispatcher.Log("Items", ms => this.Status = $"Done in {ms} ms");
                                            this.CanStart = true;
                                            this.Items.Clear();
                                            foreach (var demoItem in items)
                                            {
                                              this.Items.Add(demoItem);  
                                            }
                                            //this.Items = new ObservableCollection<DemoItem>(items);
                                            this.Columns = this.Items.FirstOrDefault()?.Widths;
                                            this.Status = "Done!";
                                        });

                            });
        }

        public DemoColumn[] Columns { get; set; }

        public ObservableCollection<DemoItem> Items
        {
            get
            {
                return this.items;
            }
            set
            {
                this.items = value;
                this.OnPropertyChanged();
            }
        }

        public bool CanStart
        {
            get
            {
                return this.canStart;
            }
            set
            {
                this.canStart = value;
            }
        }
    }
}
