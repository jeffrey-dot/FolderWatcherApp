using Microsoft.EntityFrameworkCore;
using System;
using System.Windows.Forms;

namespace FolderWatcherApp
{
    /// <summary>
    /// 应用程序的入口点
    /// </summary>
    static class Program
    {
        /// <summary>
        ///  应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 为整个应用程序生命周期创建一个单一的数据库上下文实例
            // 这样可以确保所有窗口共享同一个数据连接，避免数据不同步的问题
            using var dbContext = new AppDbContext();
            // 确保数据库已创建并且所有迁移都已应用
            // dbContext.Database.Migrate();

            // 运行主窗口，并将数据库上下文传递给它
            Application.Run(new Form1(dbContext));
        }
    }
}