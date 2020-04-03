# Lab13-Async-Inn
API Server for a Hotel Asset Management System
by Andrew Casper and Harry Cogswell
April 1st, 2020

## Description
![Image](assets/AsyncInn2.png)

### Tables
- __Hotel__ has all the information for each hotel location. This includes an ID as its integer *primary key*, the Name of the location, the city, state, and address of location, as well as a phone number, all of which are nvarchars. It also references the HotelRoom table as a navigation property.
- __Room__ has the information for the different room designs, with an integer *primary key* ID, a Name nvarchar to represent the each nickname, and an int enum layout which distinguishes between a studio, single bedroom, or double bedroom layout. This table also references the HotelRoom and RoomAmenities join tables as navigation properties.
- __Amenities__  is a table of offered amenities, it includes an integer *primary key* ID , as well as the name of the amenity as an nvarchar. It has a reference to the RoomAmenities table as a navigation property.
- __HotelRoom__ is a join table that has all the information about each individual room. It has a composite key of the HotelID from the Hotel table and a RoomNumber integer value. It also has the RoomID from the Room table as a foreign key. As payload values, it has the rate of the room as a decimal, and whether or not the room is pet-friendly as a bit. As navigation properties, it has the Room and Hotel tables.
- __RoomAmenities__ is the join table combining room designs and amenities. It has the AmenitiesID and RoomID values as composite and foreign keys, and navigation properties to the Amenities and Room tables.

### Relationships
- Our core tables are Hotel, Room, and Amenities. Those three tables are related to each other by our join tables, HotelRoom and RoomAmenities, and have navigation properties to those tables. The join tables use the primary keys from the three core tables as composite keys. They also have navigation links back to the core tables.

*AsyncInnAPI v0.2*
