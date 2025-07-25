﻿using Microsoft.EntityFrameworkCore;
using Student.Todo.Data;
using Student.Todo.Models;
using Student.Todo.Services;
using System;
using System.Linq;
using System.Configuration;
using System.Web.UI;

namespace Student.WebFormsTodo
{
    public partial class EditTask : Page
    {
        private ITodoRepository _efRepository;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["title"] != null && Request.QueryString["description"] != null)
                {
                    string title = Request.QueryString["title"];
                    string description = Request.QueryString["description"];

                    var task = _taskManager.GetTasks().FirstOrDefault(t => t.Title == title && t.Description == description);

                    if (task != null)
                    {
                        ViewState["OriginalTitle"] = task.Title;
                        ViewState["OriginalDescription"] = task.Description;
                    }
                    else
                    {
                        Response.Redirect("TasksList.aspx");
                    }
                }
                else
                {
                    Response.Redirect("TasksList.aspx");
                }
            }
        }

        // Обработывает события нажатия кнопки обновления задачи
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string originalTitle = ViewState["OriginalTitle"].ToString();
                string originalDescription = ViewState["OriginalDescription"].ToString();

                var taskToUpdate = _taskManager.GetTasks().FirstOrDefault(t => t.Title == originalTitle && t.Description == originalDescription);

                if (taskToUpdate != null)
                {
                    Response.Redirect("TasksList.aspx");
                }
                else
                {
                    Response.Redirect("TasksList.aspx");
                }
            }
        }

        // Вернуться на страницу списка задач
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TasksList.aspx");
        }

        private readonly TaskManager _taskManager = new TaskManager();
    }
}