using ESSTester.UI.Local.ViewModels;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ESSTester.UI.UI.Units
{
    public class AlarmListView : ListView
    {
        static AlarmListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AlarmListView), new FrameworkPropertyMetadata(typeof(AlarmListView)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new AlarmListViewItem();
        }

        public AlarmListView()
        {
            this.SelectionChanged += AlarmListView_SelectionChanged;
        }

        private void AlarmListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem is AlarmItem item)
            {
                SelectionCommand?.Execute(item);
                SelectedItem = null;
            }
        }

        public ICommand SelectionCommand

        {
            get { return (ICommand)GetValue(SelectionCommandProperty); }
            set { SetValue(SelectionCommandProperty, value); }
        }

        public static readonly DependencyProperty SelectionCommandProperty =
            DependencyProperty.Register("SelectionCommand", typeof(ICommand), typeof(AlarmListView), new PropertyMetadata(null));


    }
}
