using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using TaskManager_WPF.View;

namespace TaskManager_WPF.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Process> tasks;
        public ObservableCollection<Process> Tasks
        {
            get { return tasks; }
            set { tasks = value; OnPropertyChanged(); }
        }

        public RelayCommand<DataGrid> EndTaskCommand { get; set; }
        public RelayCommand RunNewTaskCommand { get; set; }
        public RelayCommand ExitCommand { get; set; }

        public MainViewModel()
        {
            Tasks = new ObservableCollection<Process>();

            GetTasks();

            EndTaskCommand = new RelayCommand<DataGrid>(EndTask);
            RunNewTaskCommand = new RelayCommand(RunNewTask);
            ExitCommand = new RelayCommand(() => { Application.Current.Shutdown(); });
        }

        public void RunNewTask()
        {
            RunTask runTask = new RunTask();
            runTask.ShowDialog();
        }

        public void EndTask(DataGrid dataGrid)
        {
            if (dataGrid.SelectedItem != null)
            {
                var process = dataGrid.SelectedItem as Process;
                process.Kill();
                GetTasks();
            }
            else MessageBox.Show("You must select a task !");
        }

        private void GetTasks()
        {
            Tasks.Clear();
            foreach (Process process in Process.GetProcesses())
            {
                Tasks.Add(process);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
