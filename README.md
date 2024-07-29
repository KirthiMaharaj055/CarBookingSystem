# CarBookingSystem
I did a tutorial called: MongoDB Provider for EF Core Tutorial: Building an App with CRUD and Change Tracking.
Car booking application using the new  MongoDB Provider for EF Core
A sample application using ASP.NET MVC that shows CRUD with change tracking using the new MongoDB EF Core Provider.

The app is a simple car garage and booking system that allows you to add, edit and delete cars and bookings.

## Prerequisites
- .NET 8.0
- [MongoDB Atlas Account](https://www.mongodb.com/cloud/atlas)
- [Free M0 Tier Cluster](https://www.mongodb.com/cloud/atlas/pricing)

## Running the application

In order to run the application, add your [Atlas Connection String](https://www.mongodb.com/docs/atlas/tutorial/connect-to-your-cluster/) to your `appsettings.json` and `appsettings.Development.json` files.

```sh
dotnet run
