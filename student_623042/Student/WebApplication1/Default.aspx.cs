using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        private readonly TaskManager _taskManager = new TaskManager();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var newTask = new TodoTask
                {
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    CreatedDate = DateTime.Now
                };

                _taskManager.AddTask(newTask);
                ClearForm();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            Response.Redirect($"EditTask.aspx?id={btn.CommandArgument}");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            Response.Redirect($"Delete.aspx?id={btn.CommandArgument}");
        }

        private void ClearForm()
        {
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
    }
}