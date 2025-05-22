using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

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
        }

        // Кнопка для добавления задачи
        private void btnAddName_Click(object sender, RoutedEventArgs e)
        {
            var name = txtTaskName.Text.Trim();
            var description = txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Пожалуйста, введите название задачи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var task = new Task(name, description);
            taskManager.AddTask(task);
            UpdateInterfaceTaskList();

            txtTaskName.Clear();
            txtDescription.Clear();
        }

        // Кнопка удаления задачи
        private void btnRemoveTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstNames.SelectedItem is Task selectedTask)
            {
                taskManager.RemoveTask(selectedTask);
                UpdateInterfaceTaskList();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите задачу для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Обновление списка задач
        /// </summary>
        private void UpdateInterfaceTaskList()
        {
            lstNames.Items.Clear();
            foreach (var task in taskManager.GetTasks())
            {
                lstNames.Items.Add(task);
            }
        }
    }
}