

This web application is for displaying my Visual Studio.NET 2022 solution for an employer to see my architecture and coding practices in both software and databases.  Downloading the code is welcomed.  The code is written in Visual Studio 2022 with a SQL Server 2022 backend.  There are SQL scripts included for setting up the database.  Just follow the directions in this file.



Requirements for running ChefsRegistry

-SQL Server 2022 database or above

-Make sure the following is installed on the machine that will execute the application
-SSMS (SQL Server Management Studio) 2021 or above
-Visual Studio 2022 or above
-.NET 9.0 Framework


Disclaimer
Since this is an ongoing effort and new files are uploaded when new features are added the application is in debug mode and must be run from Visual Studio.  A release deployment is coming.  I am currently working on deploying it in IIS (Windows 10 or 11).

Steps for setting up ChefsRegistry Web Application 

1)  In SQL Server Management Studio that has a connection to a SQL Server, execute the file ..\ChefsRegistry\Database Scripts\CreateDatabase.sql.  This will create the a Database named Chef and the Tables, Primary Keys, Foreign Keys, Indexes, UDTT, and Stored Procedures needed to support the application. A DeleteDatabase script is also included for removal purposes. 

2)  On any machine that has Visual Studio .NET 2022 or later installed copy the files and folders located in GitHub https://github.com/evanhelden/ChefsRegistry

3)  Update the appsettings.json file, set the Server=VANHEL to your SQL Server name in both the MSSqlServer and SqlConnection configuration settings.

4)  Open the file ..\ChefsRegistry\ChefsRegistry.sln in Visual Studio.  Run the application. 



