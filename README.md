# Invoice API

ASP.NET Core Web API backing the invoice viewer. Serves invoice data from SQL Server.

## Running it

Needs .NET 10 SDK and a SQL Server instance.

1. Run `init.sql` against your database - creates `Invoices` and `InvoiceItems` and inserts one sample invoice.
2. Create `appsettings.Development.json` (gitignored, won't exist after cloning):

```json
{
  "ConnectionStrings": {
    "ConnectionString": "Data Source=<server>;Initial Catalog=<database>;User Id=<user>;Password=<password>;TrustServerCertificate=True;"
  }
}
```

3. `dotnet run`
4. Swagger's at `/swagger`

## Endpoints

- `GET /api/invoice/{id}` - invoice by id, with items and total
- `GET /api/invoice` - latest invoice
- `GET /api/data` - basic health check

CORS is wide open since the frontend lives in its own repo and calls this cross-origin.
