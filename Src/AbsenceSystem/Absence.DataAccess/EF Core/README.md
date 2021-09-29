# Migrations guide

After applying migrations please run the following SQL commands on the Absence database in order for the Python scripts to function:

```SQL 
CREATE USER [Absence] FOR LOGIN [Absence] WITH DEFAULT_SCHEMA=[dbo]
GO
USE [Absence]
GO
ALTER ROLE [db_datareader] ADD MEMBER [Absence]
GO
USE [Absence]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [Absence]
GO
USE [Absence]
GO
ALTER ROLE [db_owner] ADD MEMBER [Absence]
GO
```