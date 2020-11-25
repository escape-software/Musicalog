# Musicalog
Musicalog music store web catalog.

Installation
------------

Clone git repository to local drive.

Create a Musicalog database or use the Musicalog\Database\Create Database.sql file to create database after changing filepaths to database and database logfile with a path relevant to your SQL Server installation environment.

In MS Management Studio execute the Musicalog\Database\Create Database Objects.sql file to initialise new database with objects.
In MS Management Studio perform a data import on Musicalog Database using the Musicalog\Database\Musicalog Table Data.xls file to import album data.

In MusicalogService project change the MusicalogDBContext connection string in web.config file to use your local SQL Server database server name.
In MusicalogData project change the MusicalogDBContext connection string in app.config file to use your local SQL Server database server name.

WCF Service
-----------
Before opening the Musicalog Solution file it is best to prepare the local environment to support the MusicalogService WCF service.

For testing purposes the MusicalogService project was published to a local folder and IIS was configure IIS to create an application using the endpoint http://localhost:80/MusicalogService  

The MusicalogService project also has IISExpress configured to use MusicalogService on the endpoint http://localhost:61488/MusicalogService.svc and the MusicalogWeb project used a web service reference to the IISExpress endpoint for the MusicalogService.

Testing MusicalogWeb
------------------

Set the MusicalogWeb project as the startup project.
Run the project in debug mode should open the Musicalog website and use the Musicalog Service using IISExpress endpoint.

If the website fails to display a list of albums, perform a Update Service Reference on MusicalogService reference in MusicalogWeb project.
