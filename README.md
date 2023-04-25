# Order Service
This is a basic .Net API endpoint that can receive some JSON representing a typical e-commerce order and save it into a SQL database using entity framework.It checks the total of the order against price data stored in the database, checks that all products exist in the database, and checks that there is enough stock of each item in the database. 

To run create a database with a Products and Orders table, based on the following models.
```javascript
  Order
  {
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public string? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProductIds { get; set; }
   }
   
   Product
   {
       public Guid Id { get; set; }  
       public int Stock { get; set; }
       public decimal Price { get; set; }
   }
```
 
Then simply running the Order Service through IISExpress will bring up a swagger interface where the following example json can be run. (Products in the json should be added to the database first).

## ENDPOINTS:

```http Post /PlaceOrder/ ```

Json Example 
```javascript
{
  "order": {
    "id": "bc458974-94df-454e-9ef4-9cee237c4a58",
    "total": "6.50"
  },
  "customer": {
    "name": "Hannah Ruddy"
  },
  "products": [
    {
      "id": "01c1b440-4789-4e79-8c37-6c02e07c06f0",
      "quantity": 1
    },
    {
      "id": "452185ff-6529-487d-9173-8dd08d24eb5c",
      "quantity": 1
    },
    {
      "id": "4d512429-29e1-444e-b974-905ce6dcc3c7",
      "quantity": 1
    }
  ]
}
```
