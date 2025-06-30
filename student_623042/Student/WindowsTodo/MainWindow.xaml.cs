using System.Windows;
using Student.Todo.Services;
using Student.Todo.Models;

namespace Student.WindowsTodo
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Менеджер задач для управления списком задач
        /// </summary>
        private TaskManager taskManager = new TaskManager();

        public MainWindow()
        {
            InitializeComponent();
            RefreshTaskList();
        }

        // Кнопка добавления задачи
        private void btnAddName_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTaskName.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Пожалуйста, введите название задачи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Пожалуйста, введите описание задачи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            taskManager.AddTask(new TodoTask(title, description));
            RefreshTaskList();

            txtTaskName.Clear();
            txtDescription.Clear();
        }

        // Кнопка удаления задачи
        private void btnRemoveTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstNames.SelectedItem is TodoTask selectedTask)
            {
                taskManager.RemoveTask(selectedTask);
                RefreshTaskList();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите задачу для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Обновить список задач
        /// </summary>
        private void RefreshTaskList()
        {
            lstNames.Items.Clear();
            foreach (var task in taskManager.GetTasks())
            {
                lstNames.Items.Add(task);
            }
        }
    }
}