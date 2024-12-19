using System.Data.SQLite;

//readData(createConnection());
//insertCustomer(createConnection());
//RemoveCustomer(createConnection());
//FindCustomer(createConnection());
//DisplayProduct(createConnection());
//DisplayProductWithCategory(createConnection());
//InsertCustomer(createConnection());
DisplayCustomers(createConnection());
DeleteCustomer(createConnection());

static SQLiteConnection createConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=barSuperKangelasteKohvik.db; Version = 3; New = True; Compress = True"); //Source=mydb.db - teine andmebaas
    try
    {
        connection.Open();
        //Console.WriteLine("DataBase found");
    }
    catch
    {
        Console.WriteLine("DataBase not found.");
    }
    return connection;
}

static void readData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";
    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"RowID: {readerRowId}; Full name: {readerStringFirstName} {readerStringLastName}; DoB: {readerStringDoB}");
    }
    myConnection.Close();
}

static void insertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string firstName, lastName, DoB;

    Console.WriteLine("Enter first name:");
    firstName = Console.ReadLine();
    Console.WriteLine("Enter Last name:");
    lastName = Console.ReadLine();
    Console.WriteLine("Enter the date of bith (mm-dd-yyyy):");
    DoB = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
        $"VALUES ('{firstName}', '{lastName}', '{DoB}')";
    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Rows inserted: {rowInserted}");

    readData(myConnection);
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an Id of the customer you want to remove:");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowDeleted = command.ExecuteNonQuery();
    Console.WriteLine($"{rowDeleted} was removed from the table customer");

    readData(myConnection);

}

static void FindCustomer(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;
    string searchName;
    Console.WriteLine("Enter a first name to display customer data");
    searchName = Console.ReadLine();
    command = myConnection.CreateCommand();
    command.CommandText = $"SELECT customer.rowid, customer.firstName, customer.lastName, status.statusType " +
        $"FROM customerStatus " +
        $"JOIN customer ON customer.rowid = customerStatus.customerId " +
        $"JOIN status ON status.rowid = customerStatus.statusId " +
        $"WHERE firstname LIKE '{searchName}' ";
    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringStatus = reader.GetString(3);
        Console.WriteLine($"Search result: ID: {readerRowId}. {readerStringName} {readerStringLastName}. Status: {readerStringStatus}");
    }
    myConnection.Close();
}

static void DisplayProduct(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();

    command.CommandText = "SELECT rowid, ProductName, Price FROM Product";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        string readerRowid = reader["rowid"].ToString();
        string readerProductName = reader.GetString(1);
        int readerProductPrice = reader.GetInt32(2);
        Console.WriteLine($"{readerRowid}. {readerProductName}. Price: {readerProductPrice}");
    }
    myConnection.Close();
}

static void DisplayProductWithCategory(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command; 
    command = myConnection.CreateCommand();
    command.CommandText = "SELECT Product.rowid, Product.ProductName, ProductCategory.CategoryName FROM product " +
        "JOIN ProductCategory ON ProductCategory.rowid = Product.CategoryId";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        string readerRowid = reader["rowid"].ToString();
        string readerProductName = reader.GetString(1);
        string readerProductCategory = reader.GetString(2);

        Console.WriteLine($"{readerRowid}. {readerProductName}. Category: {readerProductCategory}");
    }
    myConnection.Close();
}

static void InsertCustomer (SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;
    string FName, LName;

    Console.WriteLine("First Name");
    FName = Console.ReadLine();
    Console.WriteLine("Last Name");
    LName = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO Customer (FirstName, LastName) VALUES ('{FName}', '{LName}')";
    int rowsInserted = command.ExecuteNonQuery();
    Console.WriteLine($"{rowsInserted} new row has been inserted.");
    DisplayCustomers(myConnection);
}

static void DisplayCustomers(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();

    command.CommandText = "SELECT rowid, FirstName, LastName FROM Customer";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        string readerRowid = reader["rowid"].ToString();
        string readerFirstName = reader.GetString(1);
        string readerLastName = reader.GetString(2);
        Console.WriteLine($"{readerRowid}. {readerFirstName}. Price: {readerLastName}");
    }
    myConnection.Close();
}

static void DeleteCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an id to delete:");
    idToDelete = Console.ReadLine();
    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowsDeleted = command.ExecuteNonQuery();
    Console.WriteLine($"{rowsDeleted} has been deleted.");
    DisplayCustomers(myConnection);
}