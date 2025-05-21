using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student.WindowsTodo
{
	/// <summary>
	/// ���������� ��������
	/// </summary>
	public class TaskManager
	{
		private List<Task> tasks = new List<Task>();

		/// <summary>
		/// �������� ������
		/// </summary>
		/// <param name="task">������</param>
		public void AddTask(Task task)
		{
			tasks.Add(task);
		}

		/// <summary>
		/// ������� ������
		/// </summary>
		/// <param name="task">������</param>
		public void RemoveTask(Task task)
		{
			tasks.Remove(task);
		}

		/// <summary>
		/// �������� ������ �����
		/// </summary>
		/// <returns>������ �����</returns>
		public List<Task> GetTasks()
		{
			return tasks;
		}
	}
}