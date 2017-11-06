namespace UxParticles.Demo.ListBoxPerformance
{
    using System;

    using UxParticles.Demo.Libs.ViewModel;

    public class DemoItem
    {
        public DemoItem(int columnCount)
        {
            this.ColumnCount = columnCount;
            this.Columns = new string[columnCount];

            for (int i = 0; i < columnCount; i++)
            {
                this.Columns[i] = Guid.NewGuid().ToString().Substring(0, 4);
            }
        }

        public int ColumnCount { get; set; }

        public DemoColumn[] Widths { get; set; }

        public string[] Columns { get; set; }
    }

    public class DemoColumn : NotifyPropertyChangedEx
    {
        private double width;

        public double Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
                this.OnPropertyChanged();
            }
        }
    }
}