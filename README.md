# YMMC Container Tracker

## Prerequisites
- Windows domain account (YMMCDOM)
- Member of `ReturnContainerApp` AD security group
- Access to `YMMC-SQL1\Apps_DB` database
- Visual Studio 2022 with ASP.NET workload

## Setup
1. Clone repository
2. Update `appsettings.json` with connection string
3. Run with IIS Express profile
4. Access granted if in AD group + UserRoles table

## Roles
- **Admin**: Full access
- **Editor**: Can edit and view
- **Viewer**: Read-only access

## Troubleshooting
- Check if you're in AD group: `whoami /groups | findstr ReturnContainerApp`
- Verify database access: Query `UserRoles` table
- Check logs in Visual Studio Output window