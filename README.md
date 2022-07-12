# OilTrendApplication

### Build and Start-up Procedure

**PREMISE**:  In order for the build procedure to be successfully completed, the following software requirements must be present on the development machine:

- Visual Studio IDE (vers. 2015/2017/2019/2022) with .NET Framework 4.6.1 support
- Client for sending http requests (e.g. Postman or Web Browser)


The OilTrendApplication allows the query of an archive of daily Brent EU prices. The list refers to https://datahub.io/core/oil-prices/r/brent-daily.json. 
The data in the archive cover a period from 20/05/1987 to 28/08/2020.
The technology used is .NET Framework 4.6.1 in the C# language and implements the [JSON-RPC](https://www.jsonrpc.org/specification) protocol with streaming over HTTP. 
When the application is started, the system queries the price archive and saves all data in a SQLite file on which it will execute all queries.

The Application is a VS solution with two main projects OilTrendApplication and OilTrendApplicationTests

### OilTrendApplication Execution
- Open sln file with Visual Stuido
- In Solution Explorer execute a build of the entire solution. 
- Run the OilTrendApplication project
- At startup time a command prompt is displayed, the message **Listening for JSON-RPC requests** confirms that the application is active and listening
- The application is running on port 5000 of localhost. To send requests, as per the JSON-RPC protocol, make GET or POST calls:
- The main parameters for requests are:
	- [method] represents the method invoked (in our case oilprice.Trend)
	- [id] alphanumeric identifier associated with the call 
	- [startDateISO8601] searching start date in ISO 'YYYY-MM-DD' format
	- [endDateISO8601] searching end date in ISO 'YYYY-MM-DD' format
```
The input parameters [startDateISO8601] and [endDateISO8601] may be valorised as follows: 
-[startDateISO8601] and the [endDateISO8601] valued limits the search within the extremes
-[startDateISO8601] unvalued and the [endDateISO8601] valued will extract all records with a date equal to/previous to the end date
-[startDateISO8601] valued and the [endDateISO8601] unvalued all records with a date equal to/before the start date will be extracted
-[startDateISO8601] and [endDateISO8601] both unvalorised, all records in the db will be extracted
```
### EXAMPLE GET Call

Once the service has started, make the call
```
GET http://localhost:5000/?id=1&method=oilprice.Trend&startDateISO8601=2015-01-08&endDateISO8601=2015-02-08
```
Response:
```json
{
    "jsonrpc": "2.0",
    "id": "1",
    "result": {
        "prices": [
            {
                "dateISO8601": "2015-01-08",
                "price": 49.43
            },
            {
                "dateISO8601": "2015-01-09",
                "price": 47.64
            },
            ...
        ]
    },
    "error": null
}
```
### EXAMPLE POST Call

Once the service has started, make the call
```
POST http://localhost:5000
```
Body
```json
{
	"jsonrpc": "2.0",
	"method": "oilprice.Trend",
	"params": {
		"startDateISO8601": "2015-01-08",
		"endDateISO8601": "2015-02-08"
	},
	"id": "1"
}
```
Response
```json
{
    "jsonrpc": "2.0",
    "id": "1",
    "result": {
        "prices": [
            {
                "dateISO8601": "2015-01-08",
                "price": 49.43
            },
            {
                "dateISO8601": "2015-01-09",
                "price": 47.64
            },
            ...
        ]
    },
    "error": null
}
```
### OilTrendApplicationTest execution
**PREMISE** In order for the application to work you need to run the exe from the project folder at the path ..\OilTrendApplication\bin\[Build Profile]\OilTrendApplication.exe. In case you have not yet compiled the project proceed with the compilation and start the generated exe.

### How to start the test
Make sure the exe is started and the command prompt with the message **Listening for JSON-RPC requests** is visible.
Enter Visual Studio and from the Solution Explorer select the **OilTrendApplicationTests** project, right click and select the **Run Tests** command.
The results will be available in the Test Explorer.
