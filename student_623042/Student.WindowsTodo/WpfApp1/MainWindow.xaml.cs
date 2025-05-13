using System.Collections.Generic;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private TaskManager taskManager = new TaskManager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAddName_Click(object sender, RoutedEventArgs e)
        {
            var name = txtTaskName.Text.Trim();
            var description = txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Пожалуйста, введите название задачи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создаем задачу с использованием свойства Title
            var task = new Task { Title = name, Description = description };
            taskManager.AddTask(task);
            UpdateTaskList();

            txtTaskName.Clear();
            txtDescription.Clear();
        }

        private void btnRemoveTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstNames.SelectedItem is Task selectedTask)
            {
                taskManager.RemoveTask(selectedTask);
                UpdateTaskList();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите задачу для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateTaskList()
        {
            lstNames.Items.Clear();
            foreach (var task in taskManager.GetTasks())
            {
                lstNames.Items.Add(task);
            }
        }
    }

    public class Task
    {
        public string Title { get; set; } 
        public string Description { get; set; } 

        public override string ToString()
        {
            return $"{Title}-{Description}"; 
        }
    }

    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public void RemoveTask(Task task)
        {
            tasks.Remove(task);
        }

        public List<Task> GetTasks()
        {
            return tasks;
        }
    }
}
