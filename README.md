# YMMC Container Tracker

> Enterprise web application for managing and tracking returnable containers with comprehensive audit logging and role-based access control.

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Razor%20Pages-512BD4)](https://docs.microsoft.com/aspnet/core/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3.3-7952B3?logo=bootstrap)](https://getbootstrap.com/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2019+-CC2927?logo=microsoftsqlserver)](https://www.microsoft.com/sql-server/)

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [User Roles & Permissions](#user-roles--permissions)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Security & Authentication](#security--authentication)
- [Database Schema](#database-schema)
- [Troubleshooting](#troubleshooting)
- [Technology Stack](#technology-stack)

---

## Overview

YMMC Container Tracker is a full-stack web application designed to manage returnable container inventory with enterprise-grade security, audit logging, and role-based access control. Built for YMMC (Yamaha Motor Manufacturing Corporation), this system provides real-time tracking of container movements, modifications, and user activities.

### Key Highlights

- **Windows Authentication**: Seamless integration with Active Directory (YMMCDOM)
- **Real-time Tracking**: Monitor container status and locations
- **Comprehensive Audit Logs**: Track all changes with user attribution
- **Role-Based Access**: Granular permissions (Admin, Editor, Viewer)
- **Modern UI**: Responsive design with Bootstrap 5
- **Performance**: Optimized database queries with Entity Framework Core

---

## Features

### Container Management
- **CRUD Operations**: Create, Read, Update, Delete container records
- **Advanced Search**: Multi-field search with filtering (Item No, Packing Code, Prefix Code, etc.)
- **Inline Editing**: Edit records directly in the table (HTMX-powered)
- **Pagination**: Configurable page sizes (25, 50, 100 rows)
- **Bulk Operations**: Import containers from staging tables

### Audit & Reporting
- **Activity Logs**: Track CREATE, UPDATE, DELETE, VIEW actions
- **Timestamp Tracking**: Record exact time of each change
- **User Attribution**: Associate actions with Windows usernames
- **Advanced Filtering**: Filter by user, action, date range, item number
- **Change History**: View before/after values for all updates
- **JSON Storage**: Old and new values stored in JSON format

### Security Features
- **Windows Authentication**: Integrated with YMMCDOM Active Directory
- **LDAP Integration**: Validate group membership in real-time
- **Role-Based Access Control (RBAC)**: Three permission levels
- **Access Denial**: Clear messaging for unauthorized users
- **IP Address Logging**: Track user locations
- **User Agent Tracking**: Record browser information

### User Experience
- **Responsive Design**: Works on desktop, tablet, and mobile
- **Modern UI**: Clean Bootstrap 5 interface with icons
- **Fast Navigation**: Intuitive menu structure
- **Real-time Updates**: Dynamic content loading with HTMX
- **User-Friendly**: Context-sensitive help and clear error messages

---

## Prerequisites

### Required Software
- **Operating System**: Windows 10/11 or Windows Server 2019+
- **.NET SDK**: [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- **IDE**: [Visual Studio 2022](https://visualstudio.microsoft.com/) (17.8+) with **ASP.NET and web development** workload
- **Database**: SQL Server 2019+ or SQL Server Express

### Required Access & Permissions

#### 1. Windows Domain Account
- Active account on **YMMCDOM** domain
- Ability to authenticate with Windows credentials

#### 2. Active Directory Group Membership
- Member of **`ReturnContainerApp`** AD security group
- To verify your membership:
  ```cmd
  whoami /groups | findstr ReturnContainerApp
  ```

#### 3. Database Access
- Connection access to **`YMMCSQL1`** SQL Server
- Access to **`Apps_DB`** database
- Read/Write permissions on the following tables:
  - `RtrnCotnr.RtrnCotnr_Main`
  - `RtrnCotnr.RtrnCotnr_Stage`
  - `dbo.UserRoles`
  - `dbo.ContainerAuditLogs`

#### 4. User Registration in Database
- Your username must exist in the **`UserRoles`** table
- Contact your administrator to be added with the appropriate role
- Verify your registration:
  ```sql
  SELECT * FROM [dbo].[UserRoles] WHERE Username = 'YOUR_USERNAME'
  ```

### Optional Tools
- **SQL Server Management Studio (SSMS)**: For database management and troubleshooting
- **Git**: For version control and cloning the repository
- **Postman**: For API testing (alternative to Swagger)

---

## User Roles & Permissions

The application uses a **dual-authentication system**: Users must be members of the AD group **AND** have a role assigned in the database.

### Authentication Flow

1. **Step 1: Windows Authentication** - User logs in with YMMCDOM credentials
2. **Step 2: AD Group Validation** - System checks membership in `ReturnContainerApp`
3. **Step 3: Database Role Check** - System queries `UserRoles` table for assigned role
4. **Step 4: Access Grant/Deny** - User is granted access based on role permissions

### Role Definitions

| Role | Description | Use Case |
|------|-------------|----------|
| **Admin** | Full system access with all permissions | IT administrators, system managers |
| **Editor** | Can create, edit, and view containers | Data entry personnel, warehouse managers |
| **Viewer** | Read-only access to containers and reports | Auditors, reporting staff, read-only users |

### Detailed Permissions Matrix

| Permission | Admin | Editor | Viewer |
|-----------|-------|--------|--------|
| **View Containers** | Yes | Yes | Yes |
| **Search & Filter** | Yes | Yes | Yes |
| **View Container Details** | Yes | Yes | Yes |
| **Create New Containers** | Yes | Yes | No |
| **Edit Existing Containers** | Yes | Yes | No |
| **Delete Containers** | Yes | No | No |
| **Access Maintenance** | Yes | Yes | No |
| **View Audit Reports** | Yes | Yes | Yes |
| **Export Data** | Yes | Yes | No |
| **Import from Staging** | Yes | Yes | No |

### Adding New Users

Admins can add users by inserting records into the UserRoles table:

```sql
-- Example: Add a new Admin user
INSERT INTO [dbo].[UserRoles] (Username, Role, DisplayName, Email, CreatedDate)
VALUES ('kmurtaza', 'Admin', 'Khalid Murtaza', 'kmurtaza@ymmc.com', GETUTCDATE());

-- Example: Add a new Editor user
INSERT INTO [dbo].[UserRoles] (Username, Role, DisplayName, Email, CreatedDate)
VALUES ('jdoe', 'Editor', 'John Doe', 'jdoe@ymmc.com', GETUTCDATE());

-- Example: Add a new Viewer user
INSERT INTO [dbo].[UserRoles] (Username, Role, DisplayName, Email, CreatedDate)
VALUES ('asmith', 'Viewer', 'Alice Smith', 'asmith@ymmc.com', GETUTCDATE());
```

### Updating User Roles

```sql
-- Promote user to Admin
UPDATE [dbo].[UserRoles]
SET Role = 'Admin'
WHERE Username = 'jdoe';

-- Downgrade user to Viewer
UPDATE [dbo].[UserRoles]
SET Role = 'Viewer'
WHERE Username = 'kmurtaza';
```

### Removing User Access

```sql
-- Remove user from database (they won't be able to access the app)
DELETE FROM [dbo].[UserRoles]
WHERE Username = 'jdoe';
```

---

## Installation

### Step 1: Clone the Repository

```bash
git clone https://github.com/Kmurtaza672/ymmcContainerTrackerApi.git
cd ymmcContainerTrackerApi
```

### Step 2: Open in Visual Studio

1. Open **Visual Studio 2022**
2. Click **File ? Open ? Project/Solution**
3. Navigate to the cloned folder and open `YmmcContainerTrackerApi.sln`

### Step 3: Restore NuGet Packages

```bash
dotnet restore
```

Or in Visual Studio: Right-click solution ? **Restore NuGet Packages**

### Step 4: Configure Connection String

Update `appsettings.json` with your SQL Server details:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YMMCSQL1;Database=Apps_DB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;"
  }
}
```

### Step 5: Apply Database Migrations (If Needed)

```bash
dotnet ef database update
```

### Step 6: Build the Application

```bash
dotnet build
```

Or in Visual Studio: **Build ? Build Solution** (Ctrl+Shift+B)

### Step 7: Run the Application

**Option A: Visual Studio**
- Press **F5** to start debugging
- Or press **Ctrl+F5** to run without debugging

**Option B: Command Line**
```bash
dotnet run
```

The application will be available at:
- **HTTPS**: `https://localhost:7187`
- **HTTP**: `http://localhost:5000`

---

## Configuration

### appsettings.json Structure

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YMMCSQL1;Database=Apps_DB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;"
  },
  "Authentication": {
    "Enabled": true,
    "DevelopmentUser": "YMMCDOM\\kmurtaza",
    "RequiredAdGroup": "ReturnContainerApp",
    "LDAP": {
      "Domain": "YMMCDOM.COM",
      "Path": "LDAP://OU=users,DC=YMMCDOM,DC=COM"
    }
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "YmmcContainerTrackerApi.Services": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

### Configuration Options Explained

| Setting | Description | Example |
|---------|-------------|---------|
| `DefaultConnection` | SQL Server connection string | `Server=YMMCSQL1;Database=Apps_DB;...` |
| `Authentication.Enabled` | Enable/disable authentication | `true` or `false` |
| `Authentication.DevelopmentUser` | Default user for development | `YMMCDOM\\kmurtaza` |
| `Authentication.RequiredAdGroup` | AD group name for access | `ReturnContainerApp` |
| `LDAP.Domain` | Active Directory domain | `YMMCDOM.COM` |
| `LDAP.Path` | LDAP query path | `LDAP://OU=users,DC=YMMCDOM,DC=COM` |

---

## Usage

### Accessing the Dashboard

1. Navigate to `https://localhost:7187` (or your deployed URL)
2. You will be automatically authenticated with your Windows credentials
3. If authorized, you'll see the dashboard with available options:
   - **View Containers**: Browse all containers
   - **Reports**: View audit logs and activity

### Managing Containers

#### Viewing Containers
1. Click **"View Containers"** on the dashboard
2. Browse the paginated list of containers
3. Use the **search bar** to filter results (supports Item No, Packing Code, Prefix Code, Container Number, Alternate ID)
4. Click **column headers** to sort (if enabled)

#### Creating a Container
1. Navigate to **Returnable Containers** page
2. Click **"+ Create New"** button (Admin/Editor only)
3. Fill in the form:
   - Item No (required, unique)
   - Packing Code
   - Prefix Code
   - Container Number
   - Dimensions (Length, Width, Height, Collapsed Height)
   - Weight
   - Pack Quantity
   - Alternate ID
4. Click **"Create"** to save
5. System automatically logs the CREATE action in audit logs

#### Editing a Container
1. Find the container in the list
2. Click the **pencil icon** in the Actions column (Admin/Editor only)
3. Modify the desired fields
4. Click **"Save"** to apply changes
5. System logs the UPDATE action with before/after values

#### Deleting a Container
1. Find the container in the list
2. Click the **trash icon** in the Actions column (Admin only)
3. Confirm the deletion
4. System logs the DELETE action with old values

### Viewing Audit Reports

1. Click **"Reports"** on the dashboard
2. **Apply Filters**:
   - **Item No**: Filter by specific container (e.g., `YPB-4424-25`)
   - **Username**: Filter by user who made changes (e.g., `kmurtaza`)
   - **Action**: Filter by action type (CREATE, UPDATE, DELETE, VIEW)
   - **Start Date**: Beginning of date range
   - **End Date**: End of date range
   - **Page Size**: Number of rows to display (25, 50, 100, 200)
3. Click **"Apply Filters"**
4. View results in the table
5. Click **"View"** button on any entry to see detailed change history:
   - Timestamp
   - User and Role
   - Old Values (JSON)
   - New Values (JSON)
   - Changed Fields
   - IP Address
   - Browser Information

---

## Security & Authentication

### Multi-Layer Security

The application implements **defense in depth** with multiple security layers:

#### Layer 1: Windows Authentication
- Integrated Windows Authentication via IIS
- Automatic credential capture
- No password storage required

#### Layer 2: Active Directory Validation
- Real-time LDAP queries to validate group membership
- Must be member of `ReturnContainerApp` AD group
- Prevents unauthorized domain users from accessing the system

#### Layer 3: Database Role Verification
- Username must exist in `UserRoles` table
- Role determines allowed operations
- Centralized permission management

#### Layer 4: Action-Level Authorization
- Every controller action checks user permissions
- Unauthorized actions return HTTP 403 Forbidden
- UI elements hidden based on role (buttons, links)

### Audit Logging

Every action is logged with:
- **What**: Action type (CREATE, UPDATE, DELETE, VIEW)
- **Who**: Windows username and role
- **When**: Precise timestamp (UTC)
- **Where**: IP address and user agent
- **What Changed**: Before/after values in JSON format

---

## Database Schema

### RtrnCotnr.RtrnCotnr_Main (Container Inventory)

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `Item_No` | VARCHAR(50) | PRIMARY KEY, UNIQUE | Unique container identifier |
| `Packing_Code` | VARCHAR(50) | NULL | Container packing code |
| `Prefix_Code` | VARCHAR(50) | NULL | Container prefix |
| `Container_Number` | VARCHAR(100) | NULL | Container number |
| `Outside_Length` | DECIMAL(10,2) | NULL | Outer length in cm |
| `Outside_Width` | DECIMAL(10,2) | NULL | Outer width in cm |
| `Outside_Height` | DECIMAL(10,2) | NULL | Outer height in cm |
| `Collapsed_Height` | DECIMAL(10,2) | NULL | Height when collapsed (cm) |
| `Weight` | DECIMAL(10,2) | NULL | Weight in kg |
| `Pack_Quantity` | INT | NULL | Quantity per pack |
| `Alternate_ID` | VARCHAR(100) | NULL | Alternative identifier |

### dbo.UserRoles (User Permissions)

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `Username` | VARCHAR(50) | PRIMARY KEY | Windows username (e.g., `kmurtaza`) |
| `Role` | VARCHAR(20) | NOT NULL | Admin, Editor, or Viewer |
| `DisplayName` | VARCHAR(100) | NULL | User's full name |
| `Email` | VARCHAR(100) | NULL | Email address |
| `CreatedDate` | DATETIME | DEFAULT GETUTCDATE() | Account creation date |

### dbo.ContainerAuditLogs (Audit Trail)

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `Id` | INT | PRIMARY KEY, IDENTITY | Unique log entry ID |
| `ItemNo` | VARCHAR(50) | NOT NULL | Container item number |
| `Action` | VARCHAR(20) | NOT NULL | CREATE/UPDATE/DELETE/VIEW |
| `Username` | VARCHAR(50) | NOT NULL | User who performed action |
| `UserRole` | VARCHAR(20) | NULL | User's role at time of action |
| `Timestamp` | DATETIME | NOT NULL | When action occurred (UTC) |
| `OldValues` | NVARCHAR(MAX) | NULL | JSON of previous values |
| `NewValues` | NVARCHAR(MAX) | NULL | JSON of new values |
| `ChangedFields` | VARCHAR(500) | NULL | Comma-separated changed fields |
| `IpAddress` | VARCHAR(45) | NULL | User's IP address |
| `UserAgent` | VARCHAR(500) | NULL | Browser/client information |
| `Notes` | NVARCHAR(1000) | NULL | Additional notes |

---

## Troubleshooting

### Issue 1: Access Denied - "You are not a member of the required Active Directory security group"

**Cause**: User is not in the `ReturnContainerApp` AD group.

**Solution**:
1. Verify your AD group membership:
   ```cmd
   whoami /groups | findstr ReturnContainerApp
   ```
2. If not listed, contact your IT administrator to be added to the group
3. After being added, **log out and log back in** to Windows for changes to take effect

---

### Issue 2: Access Denied - "You are not registered in the application's user database"

**Cause**: User is in the AD group but not in the `UserRoles` table.

**Solution**:
1. Check if you exist in the database:
   ```sql
   SELECT * FROM [dbo].[UserRoles] WHERE Username = 'YOUR_USERNAME'
   ```
2. If no results, ask an admin to add you:
   ```sql
   INSERT INTO [dbo].[UserRoles] (Username, Role, DisplayName, Email)
   VALUES ('YOUR_USERNAME', 'Viewer', 'Your Full Name', 'your.email@ymmc.com');
   ```
3. Refresh the application page

---

### Issue 3: Database Connection Failed

**Symptoms**: Application crashes on startup or shows "Cannot connect to database"

**Solutions**:
1. **Verify SQL Server is running**:
   - Open SQL Server Configuration Manager
   - Check that SQL Server service is running
2. **Test connection string** in SQL Server Management Studio (SSMS)
3. **Check firewall rules** allow SQL Server port (default 1433)
4. **Verify Trusted_Connection**:
   - Ensure your Windows account has database access
   - Run this in SSMS:
     ```sql
     SELECT SYSTEM_USER
     ```

---

### Issue 4: Bootstrap/Icons Not Loading

**Symptoms**: Page appears completely unstyled, no icons visible

**Solutions**:
1. **Hard refresh browser**: Press `Ctrl + Shift + R` (Chrome/Edge) or `Ctrl + F5`
2. **Clear browser cache**:
   - Chrome: `Ctrl + Shift + Delete` - Clear cached images and files
3. **Check browser console** for errors:
   - Press `F12` - Console tab - Look for 404 errors on CDN resources
4. **Verify internet connectivity** (Bootstrap/Icons load from CDN)
5. **Check if `_ViewStart.cshtml` exists** in `Pages/` folder

---

### Issue 5: Audit Logs Not Recording

**Symptoms**: Changes made to containers but no entries appear in Reports

**Solutions**:
1. **Verify `ContainerAuditLogs` table exists**:
   ```sql
   SELECT * FROM INFORMATION_SCHEMA.TABLES 
   WHERE TABLE_NAME = 'ContainerAuditLogs'
   ```
2. **Check `AuditService` is registered** in `Program.cs`:
   ```csharp
   builder.Services.AddScoped<IAuditService, AuditService>();
   ```
3. **Review application logs** in Visual Studio Output window:
   - View - Output
   - Select "Show output from: YmmcContainerTrackerApi"
4. **Check database permissions**:
   ```sql
   -- Verify you can insert into the table
   SELECT HAS_PERMS_BY_NAME('dbo.ContainerAuditLogs', 'OBJECT', 'INSERT')
   ```

---

### Issue 6: "Create New" Button Not Visible

**Cause**: User role is Viewer (read-only access)

**Solution**:
- Contact your administrator to upgrade your role to Editor or Admin
- Verify your current role:
  ```sql
  SELECT Role FROM [dbo].[UserRoles] WHERE Username = 'YOUR_USERNAME'
  ```

---

### Viewing Application Logs

**In Visual Studio**:
1. Run the application in Debug mode (F5)
2. Go to **View - Output**
3. In the dropdown, select **"YmmcContainerTrackerApi"**
4. Look for log entries with prefixes indicating severity

---

## Technology Stack

### Backend
- **Framework**: ASP.NET Core 8.0 (Razor Pages)
- **Language**: C# 12.0
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server 2019+
- **Authentication**: Windows Authentication + LDAP (System.DirectoryServices.AccountManagement)

### Frontend
- **UI Framework**: Bootstrap 5.3.3
- **Icons**: Bootstrap Icons 1.11.3
- **Interactivity**: HTMX 1.9.10
- **Styling**: Custom CSS with responsive design

### Development Tools
- **IDE**: Visual Studio 2022
- **Version Control**: Git + GitHub
- **Package Manager**: NuGet

---

## License

This project is proprietary software owned by **Yamaha Motor Manufacturing Corporation (YMMC)**. 

**All rights reserved.** Unauthorized copying, distribution, or modification of this software is strictly prohibited.

---

## Authors

- **Khalid Murtaza** - *Initial Development* - [@Kmurtaza672](https://github.com/Kmurtaza672)

---

## Support

For issues, questions, or feature requests:

- **IT Support Email**: it-support@ymmc.com
- **Internal Service Desk**: [YMMC IT Portal](https://servicedesk.ymmc.com)
- **GitHub Issues**: [Report a Bug](https://github.com/Kmurtaza672/ymmcContainerTrackerApi/issues)

---

## Version History

### v1.0.0 (January 12, 2025)
- Initial release
- Windows Authentication with LDAP integration
- Container CRUD operations
- Comprehensive audit logging system
- Bootstrap 5 responsive UI
- Three-tier role-based access control (Admin/Editor/Viewer)
- Advanced search and filtering
- Real-time activity reports

---

<div align="center">
  
**Made by YMMC IT Department**

[Repository](https://github.com/Kmurtaza672/ymmcContainerTrackerApi) • [Report Bug](https://github.com/Kmurtaza672/ymmcContainerTrackerApi/issues) • [Contact Support](mailto:it-support@ymmc.com)

</div>