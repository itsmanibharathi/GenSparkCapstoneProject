# Real Estate Application

## Overview

This real estate application is developed using .NET, SQL, and React.js. The application allows users to browse properties, subscribe to plans, chat with property owners, and rate properties. It supports various types of properties, including homes, shops, land, and PGs (Paying Guest accommodations).

## Features

- **User Registration and Authentication:** Users can register and log in to the application.
- **Property Listings:** Users can browse properties for sale, rent, or lease.
- **Subscription Plans:** Users can subscribe to different plans to receive notifications about new properties.
- **Chat:** Users can chat with property owners.
- **Ratings:** Users can rate and review properties.
- **Notifications:** Users receive notifications about new property arrivals based on their subscription.

## Entities

### User
- `UserId`: Unique identifier for the user
- `Name`: Name of the user
- `Email`: Email address of the user
- `PhoneNumber`: Phone number of the user
- `Address`: Address of the user
- `City`: City of the user
- `State`: State of the user
- `ZipCode`: Zip code of the user
- `ProfileImageUrl`: URL of the user's profile image
- `IsActive`: Indicates if the user is active
- `IsVerified`: Indicates if the user is verified
- `TenantVerificationCode`: Tenant verification code
- `VerificationCode`: Verification code
- `PropertiesOwned`: List of properties owned by the user
- `Subscriptions`: List of subscriptions the user has
- `Ratings`: List of ratings given by the user
- `Chats`: List of chats involving the user
- `TenantVerificationNumber`: Unique verification number for the tenant

### User Auth
- `UserId`: Unique identifier for the user
- `Password`: User's password

### User Subscription
- `UserSubscriptionId`: Unique identifier for the user subscription
- `UserId`: Unique identifier for the user
- `SubscriptionId`: Unique identifier for the subscription plan
- `SubscriptionDate`: Date when the subscription started
- `ExpiryDate`: Date when the subscription expires
- `User`: The user who subscribed
- `Subscription`: The subscription plan

### Subscription Plan
- `SubscriptionId`: Unique identifier for the subscription plan
- `Name`: Name of the subscription plan
- `Price`: Price of the subscription plan
- `PriceUnit`: Unit of the price (e.g., USD, EUR)
- `ValidityInDays`: Number of days the subscription is valid
- `IsFeatured`: Indicates if the subscription plan is featured
- `IsActive`: Indicates if the subscription plan is active
- `UserSubscriptions`: List of user subscriptions for this plan

### Property
- `PropertyId`: Unique identifier for the property
- `Title`: Title of the property
- `Description`: Description of the property
- `Price`: Price of the property
- `PriceUnit`: Unit of the price (e.g., USD, EUR)
- `Landmark`: Landmark near the property
- `Street`: Street address of the property
- `City`: City where the property is located
- `State`: State where the property is located
- `Country`: Country where the property is located
- `ZipCode`: Zip code of the property
- `Latitude`: Latitude of the property
- `Longitude`: Longitude of the property
- `Category`: Category of the property (e.g., Home, Shop, Land, PG)
- `Type`: Type of the property (e.g., Rent, Sale, Lease)
- `OwnerId`: Unique identifier for the owner of the property
- `Status`: Status of the property (e.g., Active, Inactive, Sold, Rented)
- `Amenities`: List of amenities available at the property
- `Features`: List of features of the property
- `MediaFiles`: List of media files related to the property
- `Ratings`: List of ratings for the property
- `Chats`: List of chats related to the property

### Amenity
- `AmenityId`: Unique identifier for the amenity
- `Name`: Name of the amenity
- `Description`: Description of the amenity
- `IsPaid`: Indicates if the amenity is paid
- `PropertyId`: Unique identifier for the property
- `Property`: The property associated with the amenity

### Home
- `Area`: Area of the home
- `NumberOfBedrooms`: Number of bedrooms in the home
- `NumberOfBathrooms`: Number of bathrooms in the home
- `YearBuilt`: Year the home was built
- `FurnishingStatus`: Furnishing status of the home (e.g., Furnished, SemiFurnished, Unfurnished)
- `FloorNumber`: Floor number of the home
- `HomeStatus`: Status of the home (e.g., ReadyToMove, UnderConstruction)

### Shop
- `Area`: Area of the shop
- `BusinessType`: Type of business (e.g., Retail, Restaurant, Other)
- `RentPrice`: Rent price of the shop
- `RentPriceUnit`: Unit of the rent price
- `SalePrice`: Sale price of the shop
- `SalePriceUnit`: Unit of the sale price
- `LeasePrice`: Lease price of the shop
- `LeasePriceUnit`: Unit of the lease price

### Land
- `LandArea`: Area of the land
- `ZoningInformation`: Zoning information of the land
- `LandType`: Type of the land (e.g., Residential, Commercial, Agricultural)

### PG
- `RoomTypes`: Types of rooms available
- `Occupancy`: Occupancy details
- `Facilities`: Facilities available
- `RentPrice`: Rent price for the PG
- `RentPriceUnit`: Unit of the rent price
- `LeasePrice`: Lease price for the PG
- `LeasePriceUnit`: Unit of the lease price

### Media File
- `MediaFileId`: Unique identifier for the media file
- `Title`: Title of the media file
- `Description`: Description of the media file
- `Type`: Type of media file (e.g., Image, Video)
- `PropertyId`: Unique identifier for the property
- `Property`: The property associated with the media file

### Chat
- `ChatId`: Unique identifier for the chat
- `PropertyId`: Unique identifier for the property
- `SenderUserId`: Unique identifier for the sender user
- `ReceiverId`: Unique identifier for the receiver user
- `Message`: Chat message
- `Timestamp`: Timestamp of the chat
- `IsRead`: Indicates if the message has been read

### Rating
- `RatingId`: Unique identifier for the rating
- `PropertyId`: Unique identifier for the property
- `RatingValue`: Rating value
- `Comments`: Comments about the rating
- `UserId`: Unique identifier for the user who gave the rating

## Enums

- **PropertyStatus:** Active, Inactive, Sold, Rented
- **PropertyCategory:** Home, Shop, Land, PG
- **PropertyType:** Rent, Sale, Lease
- **FurnishingStatus:** Furnished, SemiFurnished, Unfurnished
- **ConstructionStatus:** ReadyToMove, UnderConstruction
- **Direction:** North, South, East, West
- **OwnershipType:** Freehold, Leasehold
- **BusinessType:** Retail, Restaurant, Other
- **MediaType:** Image, Video
- **LandType:** Residential, Commercial, Agricultural

## Class Diagram

```mermaid
classDiagram
    class User {
        int UserId
        string Name
        string Email
        string PhoneNumber
        string Address
        string City
        string State
        string ZipCode
        string ProfileImageUrl
        bool IsActive
        bool IsVerified
        int TenantVerificationCode
        int VerificationCode
        ICollection~Property~ PropertiesOwned
        ICollection~UserSubscription~ Subscriptions
        ICollection~Rating~ Ratings
        ICollection~Chat~ Chats
    }

    class UserAuth {
        int UserId
        string Password
    }

    class UserSubscription {
        int UserSubscriptionId
        int UserId
        int SubscriptionId
        DateTime SubscriptionDate
        DateTime ExpiryDate
        User User
        Subscription Subscription
    }

    class Subscription {
        int SubscriptionId
        string Name
        decimal Price
        string PriceUnit
        int ValidityInDays
        bool IsFeatured
        bool IsActive
        ICollection~UserSubscription~ UserSubscriptions
    }

    class Property {
        int PropertyId
        string Title
        string Description
        decimal Price
        string PriceUnit
        string Landmark
        string Street
        string City
        string State
        string Country
        string ZipCode
        double Latitude
        double Longitude
        PropertyCategory Category
        PropertyType Type
        int OwnerId
        PropertyStatus Status
        ICollection~Amenity~ Amenities
        ICollection~PropertyFeature~ Features
        ICollection~MediaFile~ MediaFiles
        ICollection~Rating~ Ratings
        ICollection~Chat~ Chats
    }

    class Amenity {
        int AmenityId
        string Name
        string Description
        bool IsPaid
        int PropertyId
        Property Property
    }

    class Home {
        int Area
        int NumberOfBedrooms
        int NumberOfBathrooms
        int YearBuilt
        FurnishingStatus FurnishingStatus
        int FloorNumber
        HomeStatus HomeStatus
    }

    class Shop {
        int Area
        BusinessType BusinessType
        decimal RentPrice
        string RentPriceUnit
        decimal SalePrice
        string SalePriceUnit
        decimal LeasePrice
        string LeasePriceUnit
    }

    class Land {
        int LandArea
        string ZoningInformation
        LandType LandType
    }

    class PG {
        string RoomTypes
        string Occupancy
        string Facilities
        decimal RentPrice
        string RentPriceUnit
        decimal LeasePrice
        string LeasePriceUnit
    }

    class MediaFile {
        int MediaFileId
        string Title
        string Description
        MediaType Type
        int PropertyId
        Property Property
    }

    class Chat {
        int ChatId
        int PropertyId
        int SenderUserId
        int ReceiverId
        string Message
        DateTime Timestamp
        bool IsRead
    }

    class Rating {
        int RatingId
        int PropertyId
        int RatingValue
        string Comments
        int UserId
    }

    enum PropertyStatus {
        Active
        Inactive
        Sold
        Rented
    }

    enum PropertyCategory {
        Home
        Shop
        Land
        PG
    }

    enum PropertyType {
        Rent
        Sale
        Lease
    }

    enum FurnishingStatus {
        Furnished
        SemiFurnished
        Unfurnished
    }

    enum HomeStatus {
        ReadyToMove
        UnderConstruction
    }

    enum BusinessType {
        Retail
        Restaurant
        Other
    }

    enum MediaType {
        Image
        Video
    }

    enum LandType {
        Residential
        Commercial
        Agricultural
    }

    User "1" --> "1" UserAuth
    User "1" --> "*" UserSubscription
    UserSubscription "*" --> "1" Subscription
    User "1" --> "*" Property
    Property "*" --> "1" User : Owner
    Property "*" --> "*" Amenity
    Property "*" --> "*" MediaFile
    Property "*" --> "*" Rating
    Property "*" --> "*" Chat
```
