# :pencil2: Telecom Service System
This is my defense project for **ASP.NET Core MVC** course at [SoftUni](https://softuni.bg). 

Live at <a href='https://telecomservicesystem.azurewebsites.net/'>https://telecomservicesystem.azurewebsites.net/</a>

For test use 

Username: admin

Password: 123456

# :memo: Overview
&nbsp;&nbsp;&nbsp;&nbsp;**Telecom Service System** is a platform for trading telecomunication services. The main idea is to gather trading, administration and engineering features in one place. The services are categorized into two categories, mobile and fixed.
  
&nbsp;&nbsp;&nbsp;&nbsp;In the app have 3 roles Administrator , Seller and Engineer.

&nbsp;&nbsp;&nbsp;&nbsp;In left nav site bar have buttons to all sections.

&nbsp;&nbsp;&nbsp;&nbsp;Only administrators can access Search Employyes, can track all Orders, can add new Employee(application user) new Service and new City. Only they can track all tasks and can delete employees, customers, services and cities.

&nbsp;&nbsp;&nbsp;&nbsp;Sellers can create new Custommers, can search in "Customers" section, can edit customers, can trade services to customer. Sellers can track only orders they have created.

&nbsp;&nbsp;&nbsp;&nbsp;Engineers can only see tasks assigned to their team and can complete them.


&nbsp;&nbsp;&nbsp;&nbsp;In section search can search for employees, customers and orders and can delete them from button "Delete". In employees search section can choose a employee and can edit the choosen one. 
In customer search section can choose a customer, can edit the choosen one and can edit his addresses. By clicking on nav button "Servisec" over customer's info can see all active services of this customer, on that panel can renew the contract of expiring services(they are colored in red) or can close the contract. From button "+ Create new service" on that panel will be redirected on page where have to choose mobile or fixed service you want to activate. The pages of mobile and fixed services are similar, you have to choose service plan, number and contract duration, with the difference that the fixed services page has an option to select an address, add a new address and select the time and date for installation based on the free teams for the city. After that you can generate contract in new tab with PhantomJs and can uplaod it in Cloudinary.
By clikcing on "Billing account" at nav buttons on customer panel over customer's info you can see all invoices of that customer from link to Cloudinary. 
The invoices are generated with PhantomJs automatically every month on the first day by Hangfire service. They are send to customer's email with SendGrid and are uploaded to Cloudinary.

In Orders search section you can track created orders by few criteria.

In section "Create" in left nav site bar can create new Employee(application user) and can add him to role. If role is engineer have to add him to an existing team or can create new team and add him to it. From button "New Customer" you can create new customer with his main address. From button "Add City" can add new City in which the service provider operates. From button "Add Service" you can create a new rate plan that the service provider offers.

In section "Engineering" in left nav site bar you can see all tasks for execution. The new tasks are added to the page without reloading via SignalR. The Engineers can see details for the visitation by clicking on the id of the task and can finish the task if the instalation is completed.

In section "Delete" in left nav site bar you can list and delete created services(rate plans) and cities.


# :hammer: Built With:
* Deployed in Azure at <a href='https://telecomservicesystem.azurewebsites.net/'>https://telecomservicesystem.azurewebsites.net/</a>
* ASP.NET Core 5 MVC
* Blazor Server
* ASP.NET Core view components
* ASP.NET Core areas
* MSSQL Server
* Entity Framwork Core 5
* AutoMapper
* SignalR
* PhantomJs
* JQuery / AJAX
* Hangfire
* Twilio SendGrid
* Cloudinary
* Bootstrap
* Moq
* xUnit