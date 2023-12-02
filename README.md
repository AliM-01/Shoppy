# Shoppy

This is a modular e-commerce project consisting of various modules that cater to different needs of an e-commerce website. The project currently has the following modules:

#### Shop Module 🛍️

The Shop module handles the storefront functionality of the API, including displaying products, creating and managing orders, and handling customer information.

#### Auth Module 🔒

The Auth module handles user authentication and authorization for the API. Users can create accounts, log in, and access protected endpoints based on their role.

#### Inventory Module 📦

The Inventory module is responsible for managing product inventory and stock levels. It also handles product pricing and discounts.

#### Blog Module 📝

The Blog module handles the content management of the API, allowing users to create and manage blog posts and articles related to products and the e-commerce industry.

#### Order Module 📦

The Order module handles the complete order lifecycle. This includes processing payments, managing order fulfillment, and generating order reports.

#### Comment Module 💬

The Comment module enables customers to leave feedback and comments on products. This module also allows for moderation and management of comments.

#### Discount Module 💰

The Discount module handles the creation and management of discounts and promotions for products. This module enables the creation of various types of discounts, including percentage-based, fixed amount, and free shipping.

## Technologies

- C#
- .NET 6
- Monog Db

## Getting Started

To run the project locally on your machine, follow these steps:

1. Clone the repository to your local machine using `git clone https://github.com/spaleet/Shoppy`
2. Open the solution file in Visual Studio
3. Update the connection string in the `appsettings.json` file with your own database credentials
5. Start the application by running the dotnet run command in the terminal
6. Browse the API documentation at `https://localhost:5001/swagger`
