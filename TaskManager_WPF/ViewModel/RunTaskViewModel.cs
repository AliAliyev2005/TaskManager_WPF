using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;

namespace TaskManager_WPF.ViewModel
{
    public class RunTaskViewModel : INotifyPropertyChanged
    {
        private string taskName;
        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; OnPropertyChanged(); }
        }

        public RelayCommand RunTaskCommand { get; set; }

        public RunTaskViewModel()
        {
            RunTaskCommand = new RelayCommand(RunTask);
        }

        public void RunTask()
        {
            try
            {
                Process.Start(TaskName);
            }
            catch (Exception)
            {
                MessageBox.Show("Process name is not correct !");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}