-- Create UserRoles table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserRoles' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[UserRoles] (
        [Username] NVARCHAR(50) NOT NULL PRIMARY KEY,
        [Role] NVARCHAR(20) NOT NULL,
        [CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [LastModified] DATETIME2 NULL,
        [DisplayName] NVARCHAR(100) NULL,
        [Email] NVARCHAR(100) NULL
    );
    
    PRINT 'UserRoles table created successfully.';
END
ELSE
BEGIN
    PRINT 'UserRoles table already exists.';
END
GO

-- Insert sample users (replace with your actual usernames)
INSERT INTO [dbo].[UserRoles] ([Username], [Role], [DisplayName], [Email])
VALUES 
    ('kmurtaza', 'Admin', 'Khalid Murtaza', 'kmurtaza@company.com'),
    ('testuser', 'Editor', 'Test User', 'testuser@company.com');

PRINT 'Sample users added.';
GO