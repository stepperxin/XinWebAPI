The project is strucutred by folders:
  - XinIndentity folders are containing the logic to manage Identity Management and Jwt Authentication
  - PlayGround folders are containing sample code for test reasons

DB setup
-----------------------------------
DB Context: are the bridge to che DB Data
- XinIdentityDBContext: is the context where only Aspnet Core identity tables are set
- PlayGround: is the Context where some sample table are used for test purpose. It is inehriting from XinIdentityDBContext

DB Install:
- Code First: please use the following commands from Package Manager Console
    Add-Migration 'Name' -Context $DBContextName$
    Update-Database -Context $DBContextName$
  
SQL Script: locate the folders under Data -> SQL Scripts and run them following the order specified in the name: 01.XinIdentity...

App Settings
------------------------------------
- CurrentDBContext: define which is the context to load in the Program.cs. Please makle sure there is < connection string named like this in the ConnectionStrings section
