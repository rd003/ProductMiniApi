# ProductMiniApi

APIs for Image manipulation (Add/Update/Delete/GetALL)

## Tech Stack

- .Net 6 web api
- .EF Core 6
- MSSQL

## Endpoints

- GET: api/products

  ```
   //response

    [{"id":1,"productName":"Product 1","productImage":"5b7c9e4d-3324-4f71-9b4c-a938d7edafde.jpg","imageFile":null},{"id":2,"productName":"Product 2","productImage":"347c5bf5-df20-4b30-ad63-677c829820d2.jpg","imageFile":null}]
  ```

- POST: api/products
  Content Type : Form Data
  Body:

  ```
   ProductName : STRING | REQUIRED
   ImageFile: FILE | REQUIRED
  ```

  - PUT: api/products/ {id:INTEGER}
    Content Type : Form Data
    Body:

  ```
   Id: INTEGER | REQUIRED
   ProductName : STRING | REQUIRED
   ImageFile: FILE (Pass null , if you dont want to update image),
   ProductImage : STRING | REQUIRED
  ```

- DELETE: api/products/{id:INTEGER}
