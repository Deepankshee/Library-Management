# Library-Management
* Right now it is a small scope, I have used dictionory(in-memory) to store book details, but this can
replaced with actual DB as we enhance the scope.
* We are keeping Book >> ISBN as string for simplicity but when scope grows this can become complex object
* We are keeping only the borrowed books list in the user class, this can be enhanced to add more behaviour/properties related to user, 
   but it is not required at this point of time.
* GetBookQuantity >> LibraryService - created this method to check the state of bookinventory but this can be refactored later with more usable method.
