
# Folder Watcher App (文件夹监控应用)

这是一个基于 .NET WinForms 的桌面应用程序，用于监控一个或多个文件夹中的文件变化（创建、修改、删除、重命名），并在事件发生时通过电子邮件发送通知。

## 项目结构

```
FolderWatcherApp/
├── Models/                  # 存放数据模型 (实体类)
│   ├── Setting.cs           # 主设置模型，包含 SMTP 和邮件模板信息
│   ├── MonitoredFolder.cs   # 被监控的文件夹的数据模型
│   └── Recipient.cs         # 收件人的数据模型
├── Migrations/              # Entity Framework Core 的数据库迁移文件
│   └── ...
├── AppDbContext.cs          # 数据库上下文，负责与 SQLite 数据库交互
├── Form1.cs                 # 应用程序的主窗口，用作实时日志查看器
├── Form1.Designer.cs        # 主窗口的 UI 控件和布局设计
├── SettingsForm.cs          # 统一的设置窗口，包含所有配置选项卡
├── SettingsForm.Designer.cs # 设置窗口的 UI 控件和布局设计
├── Program.cs               # 应用程序的入口点
├── FolderWatcherApp.csproj  # C# 项目文件，定义项目属性和依赖项
└── folder_watcher.db        # SQLite 数据库文件，存储所有配置
```

### 文件和文件夹作用详解

#### `Models/` 文件夹
这个文件夹包含了所有与数据库表对应的实体类，是应用程序的数据核心。

- **`Setting.cs`**: 这是最核心的设置类。它本身不包含太多数据，但通过导航属性关联了所有的文件夹和收件人。它还存储了 SMTP 服务器和邮件模板的配置。
- **`MonitoredFolder.cs`**: 代表一个需要被监控的文件夹。每条记录都包含一个文件夹的完整路径，并与 `Setting` 相关联。
- **`Recipient.cs`**: 代表一个邮件收件人。每条记录都包含一个邮箱地址，并与 `Setting` 相关联。

#### `Migrations/` 文件夹
这个文件夹由 Entity Framework Core 自动管理。每当数据模型发生变化时，我们都会创建一个新的“迁移”(migration)，EF Core 会在这里生成代码来记录如何更新数据库结构。

#### `AppDbContext.cs`
这是 Entity Framework Core 的核心文件。它定义了应用程序如何连接到数据库，以及哪些数据表 (`DbSet`) 是可用的。所有对数据库的读写操作都通过这个类来进行。

#### `Form1.cs` / `Form1.Designer.cs`
这是用户看到的主窗口。我们把它设计成了一个实时日志中心。

- **`Form1.cs`**: 包含了主窗口的所有逻辑，比如启动/停止监控、处理文件系统事件、调用邮件发送服务以及更新界面状态。
- **`Form1.Designer.cs`**: 由 Visual Studio 设计器自动生成，定义了主窗口上的所有 UI 控件（如日志框、按钮、状态标签）的属性和布局。

#### `SettingsForm.cs` / `SettingsForm.Designer.cs`
这是统一的设置窗口，用户所有的配置都在这里完成。

- **`SettingsForm.cs`**: 包含了设置窗口的所有逻辑，比如从数据库加载设置到不同的选项卡，处理添加/删除文件夹和收件人的操作，以及在窗口关闭时自动保存所有更改。
- **`SettingsForm.Designer.cs`**: 定义了设置窗口的 `TabControl` (选项卡)以及每个选项卡内的所有 UI 控件。

#### `Program.cs`
这是整个应用程序的入口。它的核心作用是：
1.  初始化应用程序。
2.  创建一个**全局唯一**的 `AppDbContext` 实例，并确保数据库已根据最新的迁移创建好。
3.  启动主窗口 `Form1`，并将数据库上下文实例传递给它，以确保整个应用共享同一个数据连接。

#### `folder_watcher.db`
这是 SQLite 数据库文件。它是一个轻量级的、基于文件的数据库，存储了用户的所有配置。如果删除这个文件，下次启动程序时会自动根据最新的数据模型重新创建一个空的数据库。

## 如何构建和运行

1.  确保已安装 .NET SDK (版本 8.0 或更高)。
2.  在项目根目录 (`FolderWatcherApp/`) 打开终端。
3.  运行 `dotnet build` 来编译项目。
4.  运行 `dotnet run` 来启动应用程序。

