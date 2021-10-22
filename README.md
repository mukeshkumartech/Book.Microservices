# Book.Microservices

A .Net Core Microservices

GET
      Get the list of books based on Title, Author and ISBN
      http://localhost:55294/api/book/get

      **Parameters**: {
          "Id" : 0,
          "Title": "Title 1",
          "Author": "Author 2",
          "PublicationDate": "",
          "ISBN": 1234567890123
      }
      
      **Output:**
      {
          "data": "[{\"Id\":1,\"Title\":\"Test Title 1\",\"Author\":\"Author 1\",\"ISBN\":1234567890123,
          \"PublicationDate\":\"2021-10-22 15:31:47.720\"},
          {\"Id\":2,\"Title\":\"Test Title 2\",\"Author\":\"Author 2\",\"ISBN\":1345678902123,\"PublicationDate\":\"2021-10-22 15:31:47.720\"}]",
          "succeeded": true,
          "message": "Books data gets successfully."
     }
POST
    Add the new book
    http://localhost:55294/api/book/ADD
    
    **parameters**
      {
        "Title": "Title 11",
        "Author": "Author 21",
        "PublicationDate": "2021-10-22 15:31:47.720",
        "ISBN": 1234567890123
      }
      
     **Output**
         {
            "data": null,
            "succeeded": true,
            "message": "Book added successfully."
        }
UPDATE
    Update the existing book
    http://localhost:55294/api/book/update
    
    **Parameters**

    {

        "Title": "Title 111",
        "Author": "Author 211",
        "PublicationDate": "2021-10-22 15:31:47.720",
        "ISBN": 1234567890123,
        "Id": 2
    }
    
    
    **Output**
    {
        "data": null,
        "succeeded": true,
        "message": "Book updated successfully."
    }
DELETE
      Delete the existing book
      http://localhost:55294/api/book/delete?id=2
      
      **Output**
      {"data":null,"succeeded":true,"message":"Book deleted successfully."}
