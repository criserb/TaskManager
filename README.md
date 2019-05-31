# TaskManager

Task Manager is a simple demonstration of database based application that lets you store a list of tasks. 
It is developed based upon MVVM pattern. The application stores and updates data in database with the help of stored procedures.

The application requires a database to store the data. Follow the below steps to setup database.

1.       Run the script 'Movie Catalog.sql' to create and populate database (MS SQL SERVER is required)

2.       Set the connection string

   i.         Open MovieCatalog.sln (Visual Studio is required)

   ii.       Go to Properties in Solution Explorer

   iii.      Go to Settings.settings

   iv.     Insert Name as 'TaskManagerConnectionString', Type as (Connection String), Scope as Application and Value as Connection String of Database.
   
   Author:
   Krzysztof Bądkowski
