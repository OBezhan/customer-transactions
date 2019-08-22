# Customer Inquiry

## How to run
### Using Docker
1. Open repository root directory
2. Build project using CLI:
`.\cli-windows\compose.ps1 -Build`
3. Run project using CLI:
`.\cli-windows\compose.ps1 -Run`
4. Navigate to http://localhost:7510/swagger

### Without Docker
1. Publish database using VisualStudio.
2. Add the connection string to the database section of appsettings.json:
```json
"Database": {
    "ConnectionString": "Your connection string here."
  }
  ```
3. Run application using Visual Studio.

### Existing test data
- Customers with id from 1 to 3  was added for testing purposes.

### PS:
- Uforunatly, I did not have enough time to write unit && integration tests(
- Db publishing in docker could be done better, but the right way is too complicated for the test project.
- In the real life, .env file should not be commited.
